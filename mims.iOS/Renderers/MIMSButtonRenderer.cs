using gha.mobile.mims.mvvm.Controls;
using mims.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MIMSButton), typeof(MIMSButtonRenderer))]
namespace mims.iOS.Renderers
{
    public class MIMSButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null) { return; }

            UIControlContentHorizontalAlignment horizontalAlignment;
            UITextAlignment textAlignment;

            // We have to use ButtonRenderer, so cast the Element to MultilineButton to get the HorizontalTextAlignment property
            var button = (MIMSButton)Element;
            if (button == null) { return; }

            switch (button.HorizontalTextAlignment)
            {
                case TextAlignment.Center:
                    horizontalAlignment = UIControlContentHorizontalAlignment.Center;
                    textAlignment = UITextAlignment.Center;
                    break;
                case TextAlignment.End:
                    horizontalAlignment = UIControlContentHorizontalAlignment.Right;
                    textAlignment = UITextAlignment.Right;
                    break;
                default:
                    horizontalAlignment = UIControlContentHorizontalAlignment.Left;
                    textAlignment = UITextAlignment.Left;
                    break;
            }

            Control.HorizontalAlignment = horizontalAlignment;
            Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
            Control.TitleLabel.TextAlignment = textAlignment;
        }
    }
}