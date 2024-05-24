using HandyControl.Themes;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Services.DataCreators;
using OOP_LernDashboard.Services.DataProviders;
using System.Configuration;
using System.Windows.Media;

namespace OOP_LernDashboard.Stores
{
    /// <summary>
    /// Stores the application state (and caches it locally).
    /// This avoides to call the datase every single time we need some data.
    /// </summary>
    class DashboardStore
    {
        private Lazy<Task> _initializeLazy;
        private IDataCreator<ToDo> _toDoCreator;
        private IDataProvider<ToDo> _toDoProvider;
        private IDataCreator<Shortcut> _shortcutCreator;
        private IDataProvider<Shortcut> _shortcutProvider;
        private IDataCreator<Countdown> _countdownCreator;
        private IDataProvider<Countdown> _countdownProvider;
        private IDataCreator<string> _calendarIdCreator;
        private IDataProvider<string> _calendarIdProvider;

        private readonly Models.LinkedList<ToDo> _toDos;
        public IEnumerable<ToDo> ToDos => _toDos;

        private readonly Models.LinkedList<Shortcut> _shortcuts;
        public IEnumerable<Shortcut> Shortcuts => _shortcuts;

        private readonly Models.LinkedList<Countdown> _countdowns;
        public IEnumerable<Countdown> Countdowns => _countdowns;

        private readonly Models.LinkedList<string> _calendarIds;
        public Models.LinkedList<string> CalendarIds => _calendarIds;

        public event Action<ToDo> ToDoCreated;
        public event Action<ToDo> ToDoDeleted;

        public event Action<Countdown> CountdownCreated;
        public event Action<Countdown> CountdownDeleted;

        public event Action<Shortcut> ShortcutCreated;
        public event Action<Shortcut> ShortcutDeleted;

        public event Action GoogleLoggedIn;

        public GoogleLogin GoogleLogin { set; get; }
        public GoogleCalendar? GoogleCalendar { set; get; }

        public Configuration AppConfig;

        private string _welcomeName = "Studierende Person";
        public string WelcomeName => _welcomeName;

        private string _selectedCalendarId = "";
        public string SelectedCalendarId => _selectedCalendarId;

        public DashboardStore(
            IDataCreator<ToDo> toDoDataCreator,
            IDataProvider<ToDo> toDoDataProvider,
            IDataCreator<Shortcut> shortcutDataCreator,
            IDataProvider<Shortcut> shortcutDataProvider,
            IDataCreator<Countdown> countdownCreator,
            IDataProvider<Countdown> countdownProvider,
            IDataCreator<string> calendarIdCreator,
            IDataProvider<string> calendarIdProvider)
        {
            _toDoCreator = toDoDataCreator;
            _toDoProvider = toDoDataProvider;

            _shortcutCreator = shortcutDataCreator;
            _shortcutProvider = shortcutDataProvider;

            _countdownCreator = countdownCreator;
            _countdownProvider = countdownProvider;

            _calendarIdCreator = calendarIdCreator;
            _calendarIdProvider = calendarIdProvider;

            // the lazy ensures to only load the data once from the database
            _initializeLazy = new Lazy<Task>(Initialize);

            _toDos = new Models.LinkedList<ToDo>();
            _shortcuts = new Models.LinkedList<Shortcut>();
            _countdowns = new Models.LinkedList<Countdown>();
            _calendarIds = new Models.LinkedList<string>();

            this.GoogleLogin = new GoogleLogin();
            this.GoogleLogin.AuthTokenReceived += GoogleLoginAuthTokenReceived;

            string? auth = ReadSetting("GoogleRefreshToken");
            if (auth != null && auth != "")
            {
                try
                {
                    InitCalendarFromRefreshToken(auth);
                }
                catch
                {
                    this.GoogleCalendar = null;
                }
            }

            string? name = ReadSetting("WelcomeName");
            if (name != null && name != "")
            {
                _welcomeName = name;
            }

            string? calendarId = ReadSetting("SelectedCalendarId");
            if (calendarId != null && calendarId != "")
            {
                _selectedCalendarId = calendarId;
            }
        }

        /// <summary>
        /// Initializes the data from the database when it is called the first time
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }


        /// <summary>
        /// Adds a ToDo to the database and updates the ToDo-List
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        public async Task AddToDo(ToDo toDo)
        {
            await _toDoCreator.CreateModel(toDo);
            _toDos.Add(toDo);
            ToDoCreated?.Invoke(toDo);
        }

        /// <summary>
        /// Removes a ToDo from the database and updates the ToDo-List
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        public async Task DeleteToDo(ToDo toDo)
        {
            await _toDoCreator.DeleteModel(toDo);
            _toDos.Remove(_toDos.Where(i => i.Id == toDo.Id).Single());
            ToDoDeleted?.Invoke(toDo);
        }

