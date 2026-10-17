using Orders.Api.Models;
using Orders.Api.DTO;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
var menu = new List<Dish>
{
    new Dish(1, "Idly", 80.00m, true),
    new Dish(2, "String Hoppers", 100.00m, true),
    new Dish(3, "Dosa", 120.00m, true),
    new Dish(4, "Uttapam", 140.00m, true),
    new Dish(5, "Vada", 60.00m, true),
    new Dish(6, "Bonda", 70.00m, true),
    new Dish(7, "Tea", 30.00m, true),
    new Dish(8, "Filter Coffee", 40.00m, true),
    new Dish(9, "Ghee Dosa", 160.00m, true),
    new Dish(10, "Pongal", 180.00m, true)
};

app.MapGet("/orders/menu", () =>
{
    return Results.Ok(menu);
});

app.MapPost("/orders/menu", (CreateDishRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.DishName) || request.DishPriceNok <= 0)
    {
        return Results.BadRequest("Invalid dish name or price.");
    }
    var newDish = new Dish(menu.Max(d => d.Id) + 1, request.DishName.Trim(), request.DishPriceNok, true);
    menu.Add(newDish);
    return Results.Created($"/orders/menu/{newDish.Id}", newDish);

});


app.Run();