using ShoppingDirectory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingDirectory.Common.Models
{
    public class InMemoryCategoryData : ICategoryData
    {
        readonly List<Category> categoryList;

        public InMemoryCategoryData()
        {
            categoryList = new List<Category>
            {
                new Category{Id=1, Name="Food Store" },
                new Category {Id=2, Name="Pharmacy"},
                new Category {Id=3,Name="Office Supply"},
                new Category {Id=4,Name="Apparel"}
            };
        }
        public Category Add(Category newCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> CategoryList()
        {
            return categoryList.OrderBy(x => x.Name).ToList();
        }

        public string CategoryNameById(int Id)
        {
            return categoryList.SingleOrDefault(x => x.Id == Id)?.Name;
        }

        public Category Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category UpdateCategory)
        {
            throw new NotImplementedException();
        }
    }
}
