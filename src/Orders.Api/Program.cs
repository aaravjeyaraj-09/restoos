using Orders.Api.Models;
using Orders.Api.DTO;
using Orders.Api.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrdersDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/orders/menu", async (OrdersDbContext dbContext) =>
{
    var dishes = await dbContext.Dishes.ToListAsync();
    return Results.Ok(dishes);
});

app.MapPost("/orders/menu", async (OrdersDbContext dbContext, CreateDishRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.DishName) || request.DishPriceNok <= 0)
    {
        return Results.BadRequest("Invalid dish name or price.");
    }

    var newDish = new Dish
    {
        DishName = request.DishName.Trim(),
        DishPriceNok = request.DishPriceNok,
        IsAvailable = true
    };
    dbContext.Dishes.Add(newDish);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/orders/menu/{newDish.Id}", newDish);
});

app.Run();