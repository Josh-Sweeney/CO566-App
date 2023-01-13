using gha.mobile.mims.mvvm.PartEnquiry.Enquire;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.PartEnquiry
{
    public partial class EnquireView
    {
        private EnquireViewModel _viewModel;

        public EnquireView(EnquireViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.AreButtonsEnabled = true;
        }
    }
}