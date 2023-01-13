using Xamarin.Forms.Xaml;

namespace gha.mobile.common.mvvm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourceDictionary : Xamarin.Forms.ResourceDictionary
    {
        public ResourceDictionary()
        {
            InitializeComponent();
        }
    }
}