using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace WebApi.Models
{
    public partial class BakingbunnyContext : DbContext
    {
        public BakingbunnyContext()
        {
        }

        public BakingbunnyContext(DbContextOptions<BakingbunnyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CakeType> CakeType { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CustomOrder> CustomOrder { get; set; }
        public virtual DbSet<Taste> Taste { get; set; }
        public virtual DbSet<OrderList> Orderlist { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SaleItem> Saleitem { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<UserSecretConfig>()
                .Build();

            if (!optionsBuilder.IsConfigured)
            {
#if (DEBUG)
                optionsBuilder.UseMySql(configuration.GetConnectionString("localBakingBunnyDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
#else
                optionsBuilder.UseMySql(configuration.GetConnectionString("prodBakingBunnyDB"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
#endif
            }
        }
    }
}
