namespace gha.mobile.mims.mvvm
{
    public partial class LoginView
    {
        private readonly LoginViewModel _viewModel;

        public LoginView(LoginViewModel viewModel)
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