using Gerenciador_asssinantes_plataforma_digital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_asssinantes_plataforma_digital.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Subscriber>(e => {
                e.HasKey(x => x.Id);
                e.Property(x => x.FullName).IsRequired().HasMaxLength(150); // Adicione isso
                e.Property(x => x.Email).IsRequired().HasMaxLength(150);
                e.HasIndex(x => x.Email).IsUnique();

                // Dica: Ignore a propriedade calculada para o banco não tentar criá-la
                e.Ignore(x => x.SubscriptionMonths);
            });
        }
    }
}
