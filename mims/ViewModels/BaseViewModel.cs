using System.ComponentModel;
using System.Threading.Tasks;
using gha.mobile.common.entities;
using gha.mobile.common.repositories;
using Xamarin.Forms;

namespace mims.ViewModels
{
    //public abstract class BaseViewModel : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected SettingsRepository settingsRepository = new SettingsRepository();

    //    public void RaisePropertyChanged(params string[] propertyNames)
    //    {
    //        foreach (var propertyName in propertyNames)
    //        {
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }

    //    public string AppTitle { get { return settingsRepository.AppTitle; } }
    //    public string AppVersion { get { return settingsRepository.AppVersion; } }
    //    public int ID { get; set; }

    //    public string BaseAppTitle
    //    {
    //        get
    //        {
    //            if (!Application.Current.Properties.ContainsKey("AppTitle"))
    //            {
    //                Application.Current.Properties["AppTitle"] = settingsRepository.AppTitle;
    //            }
    //            return Application.Current.Properties["AppTitle"].ToString();
    //        }
    //    }

    //    public string BaseAppVersion
    //    {
    //        get
    //        {
    //            if (!Application.Current.Properties.ContainsKey("AppVersion"))
    //            {
    //                Application.Current.Properties["AppVersion"] = settingsRepository.AppVersion;
    //            }
    //            return Application.Current.Properties["AppVersion"].ToString();
    //        }
    //    }

    //    public Color BackgroundColor
    //    {
    //        get
    //        {
    //            if (!Application.Current.Properties.ContainsKey("BackgroundColor"))
    //            {
    //                Application.Current.Properties["BackgroundColor"] = Color.FromHex(settingsRepository.BackColour);
    //            }
    //            return (Color)Application.Current.Properties["BackgroundColor"];
    //        }
    //        set
    //        {
    //            Application.Current.Properties["BackgroundColor"] = value;
    //            settingsRepository.BackColour = value.ToHex();
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BackgroundColor"));
    //        }
    //    }

    //    public Color ForegroundColor
    //    {
    //        get
    //        {
    //            if (!Application.Current.Properties.ContainsKey("ForegroundColor"))
    //            {
    //                Application.Current.Properties["ForegroundColor"] = Color.FromHex(settingsRepository.ForeColour);
    //            }
    //            return (Color)Application.Current.Properties["ForegroundColor"];
    //        }
    //        set
    //        {
    //            Application.Current.Properties["ForegroundColor"] = value;
    //            settingsRepository.ForeColour = value.ToHex();
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForegroundColor"));
    //        }
    //    }

    //    public byte[] LogoSource
    //    {
    //        get
    //        {
    //            if (!Application.Current.Properties.ContainsKey("Logo"))
    //            {
    //                Application.Current.Properties["Logo"] = settingsRepository.Logo;
    //            }
    //            return (byte[])Application.Current.Properties["Logo"];
    //        }
    //        set
    //        {
    //            settingsRepository.Logo = value;
    //            Application.Current.Properties["Logo"] = settingsRepository.Logo;
    //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LogoSource"));
    //        }
    //    }

    //    private bool _areButtonsEnabled = true;

    //    public bool AreButtonsEnabled
    //    {
    //        get => _areButtonsEnabled;
    //        set
    //        {
    //            if (_areButtonsEnabled != value)
    //            {
    //                _areButtonsEnabled = value;
    //                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AreButtonsEnabled"));
    //            }
    //        }
    //    }

    //    public bool _OKButtonEnabled = false;

    //    public bool OKButtonEnabled
    //    {
    //        get => _OKButtonEnabled;
    //        set
    //        {
    //            if (_OKButtonEnabled != value)
    //            {
    //                _OKButtonEnabled = value;
    //                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OKButtonEnabled"));
    //            }
    //        }
    //    }

    //    bool _IsBusy = false;

    //    public bool IsBusy
    //    {
    //        get
    //        {
    //            return _IsBusy;
    //        }
    //        set
    //        {
    //            if (_IsBusy != value)
    //            {
    //                _IsBusy = value;
    //                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBusy"));
    //            }
    //        }
    //    }
    //    public INavigation Navigation { get; set; }
    //}
}
