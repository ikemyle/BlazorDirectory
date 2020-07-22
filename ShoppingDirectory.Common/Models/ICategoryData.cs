using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingDirectory.Common.Models
{
    public interface ICategoryData
    {
        IEnumerable<Category> CategoryList();

        Category Update(Category UpdateCategory);

        Category Add(Category newCategory);

        Category Delete(int Id);

        string CategoryNameById(int Id);
    }
}
