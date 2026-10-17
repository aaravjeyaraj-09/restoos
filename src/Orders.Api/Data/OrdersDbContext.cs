using Orders.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Orders.Api.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; } = null!;
    }
}