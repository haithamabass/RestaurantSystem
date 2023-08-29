namespace RestaurantApp.Dtos.Menu
{
    public class MenuDto
    {
        public Guid DishId { get; set; }

        public string DishName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; } = true;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte[]? Image { get; set; }

    }
}
