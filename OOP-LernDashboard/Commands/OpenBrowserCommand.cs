using System.Diagnostics;

namespace OOP_LernDashboard.Commands
{
    internal class OpenBrowserCommand : CommandBase
    {
        private readonly string _url;

        public OpenBrowserCommand(string url)
        {
            _url = url;
            // if url does not have https:// add it
            if (!url.StartsWith("https://"))
                _url = "http://" + url;
        }

        public override void Execute(object? parameter)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = _url, UseShellExecute = true });
        }
    }
}
