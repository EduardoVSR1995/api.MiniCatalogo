using Microsoft.EntityFrameworkCore;

namespace api.MiniCatalogo.Model.Entity;

public partial class EntityContext : DbContext
{
    readonly api.MiniCatalogo.Configuration.Config _config;
    public EntityContext(api.MiniCatalogo.Configuration.Config config)
    {
        _config = config;
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source="+ _config.DefaultConnection);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasIndex(e => e.Nome, "IX_Categoria_nome").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.ToTable("Produto");

            entity.HasIndex(e => new { e.CategoriaId, e.Nome }, "IX_Produto_categoria_id_nome").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Nome)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("nome");
            entity.Property(e => e.Preco)
                .HasColumnType("NUMERIC(100, 4)")
                .HasColumnName("preco");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
