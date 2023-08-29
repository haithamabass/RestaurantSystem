using Microsoft.EntityFrameworkCore;
using RestaurantApp.Data;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.InterFaces;

namespace RestaurantApp.Services.Contents.Concretes
{
    public class DishImageServices : IDishImageServices
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DishImageServices> _logger;

        public DishImageServices(AppDbContext context, ILogger<DishImageServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DishImage> AddImageToDatabase(DishImage model)
        {
            _context.Images.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task AddDishImage(Menu item, IFormFile image)
        {
            try
            {
                if (image != null)
            {
                // Convert the IFormFile to a byte array
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    // Create a new DishImage object
                    var dishImage = new DishImage
                    {
                        Image = imageBytes,
                        DishId = item.DishId
                    };
                    await AddImageToDatabase(dishImage);
                }
            }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using AddDishImage");
                throw;
            }

        }


        public async Task UpdateDishImage(Menu item, IFormFile image)
        {

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    if (item.DishImage != null)
                    {
                        item.DishImage.Image = imageBytes;
                    }
                    else
                    {
                        var dishImage = new DishImage
                        {
                            Image = imageBytes,
                            DishId = item.DishId
                        };
                        await AddImageToDatabase(dishImage);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using UpdateDishImage");
                throw;
            }


        }


    }
}


