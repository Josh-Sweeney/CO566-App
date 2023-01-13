using System;
using System.Collections.Generic;
using gha.mobile.common.helpers;
using mims.ViewModels;
using Xamarin.Forms;

namespace mims.Views
{
    public partial class MainView : ContentPage
    {
        private MainViewModel _viewModel;

        public MainView(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            //viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            //_viewModel.AreButtonsEnabled = true;
        }
    }
}
