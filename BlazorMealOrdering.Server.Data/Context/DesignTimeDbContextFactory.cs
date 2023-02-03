using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace BlazorMealOrdering.Server.Data.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MealOrderinDbContext>
    {
        public MealOrderinDbContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=localhost;Port=5432;Database=MealOrderingDb;User ID=postgres;Password=asd1234;Pooling=true";
            var builder = new DbContextOptionsBuilder<MealOrderinDbContext>();

            builder.UseNpgsql(connectionString);
            return new MealOrderinDbContext(builder.Options);
        }
    }
}