using FuscaFilmes.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.DbContexts
{
    public class Context : DbContext
    {
        public DbSet<Filme>? Filmes { get; set; }
        public DbSet<Diretor>? Diretores { get; set; }

        //Configurando o banco SqlLite
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("Data Source=EFCoreConsole.db");
    }
}
