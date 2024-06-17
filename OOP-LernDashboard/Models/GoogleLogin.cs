using HandyControl.Tools.Extension;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace OOP_LernDashboard.Models
{
    /// <summary>
    /// Googel Login Logic to receive auth token
    /// </summary>
    internal class GoogleLogin
    {
        public string? AuthToken { get; set; }
        public event EventHandler<(string accessToken, string? refreshToken)>? AuthTokenReceived; // When refresh token is null, the user is loggin in again and the refresh token is not needed to update

        /// <summary>
        /// Authencitation data from https://console.cloud.google.com/apis/credentials?project=oop-lerndashboard
        /// </summary>
        const string ClientID = "995981057556-b16365t95bbj7uj0thdoq192bm0obm9a.apps.googleusercontent.com";
        const string ClientSecret = "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";
        const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";

        public GoogleLogin() { }

        /// <summary>
        /// Main methode to call when the user wants to login into account
        /// </summary>
        public async void PerformLogin()
        {
            // Generates state and PKCE values.
            string state = randomDataBase64url(32);
            string code_verifier = randomDataBase64url(32);
            string code_challenge = base64urlencodeNoPadding(sha256(code_verifier));
            const string code_challenge_method = "S256";

            // Creates a redirect URI using an available port on the loopback address.
            string redirectURI = string.Format("http://{0}:{1}/", IPAddress.Loopback, GetRandomUnusedPort());

            // Creates an HttpListener to listen for requests on that redirect URI.
            var http = new HttpListener();
            http.Prefixes.Add(redirectURI);
            http.Start();

            // Creates the OAuth 2.0 authorization request.
            string authorizationRequest = string.Format("{0}?response_type=code&scope=https://www.googleapis.com/auth/calendar%20openid%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}&access_type=offline&prompt=consent",
                AuthorizationEndpoint,
                System.Uri.EscapeDataString(redirectURI),
                ClientID,
                state,
                code_challenge,
                code_challenge_method);

            // Opens request in the browser.
            System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = authorizationRequest, UseShellExecute = true });

            // Waits for the OAuth authorization response.
            var context = await http.GetContextAsync();

            // Sends an HTTP response to the browser.
            var response = context.Response;
            string responseString = string.Format("<html><head><meta http-equiv='content-type' content='text/html; charset=utf-8'><meta http-equiv='refresh' content='5;url=about:blank' /><title>Erfolgreich angemeldet</title><style>body{{background-color:#121212;display:flex;justify-content:center;align-items:center;flex-direction:column;color:white;font-family:Arial,sans-serif;height:100%;overflow:hidden;}}</style></head><body><h1>Erfolgreich angemeldet!</h1><p>Du kannst wieder das LernDashboard öffnen und diesen Tab schließen.</p></body></html>");
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;

            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();
                Console.WriteLine("HTTP server stopped.");
            });

            // Checks for errors.
            if (context.Request.QueryString.Get("error") != null)
            {
                MessageBox.Show($"OAuth authorization error: {context.Request.QueryString.Get("error")}.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (context.Request.QueryString.Get("code") == null
                || context.Request.QueryString.Get("state") == null)
            {
                MessageBox.Show("Malformed authorization response. " + context.Request.QueryString, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // extracts the code
            var code = context.Request.QueryString.Get("code");
            var incoming_state = context.Request.QueryString.Get("state");

            // Compares the receieved state to the expected value, to ensure that
            // this app made the request which resulted in authorization.
            if (incoming_state != state)
            {
                MessageBox.Show($"Received request with invalid state ({incoming_state})", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Starts the code exchange at the Token Endpoint.
            PerformCodeExchange(code, code_verifier, redirectURI);
        }

        /// <summary>
        /// Sends a request to the token endpoint to exchange the code for an access token
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        private async Task<string> SendTokenRequestAsync(string requestBody)
        {
            string tokenRequestURI = "https://www.googleapis.com/oauth2/v4/token";

            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            byte[] byteVersion = Encoding.ASCII.GetBytes(requestBody);
            tokenRequest.ContentLength = byteVersion.Length;

            using (Stream stream = tokenRequest.GetRequestStream())
            {
                await stream.WriteAsync(byteVersion, 0, byteVersion.Length);
            }

            try
            {
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseText = await reader.ReadToEndAsync();
                            MessageBox.Show("HTTP: " + response.StatusCode + "\n" + responseText, "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Parses the response from the token endpoint
        /// </summary>
        /// <param name="responseText"></param>
        /// <returns></returns>
        private Dictionary<string, string> ParseResponse(string responseText)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText)
                ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the access token from the code when User logs in for the first time
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codeVerifier"></param>
        /// <param name="redirectURI"></param>
        private async void PerformCodeExchange(string code, string codeVerifier, string redirectURI)
        {
            string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code&access_type=offline&prompt=consent",
                code,
                Uri.EscapeDataString(redirectURI),
                ClientID,
                codeVerifier,
                ClientSecret
            );

            string responseText = await SendTokenRequestAsync(tokenRequestBody);

            if (!string.IsNullOrEmpty(responseText))
            {
                Dictionary<string, string> tokenEndpointDecoded = ParseResponse(responseText);
                string accessToken = tokenEndpointDecoded["access_token"];
                string? refreshToken = tokenEndpointDecoded.ContainsKey("refresh_token") ? tokenEndpointDecoded["refresh_token"] : null;

                this.AuthToken = accessToken;

                // Raise the event when the token is received
                AuthTokenReceived?.Invoke(this, (accessToken, refreshToken));
            }
        }

        /// <summary>
        /// Gets the new access token from the refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            string tokenRequestBody = string.Format("client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token&access_type=offline&prompt=consent",
                ClientID,
                ClientSecret,
                refreshToken
            );

            string responseText = await SendTokenRequestAsync(tokenRequestBody);

            if (!string.IsNullOrEmpty(responseText))
            {
                Dictionary<string, string> tokenEndpointDecoded = ParseResponse(responseText);
                string accessToken = tokenEndpointDecoded["access_token"];

                this.AuthToken = accessToken;

                if (!accessToken.IsNullOrEmpty())
                {
                    // Raise the event when the token is received
                    AuthTokenReceived?.Invoke(this, (accessToken, null));
                }
                    
                return accessToken;
            }

            return null;
        }

        /// <summary>
        /// returns random unused Port used for Browser interaction
        /// </summary>
        /// <returns></returns>
        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        /// <summary>
        /// Returns URI-safe data with a given input length.
        /// </summary>
        /// <param name="length">Input length (nb. output will be longer)</param>
        /// <returns></returns>
        public static string randomDataBase64url(uint length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            // var rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return base64urlencodeNoPadding(bytes);
        }

        /// <summary>
        /// Returns the SHA256 hash of the input string.
        /// </summary>
        /// <param name="inputStirng"></param>
        /// <returns></returns>
        public static byte[] sha256(string inputStirng)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(inputStirng);
            SHA256Managed sha256 = new SHA256Managed(); // obsolete
            // var sha256 = SHA512.Create();
            return sha256.ComputeHash(bytes);
        }

        /// <summary>
        /// Base64url no-padding encodes the given input buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string base64urlencodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }

        /// <summary>
        /// Logs the user out of Google
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Logout()
        {
            this.AuthToken = null;
        }
    }
}
