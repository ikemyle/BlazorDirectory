using Microsoft.AspNetCore.Components;
using ShoppingDirectory.Services;
using ShoppingDirectory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingDirectory.Components;
using Microsoft.JSInterop;

namespace ShoppingDirectory.Pages
{
    public class ListBase : ComponentBase
    {
        private List<Store> allStores;

        #region Properties
        protected string Message { get; set; } = string.Empty;
        protected string StatusClass { get; set; } = string.Empty;
        protected bool ShowAlert { get; set; }

        [Inject]
        IToastService ToastService { get; set; }

        [Inject]
        public IJSRuntime jsRuntime { get; set; }

        [Inject]
        public IStoreCategoryDataService StoreCategoryDataService { get; set; }

        [Inject]
        public IStoreDataService StoreDataService { get; set; }

        public List<Store> Stores { get; set; } = new List<Store>();

        public IEnumerable<Category> Categories { get; set; }

        private string _categoryId;
        [Parameter]
        public string CategoryId
        {
            get { return _categoryId; }
            set
            {
                _categoryId = value;
                ReInitializeStoresWrapper();
            }
        }

        public Store SelectedStore { get; set; }

        protected StoreDialog StoreDialog { get; set; }

        protected ConfirmDialog ConfirmDialog { get; set; }
        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Categories = (await StoreCategoryDataService.GetAllStoreCategories()).ToList();
                await FilterStores();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void ConfirmDialog_OnDialogClose()
        {
            await StoreDataService.DeleteStore(this.SelectedStore.Id);
            Stores = (await StoreDataService.GetAllStores()).ToList();
            ToastService.ShowToast($"Store ''{this.SelectedStore.Name}'' has been deleted!", ToastLevel.Warning);
            //DisplayStatusAlert("alert-danger", $"Store ''{this.SelectedStore.Name}'' has been deleted!");

            await FilterStores();
            await ScrollToTop();
        }

        public async void AddStoreDialog_OnDialogClose()
        {
            string message = string.Empty;
            if (this.SelectedStore != null)
            {
                message = $"Store ''{this.SelectedStore.Name}'' has been updated!";
            }
            else
            {
                message = "A new Store has been created!";
            }
            ToastService.ShowToast(message, ToastLevel.Success);
            //DisplayStatusAlert("alert-success", message);
            await FilterStores();
        }

        protected void DeleteStore(Store store)
        {
            this.SelectedStore = store;
            ConfirmDialog.Show();
        }

        protected void EditStore(Store store)
        {
            this.SelectedStore = store;
            ShowStore(store);
        }

        protected void StoreAdd()
        {
            ShowStore(null);
        }

        protected void CloseAlert()
        {
            this.Message = string.Empty;
            this.ShowAlert = false;
            StateHasChanged();
        }

        protected async Task ScrollToTop()
        {
            await jsRuntime.InvokeVoidAsync("ScrollToTop");
        }
        #endregion

        #region Private Methods
        private void ShowStore(Store store)
        {
            this.SelectedStore = store;
            StoreDialog.Show();
        }
        private void DisplayStatusAlert(string status, string message)
        {
            this.StatusClass = status;
            this.Message = message;
            this.ShowAlert = true;
        }

        private async void ReInitializeStoresWrapper()
        {
            await FilterStores();
        }

        private async Task FilterStores()
        {
            allStores = (await StoreDataService.GetAllStores()).ToList();
            int.TryParse(this.CategoryId, out var categoryId);
            if (categoryId == 0)
            {
                this.Stores = allStores;
            }
            else
            {
                if (allStores != null)
                    this.Stores = allStores.Where(store => store.CategoryId == categoryId.ToString()).ToList();
            }
            this.SelectedStore = null;
            StateHasChanged();
        }

        #endregion
    }
}
