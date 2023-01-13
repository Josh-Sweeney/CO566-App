using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace gha.mobile.common.mvvm
{
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string title, string message, string button)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, button);
        }

        public async Task<bool> ShowAsync(string title, string message, string button, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, button, cancel);
        }
        public async Task<bool> ShowDialog(string title, string message, string acceptText, string cancelText)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, acceptText, cancelText);
        }

        public async Task<string> ShowInputDialog(string title, string message, string acceptText, string cancelText, Keyboard keyboardType)
        {
            return await Application.Current.MainPage.DisplayPromptAsync(title, message, acceptText, cancelText, keyboard: keyboardType);
        }
    }
}