        /// <summary>
        /// Adds a countdown to the database and updates the Countdown-List
        /// </summary>
        /// <param name="countdown"></param>
        /// <returns></returns>
        public async Task AddCountdown(Countdown countdown)
        {
            await _countdownCreator.CreateModel(countdown);
            _countdowns.Add(countdown);
            CountdownCreated?.Invoke(countdown);
        }

        /// <summary>
        /// Removes a countdown from the database and updates the Countown-List
        /// </summary>
        /// <param name="countdown"></param>
        /// <returns></returns>
        public async Task DeleteCountdown(Countdown countdown)
        {
            await _countdownCreator.DeleteModel(countdown);
            _countdowns.Remove(_countdowns.Where(i => i.Id == countdown.Id).Single());
            CountdownDeleted?.Invoke(countdown);
        }

        /// <summary>
        /// Adds a Shortcut to the database and updates the Shortcuts-List
        /// </summary>
        /// <param name="shortcut"></param>
        /// <returns></returns>
        public async Task AddShortcut(Shortcut shortcut)
        {
            await _shortcutCreator.CreateModel(shortcut);
            _shortcuts.Add(shortcut);
            ShortcutCreated?.Invoke(shortcut);
        }

        /// <summary>
        /// Removes a Shortcut from the database and updates the ToDo-List
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        public async Task DeleteShortcut(Shortcut shortcut)
        {
            await _shortcutCreator.DeleteModel(shortcut);
            _shortcuts.Remove(_shortcuts.Where(i => i.Id == shortcut.Id).Single());
            ShortcutDeleted?.Invoke(shortcut);
        }

        public async Task ModifyShortcut(Shortcut shortcut)
        {
            await ((DatabaseShortcutCreator)_shortcutCreator).ModifyModel(shortcut);
        }

        // Loads the data from the database once
        private async Task Initialize()
        {
            IEnumerable<ToDo> toDos = await _toDoProvider.GetAllModels();
            _toDos.Clear();
            foreach (var toDo in toDos)
            {
                _toDos.Add(toDo);
            }

            IEnumerable<Shortcut> shortcuts = await _shortcutProvider.GetAllModels();
            _shortcuts.Clear();
            foreach (var shortcut in shortcuts)
            {
                _shortcuts.Add(shortcut);
            }

            IEnumerable<Countdown> countdowns = await _countdownProvider.GetAllModels();
            _countdowns.Clear();
            foreach (var countdown in countdowns)
            {
                _countdowns.Add(countdown);
            }

            IEnumerable<string> ids = await _calendarIdProvider.GetAllModels();
            _calendarIds.Clear();
            foreach (var id in ids)
            {
                _calendarIds.Add(id);
                this.GoogleCalendar?.AddCalendar(id);
            }
        }

        private void GoogleLoginAuthTokenReceived(object sender, (string authToken, string refreshToken) token)
        {
            if (token.refreshToken == null)
            {
                // User logs in again and no need to update the refresh token
                this.GoogleCalendar = new GoogleCalendar(token.authToken, false);
            }
            else
            {
                this.GoogleCalendar = new GoogleCalendar(token.authToken, true);
                AddUpdateAppSettings("GoogleRefreshToken", token.refreshToken);
            }

            if (SelectedCalendarId != null && SelectedCalendarId != "")
            {
                this.GoogleCalendar.SelectCalendar(SelectedCalendarId);
            }

            foreach (string id in CalendarIds)
            {
                this.GoogleCalendar.AddCalendar(id);
            }

            GoogleLoggedIn?.Invoke();
        }

        public bool IsUniqueShortcutName(string name)
        {
            return !_shortcuts.Any(s => s.Name == name);
        }

        public void SetWelcomeName(string name)
        {
            _welcomeName = name;
            AddUpdateAppSettings("WelcomeName", name);
        }

        public void SetAccentColor(Brush color)
        {
            ThemeManager.Current.AccentColor = color;
            AddUpdateAppSettings("AccentColor", color.ToString());
        }

        public async Task SetCalendarSelected(string id, bool isSelected)
        {
            if (this.GoogleCalendar == null)
            {
                throw new Exception("No Google Calendar available");
            }

            if (isSelected)
            {
                _calendarIds.Add(id);
                this.GoogleCalendar.AddCalendar(id);
                await _calendarIdCreator.CreateModel(id);
            }
            else
            {
                _calendarIds.Remove(id);
                this.GoogleCalendar.RemoveCalendar(id);
                await _calendarIdCreator.DeleteModel(id);
            }
        }

        public void LoadAccentColor()
        {
            string? acccentColor = ReadSetting("AccentColor");
            if (acccentColor != null && acccentColor != "")
            {
                ThemeManager.Current.AccentColor = (Brush)new BrushConverter().ConvertFrom(acccentColor);
            }
        }

        private async Task InitCalendarFromRefreshToken(string token)
        {
            this.GoogleLogin.RefreshAccessTokenAsync(token);
        }

        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public static string? ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key];
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            return null;
        }

    }
}
