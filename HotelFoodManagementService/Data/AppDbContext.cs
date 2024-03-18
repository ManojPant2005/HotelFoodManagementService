using HotelFoodManagementService.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelFoodManagementService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Recipe> Recipes { get; set; } = default!;
        public DbSet<Step> Procedures { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
    }
}
