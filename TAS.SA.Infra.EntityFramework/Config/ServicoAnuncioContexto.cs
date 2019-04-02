using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using TAS.SA.Dominio;
using TAS.SA.Infra.EntityFramework.Mapeamentos;

namespace TAS.SA.Infra.EntityFramework.Config
{
    public class ServicoAnuncioContexto : DbContext
    {
        public ServicoAnuncioContexto(DbContextOptions options) : base(options) { }

        public DbSet<Anuncio> Anuncios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new AnuncioMap());
        }
    }
}
