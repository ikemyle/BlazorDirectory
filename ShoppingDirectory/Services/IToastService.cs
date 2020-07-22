using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ShoppingDirectory.Services
{
    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }

    public interface IToastService
    {
        event Action<string, ToastLevel> OnShow;
        event Action OnHide;
        void ShowToast(string message, ToastLevel level);
        void HideToast(object source, ElapsedEventArgs args);
    }
}
