using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.BinEnquiry.Enquire
{
    public partial class EnquireView : ContentPage
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