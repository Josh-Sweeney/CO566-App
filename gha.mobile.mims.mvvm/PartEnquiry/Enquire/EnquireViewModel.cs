using System.Threading.Tasks;
using System.Windows.Input;
using gha.mobile.common.helpers;
using gha.mobile.common.resolver;
using gha.mobile.mims.entities;
using gha.mobile.mims.mvvm.PartEnquiry.Result;
using gha.mobile.mims.repositories;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.PartEnquiry.Enquire
{
    public class EnquireViewModel : AppViewModel
    {
        private bool _isPartNumEntryFocused;

        private Part _part;
        private string _partNumber;

        public bool IsPartNumEntryFocused
        {
            get => _isPartNumEntryFocused;
            set
            {
                _isPartNumEntryFocused = value;
                RaisePropertyChanged(nameof(_isPartNumEntryFocused));
            }
        }

        public string PartNumber
        {
            get => _partNumber;
            set
            {
                _partNumber = value;
                RaisePropertyChanged("PartNumber");
            }
        }


        public ICommand SelectCommand => new Command(async () =>
        {
            if (!AreButtonsEnabled)
                return;

            Logger.Log("Select button clicked");
            AreButtonsEnabled = false;

            if (string.IsNullOrEmpty(_partNumber))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "You need to enter a part number", "OK");
                AreButtonsEnabled = true;
                return;
            }

            await Select();
        });

        public ICommand ScanCommand => new Command(async () =>
        {
            var barcode = await ScanBarcode();

            if (string.IsNullOrEmpty(barcode))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "You need to scan a barcode", "OK");
                return;
            }

            PartNumber = barcode;
        });

        public virtual async Task Select()
        {

            _part = PartValidationFactory.Get().Get(_partNumber);

            if (string.IsNullOrEmpty(_part.PartNum))
            {
                await _messageService.ShowAsync("Warning", "Invalid Part Number, please try again", "OK");
                AreButtonsEnabled = true;
                return;
            }

            Application.Current.Properties["Part"] = _part;
            var resultView = Resolver.Resolve<ResultView>();
            await Navigation.PushAsync(resultView);
        }
    }
}