using Microsoft.AspNetCore.Components;
using ShoppingDirectory.Services;
using System;
using System.Threading.Tasks;

namespace ShoppingDirectory.Components
{
    public class ToastBase : ComponentBase
    {
        [Inject]
        IToastService ToastService { get; set; }

        protected string Heading { get; set; }
        protected string Message { get; set; }
        protected bool IsVisible { get; set; }
        protected string BackgroundCssClass { get; set; }
        protected string IconCssClass { get; set; }

        protected override void OnInitialized()
        {
            ToastService.OnShow += async (s, e) => await ShowToast(s, e);
            ToastService.OnHide += async () => await HideToast();
        }

        private async Task ShowToast<M, TL>(M message, TL toastLevel)
        {
            BuildToastSettings((ToastLevel)Enum.Parse(typeof(ToastLevel),toastLevel.ToString()), message.ToString());
            IsVisible = true;
            await InvokeAsync(() => StateHasChanged());
        }

        private async Task HideToast()
        {
            IsVisible = false;
            await InvokeAsync(() => StateHasChanged());
        }

        private void BuildToastSettings(ToastLevel level, string message)
        {
            switch (level)
            {
                case ToastLevel.Info:
                    BackgroundCssClass = "bg-info";
                    IconCssClass = "info";
                    Heading = "Info";
                    break;
                case ToastLevel.Success:
                    BackgroundCssClass = "bg-success";
                    IconCssClass = "check";
                    Heading = "Success";
                    break;
                case ToastLevel.Warning:
                    BackgroundCssClass = "bg-warning";
                    IconCssClass = "exclamation";
                    Heading = "Warning";
                    break;
                case ToastLevel.Error:
                    BackgroundCssClass = "bg-danger";
                    IconCssClass = "times";
                    Heading = "Error";
                    break;
            }

            Message = message;
        }

        public void Dispose()
        {
            ToastService.OnShow -= async (s, e) => await ShowToast(s, e);
        }
    }
}
