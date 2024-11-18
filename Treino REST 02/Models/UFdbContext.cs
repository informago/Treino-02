using Microsoft.EntityFrameworkCore;

namespace Treino_REST_02.Models
{
    public class UFdbContext : DbContext
    {
        public DbSet<UF> UFs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var cnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["cnMain"];
            optionsBuilder.UseSqlServer(cnStr);
        }
    }

}
