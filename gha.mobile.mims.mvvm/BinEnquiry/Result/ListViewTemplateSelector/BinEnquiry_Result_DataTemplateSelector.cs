using gha.mobile.mims.entities;
using Xamarin.Forms;

namespace gha.mobile.mims.mvvm.BinEnquiry.Result.ListViewTemplateSelector
{
    public class BinEnquiry_Result_DataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LotTrackedTemplate { get; set; }
        public DataTemplate NonLotTrackedTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is BinPart binPart))
                return new DataTemplate();

            return binPart.ShowLot ? LotTrackedTemplate : NonLotTrackedTemplate;
        }
    }
}