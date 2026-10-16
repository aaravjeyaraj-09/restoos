namespace Orders.Api.Models
{
    public record Dish(int Id, string DishName, decimal DishPriceNok, bool IsAvailable);
}