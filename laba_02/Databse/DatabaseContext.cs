using Microsoft.EntityFrameworkCore;

namespace laba_02.Databse;

public class DatabaseContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>().HasKey(x => x.DishId);
        modelBuilder.Entity<Product>().HasKey(x => x.ProductId);
        
        // Настройка связей
        modelBuilder.Entity<Ingredient>()
            .HasOne(i => i.Dish)
            .WithMany(d => d.Ingredients)
            .HasForeignKey(i => i.DishId);

        modelBuilder.Entity<Ingredient>()
            .HasOne(i => i.Product)
            .WithMany(p => p.Ingredients)
            .HasForeignKey(i => i.ProductId);
    }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}
