using ShoppingDirectory.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingDirectory.Services
{
    public class StoreCategoryDataService : IStoreCategoryDataService
    {
        private readonly HttpClient _httpClient;

        public StoreCategoryDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Category>> GetAllStoreCategories()
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/storecategory");
                return await JsonSerializer.DeserializeAsync<IEnumerable<Category>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> GetStoreCategoryById(int storeCategoryId)
        {
            return await JsonSerializer.DeserializeAsync<Category>
                (await _httpClient.GetStreamAsync($"api/storecategory/{storeCategoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
