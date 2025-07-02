using Ecommerce.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.DBContext;
public class DbEcommerceContext : DbContext
{
    public DbEcommerceContext(DbContextOptions<DbEcommerceContext> options)
        : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetallesVenta { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Categoria
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);
            entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("getdate()");
        });

        // Producto
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);
            entity.Property(e => e.Nombre).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");
            entity.Property(e => e.PrecioOferta).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Imagen);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("getdate()");

            entity.HasOne(d => d.Categoria)
                  .WithMany(p => p.Productos)
                  .HasForeignKey(d => d.IdCategoria)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);
            entity.Property(e => e.NombreCompleto).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Clave).HasMaxLength(50);
            entity.Property(e => e.Rol).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("getdate()");
        });

        // Venta
        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta);
            entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("getdate()");

            entity.HasOne(d => d.Usuario)
                  .WithMany(p => p.Ventas)
                  .HasForeignKey(d => d.IdUsuario)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // DetalleVenta
        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta);
            entity.Property(e => e.Total).HasColumnType("decimal(10,2)");

            entity.HasOne(d => d.Venta)
                  .WithMany(p => p.DetallesVenta)
                  .HasForeignKey(d => d.IdVenta)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Producto)
                  .WithMany(p => p.DetallesVenta)
                  .HasForeignKey(d => d.IdProducto)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
