using gha.mobile.mims.entities;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.PartEnquiry.Result.ListViewTemplateSelector
{
    public class PartEnquiry_ResultView_DataTemplateSelector : DataTemplateSelector
    {
        public static readonly BindableProperty LotTrackedProperty = BindableProperty.Create(
            nameof(LotTracked),
            typeof(bool),
            typeof(PartEnquiry_ResultView_DataTemplateSelector),
            null
        );

        public bool LotTracked { get; set; }

        public DataTemplate LotTrackedTemplate { get; set; }
        public DataTemplate NotLotTrackedTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is PartBin))
                return new DataTemplate();

            return LotTracked ? LotTrackedTemplate : NotLotTrackedTemplate;
        }
    }
}