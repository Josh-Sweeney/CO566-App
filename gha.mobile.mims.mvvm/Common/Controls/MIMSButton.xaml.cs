using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gha.mobile.mims.mvvm.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MIMSButton : Button
    {
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(
            propertyName: "HorizontalTextAlignment",
            returnType: typeof(TextAlignment),
            declaringType: typeof(MIMSButton),
            defaultValue: TextAlignment.Center
        );

        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        public MIMSButton()
        {
            InitializeComponent();
        }
    }
}