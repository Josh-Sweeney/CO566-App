using System.Windows.Input;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm
{
    public class MainMenuViewModel : AppViewModel
    {
        #region Public Commands

        public ICommand PartEnquiryCommand => new Command(async () => await NavigateTo<PartEnquiry.EnquireView>());
        
        public ICommand BinEnquiryCommand => new Command(async () => await NavigateTo<BinEnquiry.Enquire.EnquireView>());

        public ICommand LogoutCommand => new Command(async () =>
        {
            if (Application.Current.Properties.ContainsKey("EmpID"))
                Application.Current.Properties.Remove("EmpID");

            if (Application.Current.Properties.ContainsKey("EmpName"))
                Application.Current.Properties.Remove("EmpName");
            
            await NavigateToNewPage<LoginView>();
        });

        #endregion
    }
}