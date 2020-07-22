using ShoppingDirectory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingDirectory.Services
{
    public interface IStoreCategoryDataService
    {
        Task<IEnumerable<Category>> GetAllStoreCategories();
        Task<Category> GetStoreCategoryById(int storeCategoryId);
    }
}
