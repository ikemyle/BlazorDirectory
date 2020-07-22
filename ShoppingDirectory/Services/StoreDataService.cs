using ShoppingDirectory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingDirectory.Services
{
    public class StoreDataService : IStoreDataService
    {
        private readonly HttpClient _httpClient;

        public StoreDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Store> AddStore(Store store)
        {
            var storeJson =
                new StringContent(JsonSerializer.Serialize(store), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/store", storeJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Store>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteStore(int storeId)
        {
            await _httpClient.DeleteAsync($"api/store/{storeId}");
        }

        public async Task<IEnumerable<Store>> GetAllStores()
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/store");
                return await JsonSerializer.DeserializeAsync<IEnumerable<Store>>
                    (response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Store> GetStoreDetails(int storeId)
        {
            var response = await _httpClient.GetStreamAsync($"api/store/{storeId.ToString()}");
            return await JsonSerializer.DeserializeAsync<Store>
                (response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateStore(Store store)
        {
            var storeJson = new StringContent(JsonSerializer.Serialize(store), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/store", storeJson);
        }
    }
}
