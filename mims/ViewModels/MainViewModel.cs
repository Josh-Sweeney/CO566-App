using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using gha.mobile.common.entities;
using gha.mobile.common.helpers;
using gha.mobile.common.mvvm;
using gha.mobile.common.resolver;
using gha.mobile.mims.mvvm;
using mims.Entities;
using Xamarin.Forms;

namespace mims.ViewModels
{
    public class MainViewModel //: BaseViewModel
    {
        //public string AppVersionText
        //{
        //    get
        //    {
        //        return $"Welcome to the {AppSettings.LongAppTitle} Demo.\n\nClick the button below to start.";
        //    }
        //}

        //public MainViewModel()
        //{
        //    Messg messg = new Messg();

        //    MessagingCenter.Subscribe<Messg>(this, "FromCommon", (messg) =>
        //    {
        //        Logger.Log("MainMenuView subscribed");

        //        OpenMainMenuView();
        //    });
        //}

        //public void OpenMainMenuView()
        //{
        //    //var license = Task.Run(async () => await new Licensing().Process(AppSettings.AppVersion, AppSettings.AppLicensekey, AppSettings.AppTitle)).Result;

        //    Logger.Log("OpenMainMenuView()");
        //    var mainMenuView = Resolver.Resolve<MainMenuView>();
        //    Logger.Log("MainMenuView resolved");

        //    Navigation.PushAsync(mainMenuView);

        //    //App.Current.MainPage = new NavigationPage(mainMenuView)
        //    //{
        //    //    BarBackgroundColor = Color.FromHex(license.BackgroundColor),
        //    //    BarTextColor = Color.FromHex(license.ForegroundColor)
        //    //};

        //    Logger.Log("MainMenuView pushed");
        //}

        //public ICommand DemoStartUp => new Command(async () =>
        //{
        //    if (AreButtonsEnabled)
        //    {
        //        Logger.Log("Demo button clicked");
        //        AreButtonsEnabled = false;

        //        var existingPages = Navigation.NavigationStack.ToList();
        //        foreach (var page in existingPages)
        //        {
        //            if (Navigation.NavigationStack.Count > 1)
        //                Navigation.RemovePage(page);
        //        }
        //        //var demoStartUpView = Resolver.Resolve<DemoStartUpView>();
        //        //await Navigation.PushAsync(demoStartUpView);
        //    }
        //});
    }
}
