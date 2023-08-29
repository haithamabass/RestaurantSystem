using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Dtos.Menu;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.InterFaces;
using System.Data;

namespace RestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuServices _menuServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IDishImageServices _dishImageServices;

        public MenuController(IMenuServices menuServices, ICategoryServices categoryServices)
        {
            _menuServices = menuServices;
            _categoryServices = categoryServices;
        }


        #region GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<MenuDto>>> GetAllItems()
        {
            var items = await _menuServices.GetAll();

            if (items is null || !items.Any())
                return NoContent();

            return Ok(items);

        }
        #endregion

        #region GetItemById Endpoint
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {

            var product = await _menuServices.GetByIdDto(id);

            if (product == null)
            {
                return NotFound($"no Product with {id} was found");
            }

            return Ok(product);
        }
        #endregion

        #region GetItemByName Endpoint
        // GET: api/Products/5
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Menu>> GetItemByName(string name)
        {

            var item = await _menuServices.GetByNameDto(name);

            if (item == null)
            {
                return NotFound($"no item with {name} was found");
            }

            return Ok(item);
        }
        #endregion

        #region GetItemsByCategory Endpoint

        [HttpGet("categoryId/{categoryId}")]
        public async Task<ActionResult<MenuDto>> GetItemsByCategory(int categoryId )
        {

            var items = await _menuServices.GetAllByCategory(categoryId);

            if (items == null)
            {
                return NotFound($"no Product with {categoryId} was found");
            }

            return Ok(items);
        }
        #endregion

        #region AddItemToMenu
        [HttpPost]
        public async Task<IActionResult> AddItemToMenu([FromQuery] MenuDtoToAdd model)
        {
            var existedDish = await _menuServices.ItemIsExist(model.DishName);

            if (existedDish is true)
            {
                return BadRequest("Dish is Already registered ");
            }

            var item = await _menuServices.CreateDish(model);
            

            return Ok(item);
        }
        #endregion

        #region UpdateDish

        [HttpPut("UpdateDish")]
        public async Task<IActionResult> UpdateDish([FromQuery] Guid id, [FromQuery] MenuToUpdateDto model)
        {
           var existedItem = await _menuServices.GetById(id);

            if (existedItem == null)
                return BadRequest("record was not found");
           
          var updatedItem =  await _menuServices.UpdateDishInMenu(existedItem ,model);

            return Ok(updatedItem);
        }

        #endregion

        #region Delete dish Endpoint
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemfromMenu(Guid id)
        {
            var product = await _menuServices.GetById(id);
            if (product is null)
            {
                return NotFound($"item with Id:{id} was not found");
            }

            await _menuServices.DeleteDish(product);

            return Ok("Deleted");
        }
        #endregion


    }
}
