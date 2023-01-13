using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.Controls.Toolbar
{
    public class HomeToolbarItem : ToolbarItem
    {
        public HomeToolbarItem()
        {
            Order = ToolbarItemOrder.Primary;
            Priority = 0;
            IconImageSource = new FontImageSource
            {
                FontFamily = Device.RuntimePlatform switch
                {
                    "iOS" => "Material Design Icons",
                    "Android" => "materialdesignicons-webfont.ttf#Material Design Icons",
                    _ => "Material Design Icons"
                },
                Size = 22,
                Glyph = "\U000f02dc",
                Color = Application.Current.Resources["ForegroundColor"] is Color
                    ? (Color)Application.Current.Resources["ForegroundColor"]
                    : default
            };
        }
    }
}