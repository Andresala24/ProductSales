using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data;

public partial class PruebaIndigoContext : DbContext
{
    public PruebaIndigoContext()
    {
    }

    public PruebaIndigoContext(DbContextOptions<PruebaIndigoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InventoryProduct> InventoryProducts { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<SalesDetail> SalesDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<InventoryProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InventoryProductAndresAlarcon__Id");

            entity.ToTable("InventoryProduct", "aalarcon@indigo.tech");

            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(400)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sales__3214EC0771B0D40D");

            entity.ToTable("Sales", "aalarcon@indigo.tech");

            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreationUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<SalesDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SalesDet__3214EC07B7529C4A");

            entity.ToTable("SalesDetails", "aalarcon@indigo.tech");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalesDeta__Produ__0D7A0286");

            entity.HasOne(d => d.Sale).WithMany(p => p.SalesDetails)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalesDeta__SaleI__0E6E26BF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

