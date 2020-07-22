using ShoppingDirectory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingDirectory.Services
{
    public interface IStoreDataService
    {
        Task<IEnumerable<Store>> GetAllStores();
        Task<Store> GetStoreDetails(int storeId);
        Task<Store> AddStore(Store store);
        Task UpdateStore(Store store);
        Task DeleteStore(int storeId);
    }
}
