using Microsoft.EntityFrameworkCore;

namespace FeiraLivreAPI.DAO
{
    public class FeiraContext : DbContext {

        public FeiraContext(DbContextOptions<Feira> options): base(options)
        {  }

        public DbSet<Feira> Ferias { get; set; }

    }
}