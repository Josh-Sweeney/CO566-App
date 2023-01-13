using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using gha.mobile.mims.entities;
using gha.mobile.mims.repositories;

namespace gha.mobile.mims.mvvm.BinEnquiry.Result
{
    public class ResultViewModel : AppViewModel
    {
        private ObservableCollection<BinPart> _binParts;

        public ResultViewModel()
        {
            Task.Run(async () => await LoadData());
        }

        public ObservableCollection<BinPart> BinParts
        {
            get => _binParts;
            set
            {
                _binParts = value;
                RaisePropertyChanged("BinParts");
            }
        }

        public Bin Bin => TryGetProperty<Bin>("Bin");

        private async Task LoadData()
        {
            UserDialogs.Instance.ShowLoading("Retrieving Parts...");

            var binEnquiry = BinEnquiryFactory.Get();
            BinParts = new ObservableCollection<BinPart>(binEnquiry.Get(Bin.WarehouseCode, Bin.BinNum));

            UserDialogs.Instance.HideLoading();
        }
    }
}