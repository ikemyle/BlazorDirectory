using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDirectory.Components
{
    public class ConfirmDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public void Show()
        {
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async void Confirm()
        {
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
