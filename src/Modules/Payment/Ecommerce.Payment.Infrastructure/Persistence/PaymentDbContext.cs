using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Payment.Infrastructure.Persistence;

public class PaymentDbContext: DbContext
{
    public DbSet<Core.Aggregates.Payment> Payments { get; set; }

    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Aggregates.Payment>().Property(x => x.Id)
            .HasConversion(x => x.Value, id => new (id));
        
        modelBuilder.Entity<Core.Aggregates.Payment>().Property(x => x.OrderId)
            .HasConversion(x => x.Value, id => new (id));
        
        modelBuilder.Entity<Core.Aggregates.Payment>().OwnsOne(x => x.TotalPrice,
            navigationBuilder =>
            {
                navigationBuilder.Property(address => address.Amount)
                    .HasColumnName("Amount");
                navigationBuilder.Property(address => address.Currency)
                    .HasColumnName("Currency");
            });
    }
}