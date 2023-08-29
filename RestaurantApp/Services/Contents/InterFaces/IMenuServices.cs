using RestaurantApp.Dtos.Menu;
using RestaurantApp.Models;

namespace RestaurantApp.Services.Contents.InterFaces
{
    public interface IMenuServices
    {
        Task<List<MenuDto>> GetAll();
        Task<List<MenuDto>> GetAllByCategory(int id);
        Task<MenuDto> GetByIdDto(Guid id);
        Task<Menu> GetById(Guid id);
        Task<Menu> GetByName(string name);
        Task<List<MenuDto>> GetByNameDto(string name);
        Task<MenuDto> CreateDish(MenuDtoToAdd model);
        Task<Menu> AddMenuItemToDatabase(Menu model);
        Task<MenuDto> UpdateDishInMenu(Menu dish, MenuToUpdateDto model);
        Menu UpdateDishInDatabase(Menu model);
        Task<Menu> DeleteDish(Menu model);
        Task<bool> ItemIsExist(string name);
    }
}
