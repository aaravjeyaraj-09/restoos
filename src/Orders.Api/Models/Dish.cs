namespace Orders.Api.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; } = string.Empty;
        public decimal DishPriceNok { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}