using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InternetShopAspNetCoreMvc.Data
{
    public class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<InternetShopDbContext>
    {
        public InternetShopDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<InternetShopDbContext>();
            builder.UseSqlServer(
                "Data Source=DESKTOP-MDHDNGI\\SQLEXPRESS;" +
                "Initial Catalog=InternetShop;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;"
            );

            return new InternetShopDbContext(builder.Options);
        }
    }
}
