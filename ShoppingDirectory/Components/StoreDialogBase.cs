using Microsoft.AspNetCore.Components;
using ShoppingDirectory.Common.Models;
using ShoppingDirectory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDirectory.Components
{
    public class StoreDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }

        [Parameter]
        public int StoreId { get; set; }

        public string Title { get; set; }

        private Store _store = new Store { Id = 1, CategoryId = "" };
        [Parameter]
        public Store Store
        {
            get
            {
                return _store;
            }
            set
            {
                _store = value;
                this.SetStoreDependednProperties();
            }
        }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public IStoreDataService StoreDataService { get; set; }

        [Inject]
        public IStoreCategoryDataService StoreCategoryDataService { get; set; }

        public IEnumerable<Category> StoreCategories { get; set; }

        protected string Message = string.Empty;

        protected string StatusClass = string.Empty;


        private void SetStoreDependednProperties()
        {
            if (this.Store != null)
            {
                this.Title = $"Details for {this.Store.Name}";
            }
            else
            {
                this.Store = new Store { Id = 0, CategoryId = "" };
                this.Title = "Add Store";
            }
        }
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

        protected async Task HandleValidSubmit()
        {
            if (this.Store.Id > 0)
            {
                await StoreDataService.UpdateStore(Store);
            }
            else
            {
                await StoreDataService.AddStore(Store);
            }
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                StoreCategories = (await StoreCategoryDataService.GetAllStoreCategories()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
