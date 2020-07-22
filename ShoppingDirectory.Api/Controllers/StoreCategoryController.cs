using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingDirectory.Common.Models;

namespace ShoppingDirectory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreCategoryController : Controller
    {
        private readonly ICategoryData _storeCategoryRepository;

        public StoreCategoryController(ICategoryData storeCategoryRepository)
        {
            _storeCategoryRepository = storeCategoryRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetStoreCategories()
        {
            return Ok(_storeCategoryRepository.CategoryList());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetStoreCategoryById(int id)
        {
            return Ok(_storeCategoryRepository.CategoryNameById(id));
        }
    }
}
