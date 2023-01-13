using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using gha.mobile.common.helpers;
using gha.mobile.common.resolver;
using gha.mobile.mims.entities;
using gha.mobile.mims.mvvm.BinEnquiry.Result;
using gha.mobile.mims.repositories;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.BinEnquiry.Enquire
{
    public class EnquireViewModel : AppViewModel
    {
        public EnquireViewModel()
        {
            LoadData();
        }

        public ICommand SelectCommand => new Command(async () =>
        {
            if (AreScanButtonsEnabled)
            {
                Logger.Log("Select button clicked");
                AreScanButtonsEnabled = false;
                await Select();
            }
        });

        public ICommand ScanCommand => new Command(async () =>
        {
            var barcode = await ScanBarcode();

            if (string.IsNullOrEmpty(barcode))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "You need to scan a barcode", "OK");
                return;
            }

            if (barcode.Contains('~') == false)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "You need to scan a barcode with a '~' delimiter", "OK");
                return;
            }

            var barcodeParts = barcode.Split('~');
            
            Warehouse = Warehouses.FirstOrDefault(x => x.WarehouseCode == barcodeParts[0]);
            Bin = Bins.FirstOrDefault(x => x.BinNum == barcodeParts[1]);
        });

        public void LoadData()
        {
            Warehouses = new ObservableCollection<Warehouse>(WarehousesFactory.Get().Get());
            Bins = new ObservableCollection<Bin>();
        }

        private void PopulateBins()
        {
            if (_warehouse == null) 
                _bins = new ObservableCollection<Bin>();

            UserDialogs.Instance.ShowLoading("Loading Bins...");

            Bins = new ObservableCollection<Bin>(BinsFactory.Get().Get(_warehouse));

            UserDialogs.Instance.HideLoading();
        }

        public async Task Select()
        {
            if (Warehouse is null)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "You need to enter a bin", "OK");
                return;
            }

            if (Bin is null)
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "You need to enter a bin", "OK");
                return;
            }
            Application.Current.Properties["Bin"] = _bin;
            AreScanButtonsEnabled = true;
            var resultView = Resolver.Resolve<ResultView>();
            await Navigation.PushAsync(resultView);
        }

        #region Warehouse

        private Warehouse _warehouse;

        public Warehouse Warehouse
        {
            get => _warehouse;
            set
            {
                if (value == null)
                    return;

                _warehouse = value;
                AreScanButtonsEnabled = true;

                Bin = null!;

                PopulateBins();
                AreBinsEnabled = true;
                RaisePropertyChanged(nameof(Warehouse));
            }
        }

        #endregion

        #region Warehouses

        private ObservableCollection<Warehouse> _warehouses { get; set; }

        public ObservableCollection<Warehouse> Warehouses
        {
            get => _warehouses;
            set => _warehouses = value;
        }

        #endregion

        #region Bin

        private Bin _bin;

        public Bin Bin
        {
            get => _bin;
            set
            {
                _bin = value;
                AreScanButtonsEnabled = true;
                RaisePropertyChanged("Bin");
            }
        }

        #endregion

        #region Bins

        private ObservableCollection<Bin> _bins { get; set; }

        public ObservableCollection<Bin> Bins
        {
            get => _bins;
            set
            {
                _bins = value;
                RaisePropertyChanged("Bins");
            }
        }

        #endregion

        #region AreBinsEnabled

        private bool _areBinsEnabled;

        public bool AreBinsEnabled
        {
            get => _areBinsEnabled;
            set
            {
                if (_areBinsEnabled != value)
                {
                    _areBinsEnabled = value;
                    RaisePropertyChanged("AreBinsEnabled");
                }
            }
        }

        #endregion

        #region AreScanButtonsEnabled

        private bool _areScanButtonsEnabled;

        public bool AreScanButtonsEnabled
        {
            get => _areScanButtonsEnabled;
            set
            {
                if (_areScanButtonsEnabled != value)
                {
                    _areScanButtonsEnabled = value;
                    RaisePropertyChanged("AreScanButtonsEnabled");
                }
            }
        }

        #endregion

        #region IsWarehousePickerFocused

        private bool _isWarehousePickerFocused;

        public bool IsWarehousePickerFocused
        {
            get => _isWarehousePickerFocused;
            set
            {
                _isWarehousePickerFocused = value;
                RaisePropertyChanged(nameof(IsWarehousePickerFocused));
            }
        }

        #endregion

        #region IsBinPickerFocused

        private bool _isBinPickerFocused;

        public bool IsBinPickerFocused
        {
            get => _isBinPickerFocused;
            set
            {
                _isBinPickerFocused = value;
                RaisePropertyChanged(nameof(IsBinPickerFocused));
            }
        }

        #endregion
    }
}