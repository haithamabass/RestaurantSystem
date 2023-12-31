﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantApp.Data;

#nullable disable

namespace RestaurantApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantApp.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Appetizers"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Entrees"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Desserts"
                        },
                        new
                        {
                            CategoryId = 4,
                            Name = "Drinks"
                        },
                        new
                        {
                            CategoryId = 6,
                            Name = "Main Dishes"
                        },
                        new
                        {
                            CategoryId = 7,
                            Name = "Meals"
                        },
                        new
                        {
                            CategoryId = 8,
                            Name = "Family Meals"
                        },
                        new
                        {
                            CategoryId = 9,
                            Name = "Happy Meals"
                        },
                        new
                        {
                            CategoryId = 10,
                            Name = "Sandwiches"
                        },
                        new
                        {
                            CategoryId = 11,
                            Name = "Pizza"
                        });
                });

            modelBuilder.Entity("RestaurantApp.Models.DishImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("DishId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("RestaurantApp.Models.Invoice", b =>
                {
                    b.Property<Guid>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentStatusId")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PaymentStatusId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("RestaurantApp.Models.InvoiceItem", b =>
                {
                    b.Property<Guid>("InvoiceItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("InvoiceItemId");

                    b.HasIndex("DishId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("RestaurantApp.Models.Menu", b =>
                {
                    b.Property<Guid>("DishId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DishName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DishId");

                    b.HasIndex("CategoryId");

                    b.ToTable("DishesAndOthers");

                    b.HasData(
                        new
                        {
                            DishId = new Guid("11111111-1111-1111-1111-111111111111"),
                            Available = true,
                            CategoryId = 11,
                            Description = "Classic pizza with tomato sauce, mozzarella, and basil",
                            DishName = "Margherita Pizza",
                            Price = 10m
                        },
                        new
                        {
                            DishId = new Guid("22222222-2222-2222-2222-222222222222"),
                            Available = true,
                            CategoryId = 1,
                            Description = "Romaine lettuce with croutons and Caesar dressing",
                            DishName = "Caesar Salad",
                            Price = 8m
                        },
                        new
                        {
                            DishId = new Guid("dd66303a-29a7-473c-bb10-1560e7f34178"),
                            Available = true,
                            CategoryId = 6,
                            Description = "Spaghetti with bacon, eggs, and cheese",
                            DishName = "Spaghetti Carbonara",
                            Price = 12m
                        },
                        new
                        {
                            DishId = new Guid("bfb46c09-6111-4854-8e56-12debc7c2fe7"),
                            Available = true,
                            CategoryId = 6,
                            Description = "Breaded chicken with tomato sauce and mozzarella",
                            DishName = "Chicken Parmesan",
                            Price = 14m
                        },
                        new
                        {
                            DishId = new Guid("e13aa729-e9cf-4261-9255-ae455a2572f5"),
                            Available = true,
                            CategoryId = 3,
                            Description = "Italian dessert with ladyfingers and mascarpone cheese",
                            DishName = "Tiramisu",
                            Price = 7m
                        },
                        new
                        {
                            DishId = new Guid("442128bb-6da1-40a5-af0c-d398f5ff66b3"),
                            Available = true,
                            CategoryId = 10,
                            Description = "Triple-decker sandwich with turkey, bacon, lettuce, and tomato",
                            DishName = "Club Sandwich",
                            Price = 9m
                        },
                        new
                        {
                            DishId = new Guid("1ed0036a-ed45-474c-9ee8-62d18f33093d"),
                            Available = true,
                            CategoryId = 8,
                            Description = "Large pizza, salad, and garlic bread for four people",
                            DishName = "Family Meal Deal",
                            Price = 25m
                        });
                });

            modelBuilder.Entity("RestaurantApp.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CancelCause")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CancelDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("OrderCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<int>("OrderTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("OrderTypeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RestaurantApp.Models.OrderItem", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ItemNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("DishId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("RestaurantApp.Models.OrderQueue", b =>
                {
                    b.Property<int>("QueuePosition")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QueuePosition"));

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("QueuePosition");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderQueue");
                });

            modelBuilder.Entity("RestaurantApp.Models.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderStatusId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderStatusId");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            OrderStatusId = 1,
                            Name = "preparing"
                        },
                        new
                        {
                            OrderStatusId = 2,
                            Name = "ready to be served in house"
                        },
                        new
                        {
                            OrderStatusId = 3,
                            Name = "ready to Pickup"
                        },
                        new
                        {
                            OrderStatusId = 4,
                            Name = "ready for delivery"
                        },
                        new
                        {
                            OrderStatusId = 5,
                            Name = "Pending"
                        },
                        new
                        {
                            OrderStatusId = 6,
                            Name = "Canceled"
                        },
                        new
                        {
                            OrderStatusId = 7,
                            Name = "Done"
                        });
                });

            modelBuilder.Entity("RestaurantApp.Models.OrderType", b =>
                {
                    b.Property<int>("OrderTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderTypeId");

                    b.ToTable("OrderTypes");

                    b.HasData(
                        new
                        {
                            OrderTypeId = 1,
                            Name = "Delivery"
                        },
                        new
                        {
                            OrderTypeId = 2,
                            Name = "Take Away"
                        },
                        new
                        {
                            OrderTypeId = 3,
                            Name = "On Site"
                        });
                });

            modelBuilder.Entity("RestaurantApp.Models.PaymentStatus", b =>
                {
                    b.Property<int>("PaymentStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentStatusId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentStatusId");

                    b.ToTable("PaymentStatuses");

                    b.HasData(
                        new
                        {
                            PaymentStatusId = 1,
                            Name = "Unpaid yet"
                        },
                        new
                        {
                            PaymentStatusId = 2,
                            Name = "Paid in cash"
                        },
                        new
                        {
                            PaymentStatusId = 3,
                            Name = "Canceled"
                        },
                        new
                        {
                            PaymentStatusId = 4,
                            Name = "Paid with card"
                        });
                });

            modelBuilder.Entity("RestaurantApp.Models.DishImage", b =>
                {
                    b.HasOne("RestaurantApp.Models.Menu", "Menu")
                        .WithOne("DishImage")
                        .HasForeignKey("RestaurantApp.Models.DishImage", "DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("RestaurantApp.Models.Invoice", b =>
                {
                    b.HasOne("RestaurantApp.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantApp.Models.PaymentStatus", "PaymentStatus")
                        .WithMany()
                        .HasForeignKey("PaymentStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("PaymentStatus");
                });

            modelBuilder.Entity("RestaurantApp.Models.InvoiceItem", b =>
                {
                    b.HasOne("RestaurantApp.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantApp.Models.Invoice", "Invoice")
                        .WithMany("OrderItems")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("RestaurantApp.Models.Menu", b =>
                {
                    b.HasOne("RestaurantApp.Models.Category", "Category")
                        .WithMany("Dishes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RestaurantApp.Models.Order", b =>
                {
                    b.HasOne("RestaurantApp.Models.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantApp.Models.OrderType", "OrderType")
                        .WithMany("Orders")
                        .HasForeignKey("OrderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderType");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("RestaurantApp.Models.OrderItem", b =>
                {
                    b.HasOne("RestaurantApp.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantApp.Models.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("RestaurantApp.Models.OrderQueue", b =>
                {
                    b.HasOne("RestaurantApp.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantApp.Models.Category", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("RestaurantApp.Models.Invoice", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantApp.Models.Menu", b =>
                {
                    b.Navigation("DishImage")
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantApp.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantApp.Models.OrderType", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
