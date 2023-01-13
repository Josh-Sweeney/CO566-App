using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using gha.mobile.mims.entities;
using gha.mobile.mims.repositories;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.PartEnquiry.Result
{
    public class ResultViewModel : AppViewModel
    {
        public ResultViewModel()
        {
            Device.BeginInvokeOnMainThread(async () => { await LoadData(); });
        }

        private ObservableCollection<PartBin> _partBins { get; set; }

        public ObservableCollection<PartBin> PartBins
        {
            get => _partBins;
            set
            {
                _partBins = value;
                RaisePropertyChanged("PartBins");
            }
        }

        public Part Part => TryGetProperty<Part>("Part");

        private Task LoadData()
        {
            using (UserDialogs.Instance.Loading())
            {
                IsBusy = true;

                var partEnquiry = PartEnquiryFactory.Get();

                PartBins = new ObservableCollection<PartBin>(partEnquiry.Get(Part, null));


                IsBusy = false;
            }

            return Task.CompletedTask;
        }
    }
}