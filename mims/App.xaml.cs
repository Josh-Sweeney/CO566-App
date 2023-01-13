using gha.mobile.common.mvvm;
using gha.mobile.common.resolver;
using gha.mobile.mims.mvvm;
using mims.Entities;
using Xamarin.Forms;

namespace mims
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<IMessageService, MessageService>();

            InitializeComponent();

            var version = AppSettings.AppVersion;

            MainPage = new NavigationPage(Resolver.Resolve<ConnectionView>())
            {
                BarBackgroundColor = Color.FromHex(AppSettings.BackgroundColour),
                BarTextColor = Color.FromHex(AppSettings.ForegroundColour)
            };
        }
    }
}
