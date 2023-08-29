using Microsoft.EntityFrameworkCore;
using RestaurantApp.Data;
using RestaurantApp.Dtos.Category;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.InterFaces;
using System.Drawing;

namespace RestaurantApp.Services.Contents.Concretes
{
    public class CategoryServices : ICategoryServices
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryServices> _logger;


        public CategoryServices(AppDbContext context,  ILogger<CategoryServices> logger)
        {
            _context = context;
            _logger = logger;
        }


        #region GetAll
        public async Task<List<Category>> GetAll()
        {
            try
            {
                return await _context.Categories
                       .Include(c => c.Dishes) 
                      .AsNoTracking()
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (GetAll) method CategoryServices");
                throw;
            }
        }
        #endregion

        #region GetById
        public async Task<Category> GetCategoryById(int id)
        {
             try
            {
                var category = await _context.Categories
                               .Include(d => d.Dishes)
                             .FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category is null)
                {
                    throw new Exception("No category was found");
                }
                return category;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (GetById) method CategoryServices");
                throw;
            }

        }
        #endregion

        #region AddCategory
        public async Task<Category> AddCategory(Category model)
        {
            try
            {

               await _context.Categories.AddAsync(model);

                _context.SaveChanges();

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (AddCategory) method CategoryServices");
                throw;
            }
        }
        #endregion

        #region UpdateCategory
        public async Task <Category> UpdateCategory(int id , CategoryDto model)
        {
            try
            {
                var category = await GetCategoryById(id);
                
                category.Name = model.Name;    

                _context.Update(category);
                _context.SaveChanges();
                return category;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdateCategory) method CategoryServices");
                throw;
            }
        }
        #endregion

        #region DeleteCategory
        public Category DeleteCategory(Category model)
        {
            try
            {
                _context.Remove(model);
                _context.SaveChanges();
                return model;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (DeleteCategory) method CategoryServices");
                throw;
            }
        }
        #endregion

        #region CategoryIsExist
        public async Task<bool> CategoryIsExist(string categoryName)
        {
            try
            {
                return await _context.Categories.AnyAsync(p => p.Name == categoryName);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (CategoryIsExist) method CategoryServices");
                throw;
            }
        }
        #endregion
    }
}
