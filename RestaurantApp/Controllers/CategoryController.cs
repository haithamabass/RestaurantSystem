using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Dtos.Category;
using RestaurantApp.Dtos.Menu;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.Concretes;
using RestaurantApp.Services.Contents.InterFaces;

namespace RestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        #region GetAll
        [HttpGet("Get All Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryServices.GetAll();

            if (categories is null || !categories.Any())
                return NoContent();

            return Ok(categories);

        }
        #endregion

        #region GetById Endpoint
        [HttpGet("{id}/GetCategory")]

        public async Task<IActionResult> GetCategoryById(int id)
        {

            var category = await _categoryServices.GetCategoryById(id);

            if (category == null)
            {
                return NotFound($"no category with ID : {id} was found");
            }

            return Ok(category);
        }
        #endregion

        #region AddCategory
        [HttpPost]
        [Route("AddCategory")]

        public async Task<IActionResult> AddCategory( CategoryDto model)
        {
            var existCategory = await _categoryServices.CategoryIsExist(model.Name);

            var category = new Category
            {
               Name = model.Name
            };

            if (existCategory)
                return BadRequest("category already exist ");

            await _categoryServices.AddCategory(category);

            return Ok(category);
        }
        #endregion

        #region UpdateCategory

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromQuery] int id, [FromQuery] CategoryDto model)
        {
            var existCategory = await _categoryServices.CategoryIsExist(model.Name);

            if (existCategory)
                return BadRequest("category already exist ");

            var updatedCategory = await _categoryServices.UpdateCategory(id, model);

            return Ok(updatedCategory);
        }

        #endregion

        #region DeleteCategory Endpoint
        [HttpDelete("{id}/DeleteCategory")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryServices.GetCategoryById(id);
            if (category is null)
            {
                return NotFound($"category with ID:{id} was not found");
            }

            _categoryServices.DeleteCategory(category);

            return Ok("Deleted");
        }
        #endregion

    }
}
