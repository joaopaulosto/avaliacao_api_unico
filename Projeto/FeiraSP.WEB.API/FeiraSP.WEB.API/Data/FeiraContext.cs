using FeiraSP.WEB.API.Model;
using Microsoft.EntityFrameworkCore;

namespace FeiraSP.WEB.API.Data
{
    /// <summary>
    /// Classe que presenta o contexto onde estao as tabelas do banco de dados
    /// </summary>
    public class FeiraContext : DbContext
    {
        public FeiraContext(DbContextOptions<FeiraContext> options) : base(options)
        {   }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                             
        }

        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<SubPrefeitura> SubPrefeituras { get; set; }
        public DbSet<Feira> Feiras { get; set; }

    }
}
