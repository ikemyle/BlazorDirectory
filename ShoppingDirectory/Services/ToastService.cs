using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ShoppingDirectory.Services
{
    public class ToastService : IToastService, IDisposable
    {
        public event Action<string, ToastLevel> OnShow;
        public event Action OnHide;
        private Timer Countdown;

        public int CountDownInterval { get; set; } = 5000;

        public void Dispose()
        {
            Countdown?.Dispose();
        }

        public void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void ShowToast(string message, ToastLevel level)
        {
            OnShow?.Invoke(message, level);
            StartCountdown();
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (Countdown.Enabled)
            {
                Countdown.Stop();
                Countdown.Start();
            }
            else
            {
                Countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (Countdown == null)
            {
                Countdown = new Timer(CountDownInterval);
                Countdown.Elapsed += HideToast;
                Countdown.AutoReset = false;
            }
        }
    }
}
