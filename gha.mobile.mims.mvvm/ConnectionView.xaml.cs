using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace gha.mobile.mims.mvvm
{
    public partial class ConnectionView : ContentPage
    {
        private ConnectionViewModel _viewModel;

        public ConnectionView(ConnectionViewModel viewModel)
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
