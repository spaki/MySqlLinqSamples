using LinqSamples.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqSamples.Db
{
    public class AvengersDb : DbContext
    {
        public AvengersDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Avenger> Avengers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Avenger>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Codename).IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
