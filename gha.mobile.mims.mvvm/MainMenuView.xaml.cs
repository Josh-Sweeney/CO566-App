using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm
{
    public partial class MainMenuView : ContentPage
    {
        private MainMenuViewModel _viewModel;

        public MainMenuView(MainMenuViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.AreButtonsEnabled = true;

            if (Application.Current.Properties.ContainsKey("Part"))
                Application.Current.Properties.Remove("Part");
            if (Application.Current.Properties.ContainsKey("PartLot"))
                Application.Current.Properties.Remove("PartLot");
            if (Application.Current.Properties.ContainsKey("PartSerial"))
                Application.Current.Properties.Remove("PartSerial");
            if (Application.Current.Properties.ContainsKey("FromBin"))
                Application.Current.Properties.Remove("FromBin");
            if (Application.Current.Properties.ContainsKey("ToBin"))
                Application.Current.Properties.Remove("ToBin");
            if (Application.Current.Properties.ContainsKey("BinParts"))
                Application.Current.Properties.Remove("BinParts");
            if (Application.Current.Properties.ContainsKey("PartBins"))
                Application.Current.Properties.Remove("PartBins");
            if (Application.Current.Properties.ContainsKey("BinPartSerial"))
                Application.Current.Properties.Remove("BinPartSerial");
            if (Application.Current.Properties.ContainsKey("SavedSerials"))
                Application.Current.Properties.Remove("SavedSerials");
        }
    }
}