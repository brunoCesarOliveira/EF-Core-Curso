using FuscaFilmes.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.DbContexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Filme>? Filmes { get; set; }
        public DbSet<Diretor>? Diretores { get; set; }

        // Configurando o banco SqlLite Passou para appsettings.json
        /**
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("Data Source=EFCoreConsole.db");
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fazendo a configurção dos relacionamentos
            modelBuilder.Entity<Diretor>()
                        .HasMany(e => e.Filmes)
                        .WithOne(e => e.Diretor)
                        .HasForeignKey(fk => fk.DiretorId)
                        .IsRequired();
        }
    }
}
