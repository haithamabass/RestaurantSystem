using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantApp.Models;
using System.Web.Helpers;

namespace RestaurantApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Menu> DishesAndOthers { get; set; }
        public DbSet<DishImage> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<OrderQueue> OrderQueue { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region DishesAndOthers

            modelBuilder.Entity<Menu>().HasData(
                    new Menu
                    {
                        DishId = new Guid("11111111-1111-1111-1111-111111111111"),
                        DishName = "Margherita Pizza",
                        Description = "Classic pizza with tomato sauce, mozzarella, and basil",
                       
                        Price = 10,
                        CategoryId = 11
                    },
                    new Menu
                    {
                        DishId = new Guid("22222222-2222-2222-2222-222222222222"),
                        DishName = "Caesar Salad",
                        Description = "Romaine lettuce with croutons and Caesar dressing",
                      
                        Price = 8,
                        CategoryId = 1
                    },
                    new Menu
                    {
                        DishId = Guid.NewGuid(),
                        DishName = "Spaghetti Carbonara",
                        Description = "Spaghetti with bacon, eggs, and cheese",
                      
                        Price = 12,
                        CategoryId = 6
                    },
                    new Menu
                    {
                        DishId = Guid.NewGuid(),
                        DishName = "Chicken Parmesan",
                        Description = "Breaded chicken with tomato sauce and mozzarella",
                        Price = 14,
                        CategoryId = 6
                    },
                    new Menu
                    {
                        DishId = Guid.NewGuid(),
                        DishName = "Tiramisu",
                        Description = "Italian dessert with ladyfingers and mascarpone cheese",
                     
                        Price = 7,
                        CategoryId = 3
                    },
                    new Menu
                    {
                        DishId = Guid.NewGuid(),
                        DishName = "Club Sandwich",
                        Description = "Triple-decker sandwich with turkey, bacon, lettuce, and tomato",
                       
                        Price = 9,
                        CategoryId = 10
                    },
                    new Menu
                    {
                        DishId = Guid.NewGuid(),
                        DishName = "Family Meal Deal",
                        Description = "Large pizza, salad, and garlic bread for four people",
                       
                        Price = 25,
                        CategoryId = 8
                    });


            #endregion

            #region Categories
            modelBuilder.Entity<Category>().HasData(
                  new Category { CategoryId = 1, Name = "Appetizers" },
                  new Category { CategoryId = 2, Name = "Entrees" },
                  new Category { CategoryId = 3, Name = "Desserts" },
                  new Category { CategoryId = 4, Name = "Drinks" },
                  new Category { CategoryId = 6, Name = "Main Dishes" },
                  new Category { CategoryId = 7, Name = "Meals" },
                  new Category { CategoryId = 8, Name = "Family Meals" },
                  new Category { CategoryId = 9, Name = "Happy Meals" },
                  new Category { CategoryId = 10, Name = "Sandwiches" },
                  new Category { CategoryId = 11, Name = "Pizza" }

              );
            #endregion

            #region OrderTypes

            modelBuilder.Entity<OrderType>().HasData
                (
                new OrderType { OrderTypeId = 1, Name = "Delivery" },
                new OrderType { OrderTypeId = 2, Name = "Take Away" },
                new OrderType { OrderTypeId = 3, Name = "On Site" }
                );

            #endregion

            #region OrderStatus
            modelBuilder.Entity<OrderStatus>().HasData
              (
              new OrderStatus { OrderStatusId = 1, Name = "preparing" },
              new OrderStatus { OrderStatusId = 2, Name = "ready to be served in house" },
              new OrderStatus { OrderStatusId = 3, Name = "ready to Pickup" },
              new OrderStatus { OrderStatusId = 4, Name = "ready for delivery" },
              new OrderStatus { OrderStatusId = 5, Name = "Pending" },
              new OrderStatus { OrderStatusId = 6, Name = "Canceled" },
              new OrderStatus { OrderStatusId = 7, Name = "Done" }
              );
            #endregion

            #region PaymentStatus

            modelBuilder.Entity<PaymentStatus>().HasData
             (
             new PaymentStatus { PaymentStatusId = 1, Name = "Unpaid yet" },
             new PaymentStatus { PaymentStatusId = 2, Name = "Paid in cash" },
             new PaymentStatus { PaymentStatusId = 3, Name = "Canceled" },
             new PaymentStatus { PaymentStatusId = 4, Name = "Paid with card" }
             );
            #endregion


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Enable sensitive data logging
            optionsBuilder.EnableSensitiveDataLogging();
        }





    }
}
