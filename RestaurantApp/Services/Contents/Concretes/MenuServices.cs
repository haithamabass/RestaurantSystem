using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RestaurantApp.Data;
using RestaurantApp.Dtos.Menu;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.InterFaces;

namespace RestaurantApp.Services.Contents.Concretes
{
    public class MenuServices : IMenuServices
    {
        private readonly AppDbContext _context;
        private readonly ICategoryServices _categoryServices;
        private readonly ILogger<MenuServices> _logger;
        private readonly IDistributedCache _cache;
        private readonly IDishImageServices _dishImageServices;



        public MenuServices(AppDbContext context, ILogger<MenuServices> logger,
            IDistributedCache cache, IDishImageServices dishImageServices, ICategoryServices categoryServices)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
            _dishImageServices = dishImageServices;
            _categoryServices = categoryServices;
        }


        #region GetAll
        public async Task<List<MenuDto>> GetAll()
        {
            try
            {

                var cacheKey = "MenuItems";
                var cachedItems = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedItems))
                {
                    // Data found in cache, deserialize and return it
                    return JsonConvert.DeserializeObject<List<MenuDto>>(cachedItems);
                }

                var items = await _context.DishesAndOthers
                     .Include(r => r.Category)
                     .Include(i => i.DishImage)
                     .Where(a => a.Available == true)
                     .Select(x => new MenuDto
                     {
                         DishId = x.DishId,
                         DishName = x.DishName,
                         Description = x.Description,
                         Image = x.DishImage.Image,
                         CategoryId = x.CategoryId,
                         CategoryName = x.Category.Name,
                         
                         Available = x.Available,
                         Price = x.Price
                     })
                     .AsNoTracking()
                     .ToListAsync();

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(items), cacheEntryOptions);


                return items;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (GetAll) method");
                throw;
            }
        }
        #endregion

        #region GetAllByCategory
        public async Task<List<MenuDto>> GetAllByCategory(int id)
        {
            try
            {
                var cacheKey = $"MenuItems-{id}";
                var cachedItems = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedItems))
                {
                    // Data found in cache, deserialize and return it
                    return JsonConvert.DeserializeObject<List<MenuDto>>(cachedItems);
                }

                var items = await _context.DishesAndOthers
                     .Include(r => r.Category)
                     .Include(i => i.DishImage)

                     .Where(c => c.Category.CategoryId == id)
                     .Select(x => new MenuDto
                     {
                         DishId = x.DishId,
                         DishName = x.DishName,
                         Description = x.Description,
                         CategoryId = x.CategoryId,
                         CategoryName = x.Category.Name,
                         Image = x.DishImage.Image,
                         Available = x.Available,
                         Price = x.Price
                     })
                     .AsNoTracking()
                     .ToListAsync();

                var cacheEntryOptions = new DistributedCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(items), cacheEntryOptions);


                return items;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (GetAllByCategory) method");
                throw;
            }
        }

        #endregion

        #region GetById
        public async Task<Menu> GetById(Guid id)
        {

            try
            {

                var cacheKey = "MenuItems:GetById:" + id.ToString();
                var cachedItem = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedItem))
                {
                    // Data found in cache, deserialize and return it
                    return JsonConvert.DeserializeObject<Menu>(cachedItem);
                }


                var dish = await _context.DishesAndOthers
                    .Where(m=> m.DishId == id)
                     .Include(r => r.Category)
                     .Include(i => i.DishImage)
                     .Select(x => new Menu
                     {
                         DishId = x.DishId,
                         DishName = x.DishName,
                         Description = x.Description,
                         DishImage = x.DishImage,
                         CategoryId = x.CategoryId,
                         Available = x.Available,
                         Price = x.Price
                     })
                     .FirstOrDefaultAsync();

                if (dish == null)
                    throw new Exception("NO DISH WAS FOUND (via GetById Menu) ");

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(dish), cacheEntryOptions);


                return dish;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (via GetById Menu)");
                throw;
            }


        }
        public async Task<MenuDto> GetByIdDto(Guid id)
        {

            try
            {

                var cacheKey = "MenuItems:GetByIdDto:" + id.ToString();
                var cachedItem = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedItem))
                {
                    // Data found in cache, deserialize and return it
                    return JsonConvert.DeserializeObject<MenuDto>(cachedItem);
                }


                var dish = await _context.DishesAndOthers
                    .Where(m=> m.DishId == id)
                     .Include(r => r.Category)
                     .Include(i => i.DishImage)
                     .Select(x => new MenuDto
                     {
                         DishId = x.DishId,
                         DishName = x.DishName,
                         Description = x.Description,
                         Image = x.DishImage.Image,
                         CategoryId = x.CategoryId,
                         CategoryName =x.Category.Name,
                         Available = x.Available,
                         Price = x.Price
                     })
                     .FirstOrDefaultAsync();

                if (dish == null)
                    throw new Exception("NO DISH WAS FOUND ");

                var cacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(dish), cacheEntryOptions);


                return dish;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (via GetById Menu)");
                throw;
            }


        }
        #endregion

        #region GetByName
        public async Task<Menu> GetByName(string name)
        {

            try
            {

                var cacheKey = "MenuItems:GetByName:" + name;
                var cachedItem = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedItem))
                {
                    // Data found in cache, deserialize and return it
                    return JsonConvert.DeserializeObject<Menu>(cachedItem);
                }



                var dish = await _context.DishesAndOthers
                         .Include(r => r.Category)
                         .Include(i => i.DishImage)

                        .Where(m => m.DishName == name)
                         .Select(x => new Menu
                         {
                             DishId = x.DishId,
                             DishName = x.DishName,
                             Description = x.Description,
                             DishImage = x.DishImage,
                             CategoryId = x.CategoryId,
                             Available = x.Available,
                             Price = x.Price
                         })
                         .AsNoTracking()
                         .FirstOrDefaultAsync();

                if (dish == null)
                    throw new Exception("NO PRODUCT WAS FOUND ");


                var cacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(dish), cacheEntryOptions);

                return dish;
               

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (GetByName) method");
                throw;
            }
        }

        public async Task<List<MenuDto>> GetByNameDto(string name)
        {

            try
            {

                var cacheKey = "MenuItems:GetByNameDto:" + name;
                var cachedItems = await _cache.GetStringAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedItems))
                {
                    return JsonConvert.DeserializeObject<List<MenuDto>>(cachedItems);
                }

                var dish = await _context.DishesAndOthers
                         .Include(r => r.Category)
                         .Include(i => i.DishImage)

                         //.Where(m => m.DishName == name)
                         .Where(m => m.DishName.ToLower().StartsWith(name.ToLower()))

                         .Select(x => new MenuDto
                         {   DishId = x.DishId,
                             DishName = x.DishName,
                             Description = x.Description,
                             Image= x.DishImage.Image,
                             CategoryId = x.CategoryId,
                             CategoryName = x.Category.Name,
                             Available = x.Available,
                             Price = x.Price
                         })
                         .AsNoTracking()
                         .ToListAsync();

                if (dish == null)
                    throw new Exception("NO PRODUCT WAS FOUND  ");


                var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(dish), cacheEntryOptions);

                return dish;
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (GetByNameDto) method");
                throw;
            }

        }

        #endregion

        #region Add 
        public async Task<Menu> AddMenuItemToDatabase(Menu model)
        {
            await _context.DishesAndOthers.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<MenuDto> CreateDish(MenuDtoToAdd model)
        {
            var category = await _categoryServices.GetCategoryById(model.CategoryId);

            if (category == null)
                throw new Exception("NO category WAS FOUND");

            var dish = new Menu
            {
                
                DishName = model.DishName,
                Available = model.Available,
                CategoryId = (int)model.CategoryId,
                Price = (int)model.Price,
                Description = model.Description,
                
            };

            await AddMenuItemToDatabase(dish);

            await _dishImageServices.AddDishImage(dish, model.Image);

            // Invalidate the cache for the GetAll method
            var cacheKey = "MenuItems";
            await _cache.RemoveAsync(cacheKey);

            var dishDto = await GetByIdDto(dish.DishId);

            return dishDto;

        }


        #endregion

        #region Update
        public Menu UpdateDishInDatabase(Menu model)
        {
            try
            {
                _context.Update(model);
                _context.SaveChanges();
                return model;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdateDishInDatabase) method");
                throw;
            }

        }  
        public async Task<MenuDto> UpdateDishInMenu(Menu dish ,MenuToUpdateDto model )
        {
           try 
            {
                if (dish is null)
                {
                    throw new Exception("No dish was Found");
                }


                dish.DishName = model.DishName ?? dish.DishName;
                dish.Available = (bool)model.Available;
                dish.Description = model.Description ?? dish.Description;

                if (model.CategoryId != null)
                {
                    var category = await _categoryServices.GetCategoryById(dish.CategoryId);
                    if (category != null)
                        dish.CategoryId = (int)model.CategoryId;
                }

                if (model.Price != null)
                    dish.Price = (int)model.Price;

                await _dishImageServices.UpdateDishImage(dish, model.Image);

                UpdateDishInDatabase(dish);

                var cacheKey = "MenuItems";
                await _cache.RemoveAsync(cacheKey);

                var dishDto = await GetByIdDto(dish.DishId);

                return dishDto;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (UpdateDishInMenu) method");
                throw;
            }

                   }
        #endregion

        #region Delete
        public async Task<Menu> DeleteDish(Menu model)
        {
            
            try
            {
                _context.DishesAndOthers.Remove(model);
                _context.SaveChanges();


                await _cache.RemoveAsync("MenuItems");
                await _cache.RemoveAsync("MenuItems:GetById:" + model.DishId.ToString());


                return model;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (DeleteDish) method");
                throw;
            }
        }
        #endregion

        #region ItemIsExist
        public async Task<bool> ItemIsExist(string name)
        {
            try
            {
                return await _context.DishesAndOthers.AnyAsync(p => p.DishName == name);    

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while using (Update) method");
                throw;
            }
        }
        #endregion




    }
}
