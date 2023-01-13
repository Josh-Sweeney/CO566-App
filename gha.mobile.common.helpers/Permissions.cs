using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace gha.mobile.common.helpers
{
    public static class Permissions
    {
        public static async Task<PermissionStatus> CheckPermissions(Permission permission)
        {
            PermissionStatus permissionStatus = PermissionStatus.Unknown;
            if (permission == Permission.Calendar) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CalendarPermission>();
            if (permission == Permission.Camera) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
            if (permission == Permission.Contacts) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<ContactsPermission>();
            if (permission == Permission.Location) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
            if (permission == Permission.LocationAlways) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationAlwaysPermission>();
            if (permission == Permission.LocationWhenInUse) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>();
            if (permission == Permission.MediaLibrary) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<MediaLibraryPermission>();
            if (permission == Permission.Microphone) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<MicrophonePermission>();
            if (permission == Permission.Phone) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<PhotosPermission>();
            if (permission == Permission.Photos) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<PhotosPermission>();
            if (permission == Permission.Reminders) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<RemindersPermission>();
            if (permission == Permission.Sensors) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<SensorsPermission>();
            if (permission == Permission.Sms) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<SmsPermission>();
            if (permission == Permission.Speech) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<SpeechPermission>();
            if (permission == Permission.Storage) permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {

                    var title = $"{permission} Permission";
                    var question = $"To use this application the {permission} permission is required. Please go into Settings and turn on {permission} for the app.";
                    var positive = "Settings";
                    var negative = "Maybe Later";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return permissionStatus;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return permissionStatus;
                }

                request = true;

            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                PermissionStatus newStatus = PermissionStatus.Unknown;
                if (permission == Permission.Calendar) newStatus = await CrossPermissions.Current.RequestPermissionAsync<CalendarPermission>();
                if (permission == Permission.Camera) newStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
                if (permission == Permission.Contacts) newStatus = await CrossPermissions.Current.RequestPermissionAsync<ContactsPermission>();
                if (permission == Permission.Location) newStatus = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                if (permission == Permission.LocationAlways) newStatus = await CrossPermissions.Current.RequestPermissionAsync<LocationAlwaysPermission>();
                if (permission == Permission.LocationWhenInUse) newStatus = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
                if (permission == Permission.MediaLibrary) newStatus = await CrossPermissions.Current.RequestPermissionAsync<MediaLibraryPermission>();
                if (permission == Permission.Microphone) newStatus = await CrossPermissions.Current.RequestPermissionAsync<MicrophonePermission>();
                if (permission == Permission.Phone) newStatus = await CrossPermissions.Current.RequestPermissionAsync<PhotosPermission>();
                if (permission == Permission.Photos) newStatus = await CrossPermissions.Current.RequestPermissionAsync<PhotosPermission>();
                if (permission == Permission.Reminders) newStatus = await CrossPermissions.Current.RequestPermissionAsync<RemindersPermission>();
                if (permission == Permission.Sensors) newStatus = await CrossPermissions.Current.RequestPermissionAsync<SensorsPermission>();
                if (permission == Permission.Sms) newStatus = await CrossPermissions.Current.RequestPermissionAsync<SmsPermission>();
                if (permission == Permission.Speech) newStatus = await CrossPermissions.Current.RequestPermissionAsync<SpeechPermission>();
                if (permission == Permission.Storage) newStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();

                if (newStatus != PermissionStatus.Granted)
                {
                    permissionStatus = newStatus;
                    var title = $"{permission} Permission";
                    var question = $"To use the application the {permission} permission is required.";
                    var positive = "Settings";
                    var negative = "Maybe Later";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return permissionStatus;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return permissionStatus;
                }
            }

            return permissionStatus;
        }
    }
}
