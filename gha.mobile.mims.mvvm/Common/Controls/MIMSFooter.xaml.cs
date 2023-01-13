using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gha.mobile.mims.mvvm.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MIMSFooter : Grid
    {
        public MIMSFooter()
        {
            InitializeComponent();
        }

        protected override void OnAdded(View view)
        {
            base.OnAdded(view);

            ColumnDefinitions.Clear();

            for (int index = 0; index < Children.Count; index++)
            {
                ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                View child = Children[index];

                SetColumn(child, index);
            }
        }
    }
}