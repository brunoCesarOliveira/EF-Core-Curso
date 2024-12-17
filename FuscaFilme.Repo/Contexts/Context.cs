using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Contexts
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

            modelBuilder.Entity<Diretor>().HasData(
                 new Diretor { Id = 1, Name = "Christopher Nolan" },
                 new Diretor { Id = 2, Name = "Quentin Tarantino" },
                 new Diretor { Id = 3, Name = "Steven Spielberg" },
                 new Diretor { Id = 4, Name = "Martin Scorsese" },
                 new Diretor { Id = 5, Name = "Greta Gerwig" }
             );

            modelBuilder.Entity<Filme>().HasData(
                new Filme { Id = 1, Titulo = "Inception", Ano = 2010, DiretorId = 1 },
                new Filme { Id = 2, Titulo = "The Dark Knight", Ano = 2008, DiretorId = 1 },
                new Filme { Id = 3, Titulo = "Pulp Fiction", Ano = 1994, DiretorId = 2 },
                new Filme { Id = 4, Titulo = "Django Unchained", Ano = 2012, DiretorId = 2 },
                new Filme { Id = 5, Titulo = "Jurassic Park", Ano = 1993, DiretorId = 3 },
                new Filme { Id = 6, Titulo = "Schindler's List", Ano = 1993, DiretorId = 3 },
                new Filme { Id = 7, Titulo = "The Wolf of Wall Street", Ano = 2013, DiretorId = 4 },
                new Filme { Id = 8, Titulo = "Goodfellas", Ano = 1990, DiretorId = 4 },
                new Filme { Id = 9, Titulo = "Lady Bird", Ano = 2017, DiretorId = 5 },
                new Filme { Id = 10, Titulo = "Barbie", Ano = 2023, DiretorId = 5 }
            );
        }
    }
}
