using Microsoft.AspNetCore.Mvc;
using ShoppingDirectory.Common.Models;

namespace ShoppingDirectory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {
        private const string StoreValidateMessage = "Store name or category cannot be empty";
        private const string StoreValidateTitle = "Name/Category";
        private readonly IShoppingData _storeRepository;

        public StoreController(IShoppingData storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public IActionResult GetAllStores()
        {
            return Ok(_storeRepository.StoreList());
        }

        [HttpGet("{id}")]
        public IActionResult GetStoreById(int id)
        {
            return Ok(_storeRepository.StoreById(id));
        }

        [HttpPost]
        public IActionResult CreateStore([FromBody] Store store)
        {
            if (store == null)
                return BadRequest();

            if (Validate(store))
            {
                ModelState.AddModelError(StoreValidateTitle, StoreValidateMessage);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdStore = _storeRepository.Add(store);

            return Created("store", createdStore);
        }

        [HttpPut]
        public IActionResult UpdateStore([FromBody] Store store)
        {
            if (store == null)
                return BadRequest();

            if (Validate(store))
            {
                ModelState.AddModelError(StoreValidateTitle, StoreValidateMessage);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storeToUpdate = _storeRepository.StoreById(store.Id);

            if (storeToUpdate == null)
                return NotFound();

            _storeRepository.Update(store);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStore(int id)
        {
            if (id == 0)
                return BadRequest();

            var storeToDelete = _storeRepository.StoreById(id);
            if (storeToDelete == null)
                return NotFound();

            _storeRepository.Delete(id);

            return NoContent();//success
        }

        private bool Validate(Store store)
        {
            return string.IsNullOrEmpty(store.Name) || string.IsNullOrEmpty(store.CategoryId) || store.CategoryId == "0";
        }
    }
}
