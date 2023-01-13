using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.BinEnquiry.Result
{
    public partial class ResultView : ContentPage
    {
        private readonly ResultViewModel _viewModel;

        public ResultView(ResultViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.AreButtonsEnabled = true;
        }
    }
}