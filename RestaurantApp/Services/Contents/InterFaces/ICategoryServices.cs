using RestaurantApp.Dtos.Category;
using RestaurantApp.Models;

namespace RestaurantApp.Services.Contents.InterFaces
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetAll();


        Task<Category> GetCategoryById(int id);


        Task<Category> AddCategory(Category model);



        Task<Category> UpdateCategory(int id, CategoryDto model);



        Category DeleteCategory(Category model);



        Task<bool> CategoryIsExist(string categoryName);
    }
}
