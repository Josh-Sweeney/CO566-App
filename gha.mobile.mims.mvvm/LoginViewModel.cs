using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using gha.mobile.common.helpers;
using gha.mobile.common.resolver;
using gha.mobile.mims.entities;
using gha.mobile.mims.repositories;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm
{
    public class LoginViewModel : AppViewModel
    {
        #region Properties

        #region Employee Picked

        private Employee _employeePicked;

        public Employee EmployeePicked
        {
            get => _employeePicked;
            set
            {
                _employeePicked = value;

                if (_employeePicked is null)
                    return;

                _sitePicked = null;
                RaisePropertyChanged("SitePicked");
                _sites = null;
                RaisePropertyChanged("Sites");

                var siteRepository = SitesFactory.Get();
                Sites = new ObservableCollection<Site>(siteRepository.GetSites(_employeePicked));

                RaisePropertyChanged("EmployeePicked");

                if (Sites.Count != 1)
                    return;

                _sitePicked = Sites.FirstOrDefault();
                RaisePropertyChanged("SitePicked");
            }
        }

        #endregion

        #region Employees

        private ObservableCollection<Employee> _employees;
        
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                RaisePropertyChanged("Employees");
            }
        }

        #endregion

        #region Sites

        private ObservableCollection<Site> _sites;

        public ObservableCollection<Site> Sites
        {
            get => _sites;
            set
            {
                _sites = value;
                RaisePropertyChanged("Sites");
            }
        }

        #endregion

        #region Site Picked

        private Site _sitePicked;

        public Site SitePicked
        {
            get => _sitePicked;
            set
            {
                _sitePicked = value;
                RaisePropertyChanged("SitePicked");
            }
        }

        #endregion

        #endregion

        public LoginViewModel()
        {
            Device.BeginInvokeOnMainThread(async () => await LoadPage());
        }

        #region Commands

        public ICommand LoginCommand => new Command(async () =>
        {
            if (!AreButtonsEnabled)
                return;

            Logger.Log("Select button clicked");

            if (EmployeePicked == null)
                return;

            if (SitePicked == null)
                return;

            AreButtonsEnabled = false;

            Application.Current.Properties["EmpID"] = EmployeePicked.EmployeeID;
            Application.Current.Properties["EmpName"] = EmployeePicked.Name;
            Application.Current.Properties["Employee"] = EmployeePicked;

            _settingsRepository.Plant = _sitePicked.SiteId;
            var mainMenuView = Resolver.Resolve<MainMenuView>();
            await Navigation.PushAsync(mainMenuView);
        });

        #endregion

        private async Task LoadPage()
        {
            Employees = new ObservableCollection<Employee>(await GetEmployees());
        }

        private async Task<List<Employee>> GetEmployees()
        {
            var response = EmployeesFactory.Get().GetEmployees();

            if (response.Success)
                return response.ReturnObj;

            await _messageService.ShowAsync("Epicor Error", response.ErrorMessage, "OK");
            return Enumerable.Empty<Employee>().ToList();
        }
    }
}