namespace RestaurantApp.Dtos.Menu
{
    public class MenuToUpdateDto
    {
        public string? DishName { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Available { get; set; } = true;
        public int? CategoryId { get; set; }
        public IFormFile? Image { get; set; }

    }
}
