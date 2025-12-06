using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MidasBlazor.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

          
            var connection = "Data Source=localhost; Initial Catalog=DB-DS-HERNAN; User Id=sa; Password=*123456HAS*;TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connection);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}