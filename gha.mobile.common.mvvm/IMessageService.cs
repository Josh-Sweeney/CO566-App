using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace gha.mobile.common.mvvm
{
    public interface IMessageService
    {
        Task ShowAsync(string title, string message, string button);

        Task<bool> ShowAsync(string title, string message, string button, string cancel);

        Task<bool> ShowDialog(string title, string message, string acceptText, string cancelText);

        Task<string> ShowInputDialog(string title, string message, string acceptText, string cancelText, Keyboard keyboardType);
    }
}

