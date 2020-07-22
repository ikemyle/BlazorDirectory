using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingDirectory.Common.Models
{
    public class Store
    {
        public int Id { get; set; }
        [Required,]
        public string CategoryId { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "Store name is too long. Maximum length is 80 characters.")]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string Address { get; set; }
        [Required, StringLength(30)]
        public string City { get; set; }

        public string CategoryName
        {
            get
            {
                var categoryData = new InMemoryCategoryData();
                return categoryData.CategoryNameById(int.Parse(CategoryId));
            }
        }
    }
}
