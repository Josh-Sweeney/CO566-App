namespace gha.mobile.mims.mvvm.PartEnquiry.Result
{
    public partial class ResultView
    {
        private readonly ResultViewModel _viewModel;

        public ResultView(ResultViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();

            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;

            DataTemplateSelector.LotTracked = _viewModel.Part.LotTracked;
        }

        protected override void OnAppearing()
        {
            _viewModel.AreButtonsEnabled = true;
        }
    }
}