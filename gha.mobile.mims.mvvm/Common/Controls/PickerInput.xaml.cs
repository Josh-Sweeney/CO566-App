using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gha.mobile.mims.mvvm.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerInput : ContentView
    {
        #region Constructors

        public PickerInput()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public object SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public ICommand SelectionChangedCommand
        {
            get { return (ICommand)GetValue(SelectionChangedCommandProperty); }
            set { SetValue(SelectionChangedCommandProperty, value); }
        }

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(
            propertyName: "Header",
            returnType: typeof(string),
            declaringType: typeof(PickerInput),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldvalue, newValue) =>
            {
                ((PickerInput)bindable).HeaderControl.Text = (string)newValue ?? string.Empty;
            }
        );

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            propertyName: "ItemsSource",
            returnType: typeof(IList),
            declaringType: typeof(PickerInput),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldvalue, newValue) =>
            {
                ((PickerInput)bindable).PickerControl.ItemsSource = (IList)newValue;
            }
        );

        public static readonly BindableProperty SelectionChangedCommandProperty = BindableProperty.Create(
            propertyName: "SelectionChangedCommand",
            returnType: typeof(ICommand),
            declaringType: typeof(PickerInput),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldvalue, newValue) =>
            {
                if (newValue != null)
                    ((PickerInput)bindable).SelectionChangedCommand = (ICommand)newValue;
            }
        );

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            propertyName: "SelectedItem",
            returnType: typeof(object),
            declaringType: typeof(PickerInput),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldvalue, newValue) =>
            {
                ((PickerInput)bindable).PickerControl.SelectedItem = newValue;
            }
        );

        public BindingBase ItemDisplayBinding
        {
            get => PickerControl.ItemDisplayBinding;
            set => PickerControl.ItemDisplayBinding = value;
        }

        #endregion

        #region User Control Events

        #endregion

        private void PickerControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selectedItem = ((Picker)sender).SelectedItem as object;
            if (selectedItem == null) return;

            SelectedItem = selectedItem;

            if (SelectionChangedCommand == null) return;

            SelectionChangedCommand.Execute(selectedItem);
        }
    }
}