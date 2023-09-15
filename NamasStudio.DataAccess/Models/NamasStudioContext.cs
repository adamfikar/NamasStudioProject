using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NamasStudio.DataAccess.Models;

public partial class NamasStudioContext : DbContext
{
    public NamasStudioContext()
    {
    }

    public NamasStudioContext(DbContextOptions<NamasStudioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PhotoProduct> PhotoProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<RoleUser> RoleUsers { get; set; }

    public virtual DbSet<StockProduct> StockProducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<SizeCategory> SizeCategory { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-TK8A3MKV;Initial Catalog=NamasStudio;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryProduct>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B5C81788B");

            entity.ToTable("CategoryProduct");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.DeleteAt).HasColumnType("date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF54D216AA");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ShipCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ShipCost).HasColumnType("money");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateAt).HasColumnType("date");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UsernameOrder");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_OrderId");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_ProductId");
        });

        modelBuilder.Entity<PhotoProduct>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__PhotoPro__21B7B5E292D69BC9");

            entity.Property(e => e.PathName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.PhotoProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_ProductId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD8C0723E7");

            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.DeleteAt).HasColumnType("date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Fabric)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UnitPrice).HasColumnType("money");
            entity.Property(e => e.UpdateAt).HasColumnType("date");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryProduct_CategoryId");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__ProductS__83BD097A91CAE4D0");

            entity.ToTable("ProductSize");

            entity.Property(e => e.ArmHole)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BottomSleeve)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Bust)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DeleteAt).HasColumnType("date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Hips)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LengthLower)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LengthUpper)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SizeName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SleeveLength)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdateAt).HasColumnType("date");
            entity.Property(e => e.Waist)
                .HasMaxLength(20)
                .IsUnicode(false);

          
        });

        modelBuilder.Entity<SizeCategory>(entity =>
        {
            entity.ToTable("SizeCategory");

            // Define the primary key for the junction table.
            entity.HasKey(e => new { e.SizeId, e.CategoryId }).HasName("PK__SizeCate__222D9ADA848384BD");

            // Configure the relationship with CategoryProduct
            entity.HasOne(d => d.CategoryProduct)
                .WithMany(p => p.Sizes) // Use the "Sizes" navigation property in the CategoryProduct class
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SizeCategory_CategoryProduct");

            // Configure the relationship with ProductSize
            entity.HasOne(d => d.ProductSize)
                .WithMany(p => p.Sizes) // Use the "Categories" navigation property in the ProductSize class
                .HasForeignKey("SizeId")
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SizeCategory_ProductSize");
        });

        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__RoleUser__8AFACE1A5264CFE2");

            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StockProduct>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.SizeId }).HasName("PK_StockProduct");

            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.DeleteAt).HasColumnType("date");
            entity.Property(e => e.UpdateAt).HasColumnType("date");

            entity.HasOne(d => d.Product).WithMany(p => p.StockProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_ProductId");

            entity.HasOne(d => d.Size).WithMany(p => p.StockProducts)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_SizeId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Users__536C85E576CA1FBE");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534501E4201").IsUnique();

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.DeleteAt).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegisterDate).HasColumnType("date");
            entity.Property(e => e.UpdateAt).HasColumnType("date");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleUsers_RoleId");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__UserAddr__091C2AFB3AF15CF7");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
