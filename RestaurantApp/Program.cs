using Microsoft.EntityFrameworkCore;
using RestaurantApp.Data;
using RestaurantApp.Helpers;
using RestaurantApp.Helpers.Queue;
using RestaurantApp.Models;
using RestaurantApp.Services.Contents.Concretes;
using RestaurantApp.Services.Contents.InterFaces;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString),ServiceLifetime.Scoped);


builder.Services.AddScoped<IMenuServices, MenuServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IInvoiceServices, InvoiceServices>();
builder.Services.AddScoped<IOrderQueueService, OrderQueueService>();
builder.Services.AddScoped<IDishImageServices, DishImageServices>();
builder.Services.AddScoped<IPaymentStatus,PaymentStatusServices>();



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

        options.JsonSerializerOptions.Converters.Add(new ActionJsonConverter());
    });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "SampleInstance";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
