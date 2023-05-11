using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShopServer.db
{
    public partial class shop4Context : DbContext
    {
        public shop4Context()
        {
        }

        public shop4Context(DbContextOptions<shop4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionType> ActionTypes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Fabricator> Fabricators { get; set; }
        public virtual DbSet<LegalClient> LegalClients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderOut> OrderOuts { get; set; }
        public virtual DbSet<PhysicalClient> PhysicalClients { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCostHistory> ProductCostHistories { get; set; }
        public virtual DbSet<ProductOrderIn> ProductOrderIns { get; set; }
        public virtual DbSet<ProductOrderOut> ProductOrderOuts { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<SaleType> SaleTypes { get; set; }
        public virtual DbSet<SupplierProduct> SupplierProducts { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Integrated Security=false; Server=192.168.1.14\\sqlexpress; Initial Catalog=shopTest; user=student; password=student;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.ToTable("ActionType");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Fabricator>(entity =>
            {
                entity.ToTable("Fabricator");

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<LegalClient>(entity =>
            {
                entity.ToTable("LegalClient");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Inn)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("INN");

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.LegalClients)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK_LegalClient_Client");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.IdActionTypeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdActionType)
                    .HasConstraintName("FK_Order_ActionType");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK_Order_Client1");
            });

            modelBuilder.Entity<OrderOut>(entity =>
            {
                entity.ToTable("OrderOut");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderOuts)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK_OrderOut_Order");

                entity.HasOne(d => d.IdSaleTypeNavigation)
                    .WithMany(p => p.OrderOuts)
                    .HasForeignKey(d => d.IdSaleType)
                    .HasConstraintName("FK_Order_SaleType");
            });

            modelBuilder.Entity<PhysicalClient>(entity =>
            {
                entity.ToTable("PhysicalClient");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.PhysicalClients)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK_PhysicalClient_Client");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("date")
                    .HasColumnName("deleted_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.RetailPrice).HasColumnType("money");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.WholesalePrice).HasColumnType("money");

                entity.HasOne(d => d.IdFabricatorNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdFabricator)
                    .HasConstraintName("FK_Product_Fabricator");

                entity.HasOne(d => d.IdProductTypeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdProductType)
                    .HasConstraintName("FK_Product_ProductType");

                entity.HasOne(d => d.IdUnitNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdUnit)
                    .HasConstraintName("FK_Product_Unit");
            });

            modelBuilder.Entity<ProductCostHistory>(entity =>
            {
                entity.ToTable("ProductCostHistory");

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.RetailPriceValue).HasColumnType("money");

                entity.Property(e => e.WholesalePirceValue).HasColumnType("money");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductCostHistories)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK_ProductCostHistory_Product1");
            });

            modelBuilder.Entity<ProductOrderIn>(entity =>
            {
                entity.ToTable("ProductOrderIn");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.ProductOrderIns)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK_ProductOrderIn_Order");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductOrderIns)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK_ProductOrderIn_Product1");
            });

            modelBuilder.Entity<ProductOrderOut>(entity =>
            {
                entity.HasKey(e => new { e.IdProductOrderIn, e.IdOrderOut })
                    .HasName("PK_ProductOrderOut_1");

                entity.ToTable("ProductOrderOut");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdOrderOutNavigation)
                    .WithMany(p => p.ProductOrderOuts)
                    .HasForeignKey(d => d.IdOrderOut)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInOrder_Order");

                entity.HasOne(d => d.IdProductOrderInNavigation)
                    .WithMany(p => p.ProductOrderOuts)
                    .HasForeignKey(d => d.IdProductOrderIn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOrderOut_ProductOrderIn");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SaleType>(entity =>
            {
                entity.ToTable("SaleType");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SupplierProduct");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK_SupplierProduct_Product");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
