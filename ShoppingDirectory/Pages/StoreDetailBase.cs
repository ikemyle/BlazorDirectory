using Microsoft.AspNetCore.Components;
using ShoppingDirectory.Common.Models;
using MapHelper = ShoppingDirectory.Libraries.Map;
using ShoppingDirectory.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingDirectory.Pages
{
    public class StoreDetailBase : ComponentBase
    {
        [Inject]
        public IStoreDataService StoreDataService { get; set; }

        [Parameter]
        public string StoreId { get; set; }

        public Store Store { get; set; }

        protected string Message = string.Empty;

        protected string StatusClass = string.Empty;

        public List<MapHelper.Marker> MapMarkers { get; set; } = new List<MapHelper.Marker>();

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(this.StoreId, out var storeId);
            this.Store = (await StoreDataService.GetStoreDetails(storeId));
            //hardcoded latitude. They should be poulated fromstore address
            double lat = 50.836025;
            double longit = 4.370705;
            MapMarkers = new List<MapHelper.Marker>
            {
                new MapHelper. Marker{Description = $"{this.Store.Name}",  ShowPopup = false, X = longit, Y = lat}
            };
        }

        protected async Task HandleValidSubmit()
        {
            if (this.Store.Id == 0)
            {
                var newStore = StoreDataService.AddStore(this.Store);
            }
            else
            {
                await StoreDataService.UpdateStore(this.Store);
            }
        }
        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }
    }
}