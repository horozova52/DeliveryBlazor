using DeliveryBlazor.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DeliveryAppBlazor.Infrastructure.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<Order> Orders { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<Courier> Couriers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Rela?ie 1:1 ?ntre ApplicationUser ?i Client
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Client)
                .WithOne(c => c.User)
                .HasForeignKey<ClientEntity>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rela?ie 1:1 ?ntre ApplicationUser ?i Courier
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Courier)
                .WithOne(c => c.User)
                .HasForeignKey<Courier>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rela?ie 1:M pentru Client
            builder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Rela?ie 1:M pentru Courier
            builder.Entity<Order>()
                .HasOne(o => o.Courier)
                .WithMany()
                .HasForeignKey(o => o.CourierId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adaug? indici unici
            builder.Entity<ClientEntity>()
                .HasIndex(c => c.UserId)
                .IsUnique();

            builder.Entity<Courier>()
                .HasIndex(c => c.UserId)
                .IsUnique();

            //configuram enum-ul ca  o valoare unica 

            builder.Entity<ApplicationUser>()
            .Property(u => u.Role)
            .HasConversion<string>();
        }

    }
}