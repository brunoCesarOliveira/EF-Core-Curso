using FuscaFilme.Domain.Entities;
using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FuscaFilmes.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<DiretorFilme> DiretoresFilmes { get; set; }

        // Configurando o banco SqlLite Passou para appsettings.json
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("Data Source=EFCoreConsole.db");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /** Relacionamneto de 1-Diretor N-Filmes
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
            */


            /** Relacionamentos N-Diretores N-Filmes**/

            modelBuilder.Entity<Diretor>()
                .HasMany(d => d.Filmes)
                .WithMany(f => f.Diretores)
                .UsingEntity<DiretorFilme>(

                df => df
                     .HasOne<Filme>(e => e.Filme)
                     .WithMany(e => e.DiretoresFilmes)
                     .HasForeignKey(df => df.FilmeId),

                 df => df
                     .HasOne<Diretor>(e => e.Diretor)
                     .WithMany(e => e.DiretoresFilmes)
                     .HasForeignKey(df => df.DiretorId),

           df =>
                   {
                       df.HasKey(df => new { df.DiretorId, df.FilmeId });
                       df.ToTable("DiretoresFilmes");
                   }
                );

            modelBuilder.Entity<Diretor>()
                .HasOne(d => d.DiretorDetalhes)
                .WithOne(d => d.Diretor)
                .HasForeignKey<DiretorDetalhe>(dd => dd.DiretorId);

            modelBuilder.Entity<DiretorDetalhe>().HasData(
                new DiretorDetalhe { Id = 1, DiretorId = 1, Biografia = "Biografia do Christopher Nolan", DataNascimento = new DateTime(1970, 7, 30) },
                     new DiretorDetalhe { Id = 2, DiretorId = 2, Biografia = "Stevem Spielberg do Christopher Nolan", DataNascimento = new DateTime(1946, 12, 18) }
                );
            modelBuilder.Entity<Diretor>().HasData(
                 new Diretor { Id = 1, Name = "Christopher Nolan" },
                 new Diretor { Id = 2, Name = "Quentin Tarantino" },
                 new Diretor { Id = 3, Name = "Steven Spielberg" },
                 new Diretor { Id = 4, Name = "Martin Scorsese" },
                 new Diretor { Id = 5, Name = "Greta Gerwig" }
             );

            modelBuilder.Entity<Filme>().HasData(
                new Filme { Id = 1, Titulo = "Inception", Ano = 2010, },
                new Filme { Id = 2, Titulo = "The Dark Knight", Ano = 2008, },
                new Filme { Id = 3, Titulo = "Pulp Fiction", Ano = 1994, },
                new Filme { Id = 4, Titulo = "Django Unchained", Ano = 2012, },
                new Filme { Id = 5, Titulo = "Jurassic Park", Ano = 1993, },
                new Filme { Id = 6, Titulo = "Schindler's List", Ano = 1993, },
                new Filme { Id = 7, Titulo = "The Wolf of Wall Street", Ano = 2013, },
                new Filme { Id = 8, Titulo = "Goodfellas", Ano = 1990, },
                new Filme { Id = 9, Titulo = "Lady Bird", Ano = 2017, },
                new Filme { Id = 10, Titulo = "Barbie", Ano = 2023, }
            );

            modelBuilder.Entity<DiretorFilme>().HasData(
                new { DiretorId = 1, FilmeId = 1 },
                new { DiretorId = 1, FilmeId = 2 },
                new { DiretorId = 1, FilmeId = 3 },
                new { DiretorId = 2, FilmeId = 4 },
                new { DiretorId = 2, FilmeId = 5 },
                new { DiretorId = 2, FilmeId = 6 },
                new { DiretorId = 3, FilmeId = 7 },
                new { DiretorId = 3, FilmeId = 8 },
                new { DiretorId = 3, FilmeId = 9 },
                new { DiretorId = 4, FilmeId = 10 },
                new { DiretorId = 4, FilmeId = 11 },
                new { DiretorId = 4, FilmeId = 12 },
                new { DiretorId = 5, FilmeId = 13 },
                new { DiretorId = 5, FilmeId = 14 },
                new { DiretorId = 5, FilmeId = 15 },
                new { DiretorId = 6, FilmeId = 16 },
                new { DiretorId = 6, FilmeId = 17 },
                new { DiretorId = 6, FilmeId = 18 }
                );

        }
    }
}
