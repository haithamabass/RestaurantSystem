using RestaurantApp.Models;

namespace RestaurantApp.Services.Contents.InterFaces
{
    public interface IDishImageServices
    {
        Task<DishImage> AddImageToDatabase(DishImage model);
        Task AddDishImage(Menu item, IFormFile image);

        Task UpdateDishImage(Menu item, IFormFile image);
    }
}
