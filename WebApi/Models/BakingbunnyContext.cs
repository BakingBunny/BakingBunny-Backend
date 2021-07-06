using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace WebApiCrud.Models
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

        public virtual DbSet<Caketype> Caketypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customorder> Customorders { get; set; }
        public virtual DbSet<Fruit> Fruits { get; set; }
        public virtual DbSet<Orderlist> Orderlists { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Saleitem> Saleitems { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Caketype>(entity =>
            {
                entity.ToTable("caketype");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customorder>(entity =>
            {
                entity.ToTable("customorder");

                entity.HasIndex(e => e.CakeTypeId, "fk_customOrder_cakeType");

                entity.HasIndex(e => e.FruitId, "fk_customOrder_fruit");

                entity.HasIndex(e => e.SizeId, "fk_customOrder_size");

                entity.HasIndex(e => e.UserId, "fk_customOrder_user");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.ExampleImage).HasMaxLength(200);

                entity.Property(e => e.Message).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CakeType)
                    .WithMany(p => p.Customorders)
                    .HasForeignKey(d => d.CakeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customOrder_cakeType");

                entity.HasOne(d => d.Fruit)
                    .WithMany(p => p.Customorders)
                    .HasForeignKey(d => d.FruitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customOrder_fruit");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Customorders)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customOrder_size");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customorders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_customOrder_user");
            });

            modelBuilder.Entity<Fruit>(entity =>
            {
                entity.ToTable("fruit");

                entity.Property(e => e.Fruit1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Fruit");
            });

            modelBuilder.Entity<Orderlist>(entity =>
            {
                entity.ToTable("orderlist");

                entity.HasIndex(e => e.SaleItemId, "fk_orderList_saleItem");

                entity.HasIndex(e => e.UserId, "fk_orderList_user");

                entity.Property(e => e.Delivery).HasColumnType("bit(1)");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.SaleItem)
                    .WithMany(p => p.Orderlists)
                    .HasForeignKey(d => d.SaleItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderList_saleItem");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orderlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orderList_user");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.CategoryId, "fk_product_category");

                entity.Property(e => e.Active).HasColumnType("bit(1)");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductImage).HasMaxLength(200);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_category");
            });

            modelBuilder.Entity<Saleitem>(entity =>
            {
                entity.ToTable("saleitem");

                entity.HasIndex(e => e.FruitId, "fk_saleItem_fruit");

                entity.HasIndex(e => e.ProductId, "fk_saleItem_product");

                entity.HasIndex(e => e.SizeId, "fk_saleItem_size");

                entity.Property(e => e.Discount).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Fruit)
                    .WithMany(p => p.Saleitems)
                    .HasForeignKey(d => d.FruitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_saleItem_fruit");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Saleitems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_saleItem_product");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Saleitems)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_saleItem_size");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("size");

                entity.Property(e => e.Size1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Size");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
