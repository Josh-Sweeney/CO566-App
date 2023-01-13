using gha.mobile.common.entities;
using gha.mobile.mims.repositories.Setup;
using LicenseServerHelper;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm
{
    public class ConnectionViewModel : AppViewModel
    {
        #region Properties

        #region App Pool Host

        private string _appPoolHost;

        public string AppPoolHost
        {
            get => _appPoolHost;
            set
            {
                _appPoolHost = value;
                RaisePropertyChanged(nameof(AppPoolHost));
            }
        }

        #endregion

        #region App Pool Instance

        private string _appPoolInstance;

        public string AppPoolInstance
        {
            get => _appPoolInstance;
            set
            {
                _appPoolInstance = value;
                RaisePropertyChanged(nameof(AppPoolInstance));
            }
        }

        #endregion

        #region Company

        private string _company;

        public string Company
        {
            get => _company;
            set
            {
                _company = value;
                RaisePropertyChanged(nameof(Company));
            }
        }

        #endregion

        #region Username

        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        #endregion

        #region Password

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        #endregion

        #endregion

        #region Constructors

        public ConnectionViewModel()
        {
            AppPoolHost = _settingsRepository.ERPServer;
            AppPoolInstance = _settingsRepository.ERPPDatabase;
            Company = _settingsRepository.ERPCompany;
            Username = _settingsRepository.ERPUserName;
            Password = string.IsNullOrEmpty(_settingsRepository.ERPPassword) == false
                ? Secret.DecryptText(_settingsRepository.ERPPassword, ststen.outr())
                : string.Empty;
        }

        #endregion

        public ICommand ConnectCommand => new Command(async () => await Login());

        public async Task Login()
        {
            // Attempt the connection
            if (await ValidateConnection() == false)
                return;
            
            _settingsRepository.ERPServer = AppPoolHost;
            _settingsRepository.ERPPDatabase = AppPoolInstance;
            _settingsRepository.ERPCompany = Company;
            _settingsRepository.ERPUserName = Username;
            _settingsRepository.ERPPassword = Secret.EncryptText(Password, ststen.outr());

            await NavigateTo<LoginView>();
        }

        private async Task<bool> ValidateConnection()
        {
            var erpConnection = new ERPConnection()
            {
                AppPoolHost = AppPoolHost,
                AppPoolInstance = AppPoolInstance,
                Company = Company,
                UserName = Username,
                Password = Secret.EncryptText(Password, ststen.outr())
            };
            
            var connectionTest = SetupFactory.Get(_settingsRepository.ERP).TestConnection(erpConnection);
            
            if (connectionTest.Success == false)
                await _messageService.ShowAsync("Connection Failed!", connectionTest.ErrorMessage, "OK");

            return connectionTest.Success;
        }
    }
}