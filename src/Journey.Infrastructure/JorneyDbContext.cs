using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure
{
    public class JorneyDbContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\Matheus\Documents\Repositories\Journey\JourneyDatabase.db");
        }

        //Aqui estamos configuirando o entity framework para buscar a entidade "Activity" na tabela "Activities"
        //Como isso não precisamos criar propeties com o mesmo nome das tabelas.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Activity>().ToTable("Activities");
        }
    }
}
