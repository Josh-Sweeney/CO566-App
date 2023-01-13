using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gha.mobile.mims.mvvm.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryInput : ContentView
    {
        #region Bindable Properties

        public static readonly BindableProperty HeaderProperty = BindableProperty.Create(nameof(Header), typeof(string), typeof(EntryInput), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(nameof(EntryText), typeof(string), typeof(EntryInput), string.Empty, BindingMode.TwoWay);
        public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(EntryInput), true, BindingMode.TwoWay);
        public static readonly BindableProperty EntryKeyboardProperty = BindableProperty.Create(nameof(EntryKeyboard), typeof(Keyboard), typeof(EntryInput), null, BindingMode.TwoWay);

        #region IsPassword

        public bool IsPassword
        {
            get => Entry.IsPassword;
            set => Entry.IsPassword = value;
        }

        #endregion
        
        #endregion

        #region Properties

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public string EntryText
        {
            get { return (string)GetValue(EntryTextProperty); }
            set { SetValue(EntryTextProperty, value); }
        }

        public new bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public Keyboard EntryKeyboard
        {
            get { return (Keyboard)GetValue(EntryKeyboardProperty); }
            set { SetValue(EntryKeyboardProperty, value); }
        }

        #endregion

        public EntryInput()
        {
            InitializeComponent();
        }
    }
}