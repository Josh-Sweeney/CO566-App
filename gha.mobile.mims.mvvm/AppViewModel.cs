using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using gha.mobile.common.helpers;
using gha.mobile.common.mvvm;
using gha.mobile.common.repositories;
using gha.mobile.common.resolver;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace gha.mobile.mims.mvvm
{
    public abstract class AppViewModel : ObservableObject, INotifyPropertyChanged
    {
        protected readonly IMessageService _messageService;
        protected SettingsRepository _settingsRepository;
        
        public AppViewModel()
        {
            Application.Current.Resources["ForegroundColor"] = ForegroundColor;
            Application.Current.Resources["ForegroundColorContrast"] = ForegroundColorContrast;

            Application.Current.Resources["BackgroundColor"] = BackgroundColor;
            Application.Current.Resources["BackgroundColorContrast"] = BackgroundColorContrast;

            _settingsRepository = new SettingsRepository();
            _messageService = DependencyService.Get<IMessageService>();
        }

        protected AppViewModel(IMessageService messageService, SettingsRepository settingsRepository)
        {
            _messageService = messageService;
            _settingsRepository = settingsRepository;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Private Variables

        private bool _areButtonsEnabled = true;
        public bool _OKButtonEnabled = false;
        bool _IsBusy = false;

        #endregion

        #region Public Properties

        public string AppTitle => _settingsRepository.AppTitle;

        public string AppVersion => _settingsRepository.AppVersion;

        public int ID { get; set; }

        public string BaseAppTitle
        {
            get
            {
                if (!Application.Current.Properties.ContainsKey("AppTitle"))
                {
                    Application.Current.Properties["AppTitle"] = _settingsRepository.AppTitle;
                }

                return Application.Current.Properties["AppTitle"].ToString();
            }
        }

        public string BaseAppVersion
        {
            get
            {
                if (!Application.Current.Properties.ContainsKey("AppVersion"))
                {
                    Application.Current.Properties["AppVersion"] = _settingsRepository.AppVersion;
                }

                return Application.Current.Properties["AppVersion"].ToString();
            }
        }

        public Color BackgroundColor => Color.FromHex("#000000");
        //{
        //    get
        //    {
        //        if (!Application.Current.Properties.ContainsKey("BackgroundColor"))
        //        {
        //            Application.Current.Resources["BackgroundColor"] = Color.FromHex(_settingsRepository.BackColour);
        //            Application.Current.Properties["BackgroundColor"] = Color.FromHex(_settingsRepository.BackColour);
        //        }

        //        return (Color)Application.Current.Properties["BackgroundColor"];
        //    }
        //    set
        //    {
        //        Application.Current.Properties["BackgroundColor"] = value;
        //        _settingsRepository.BackColour = value.ToHex();
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackgroundColor"));
        //    }
        //}

        public Color ForegroundColor => Color.FromHex("#FFFFFF");// _settingsRepository.ForeColour ?? "#000000");
        //{
        //    get => 
        //    {
        //        if (!Application.Current.Properties.ContainsKey("ForegroundColor"))
        //        {
        //            Application.Current.Resources["ForegroundColor"] = Color.FromHex(_settingsRepository.ForeColour);
        //            Application.Current.Properties["ForegroundColor"] = Color.FromHex(_settingsRepository.ForeColour);
        //        }

        //        return (Color)Application.Current.Properties["ForegroundColor"];
        //    }
        //    set
        //    {
        //        Application.Current.Properties["ForegroundColor"] = value;
        //        _settingsRepository.ForeColour = value.ToHex();
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForegroundColor"));
        //    }
        //}

        public Color BackgroundColorContrast
        {
            get
            {
                var R = BackgroundColor.R > 0.5 ? BackgroundColor.R - 0.25 : BackgroundColor.R + 0.25;
                var G = BackgroundColor.G > 0.5 ? BackgroundColor.G - 0.25 : BackgroundColor.G + 0.25;
                var B = BackgroundColor.B > 0.5 ? BackgroundColor.B - 0.25 : BackgroundColor.B + 0.25;

                return new Color(R, G, B);
            }
        }

        public Color ForegroundColorContrast
        {
            get
            {
                var R = ForegroundColor.R > 0.5 ? ForegroundColor.R - 0.25 : ForegroundColor.R + 0.25;
                var G = ForegroundColor.G > 0.5 ? ForegroundColor.G - 0.25 : ForegroundColor.G + 0.25;
                var B = ForegroundColor.B > 0.5 ? ForegroundColor.B - 0.25 : ForegroundColor.B + 0.25;

                return new Color(R, G, B);
            }
        }

        public byte[] LogoSource
        {
            get
            {
                if (!Application.Current.Properties.ContainsKey("Logo"))
                {
                    Application.Current.Properties["Logo"] = _settingsRepository.Logo;
                }

                return (byte[])Application.Current.Properties["Logo"];
            }
            set
            {
                _settingsRepository.Logo = value;
                Application.Current.Properties["Logo"] = _settingsRepository.Logo;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LogoSource"));
            }
        }

        public string Employee
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("EmpName"))
                {
                    return string.Concat(Application.Current.Properties["EmpName"].ToString());
                }
                else
                    return string.Empty;
            }
        }

        public bool AreButtonsEnabled
        {
            get => _areButtonsEnabled;
            set
            {
                if (_areButtonsEnabled != value)
                {
                    _areButtonsEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AreButtonsEnabled"));
                }
            }
        }

        public bool OKButtonEnabled
        {
            get => _OKButtonEnabled;
            set
            {
                if (_OKButtonEnabled != value)
                {
                    _OKButtonEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OKButtonEnabled"));
                }
            }
        }

        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
        }

        public INavigation Navigation { get; set; }

        #endregion

        #region Public Commands

        public virtual ICommand HomeCommand => new Command(async () =>
        {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (Navigation.NavigationStack.Count > 1)
                    Navigation.RemovePage(page);
            }

            var mainMenuView = Resolver.Resolve<MainMenuView>();
            await Navigation.PushAsync(mainMenuView);
        });

        public ICommand SerialScanDoneCommand => new Command(() => { Navigation.PopAsync(); });

        #endregion

        #region Protected Helpers

        #region Messages

        public async Task ShowWarning(string message)
        {
            await _messageService.ShowAsync("Warning", message, "OK");
        }

        #endregion

        /// <summary>
        /// Navigates back to new instance of a previous page in the navigation stack
        /// removing all pages that came after it
        /// </summary>
        protected async Task NavigateBackToPage<T>() where T : Page
        {
            // Get all pages after the first instance of the requested page
            var pages = Navigation.NavigationStack
                .SkipWhile(r => !(r is T))
                .SkipLast(1)
                .ToList();

            // Remove all those pages
            foreach (var page in pages) Navigation.RemovePage(page);

            // Resolve a new instance of the requested page
            // and insert it before the current page
            Navigation.InsertPageBefore(Resolver.Resolve<T>(), Navigation.NavigationStack.Last());

            // Pop the current page
            await Navigation.PopAsync();
        }

        protected T TryGetProperty<T>(string key)
        {
            try
            {
                return (T)Application.Current.Properties[key];
            }
            catch
            {
                return default;
            }
        }

        public void RaisePropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected async Task<string> ScanBarcode()
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();

            IsBusy = true;
            AreButtonsEnabled = false;
            bool scanComplete = false;

            var scanPage = new ZXingScannerPage();
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushAsync(scanPage));

            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (scanComplete == false)
                    {
                        scanComplete = true;
                        await Navigation.PopAsync();
                    }
                });
                taskCompletionSource.SetResult(result.Text);
            };

            IsBusy = false;
            AreButtonsEnabled = true;

            return await taskCompletionSource.Task;
        }

        /// <summary>
        /// Navigates to a page, adding it to the navigation stack
        /// </summary>
        protected async Task NavigateTo<T>() where T : Page
        {
            AreButtonsEnabled = false;

            Logger.Log($"Navigating to {typeof(T)}");

            var newPage = Resolver.Resolve<T>();
            await Navigation.PushAsync(newPage);
            AreButtonsEnabled = true;
        }

        /// <summary>
        /// Navigates to a new page, clearing the navigation stack
        /// </summary>
        protected async Task NavigateToNewPage<T>() where T : Page
        {
            if (AreButtonsEnabled == false) return;
            AreButtonsEnabled = false;

            Logger.Log($"Clearing Navigation stack and navigating to {typeof(T)}");

            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var existingPage in existingPages)
            {
                if (Navigation.NavigationStack.Count > 1)
                    Navigation.RemovePage(existingPage);
            }

            var newPage = Resolver.Resolve<T>();
            await Navigation.PushAsync(newPage);
            AreButtonsEnabled = true;
        }

        #endregion
    }
}