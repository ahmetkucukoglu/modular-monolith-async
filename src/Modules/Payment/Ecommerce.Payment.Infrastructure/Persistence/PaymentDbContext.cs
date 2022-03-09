using Ecommerce.Shared.Abstractions.DDD;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Payment.Infrastructure.Persistence;

public class PaymentDbContext: DbContext
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    
    public DbSet<Core.Aggregates.Payment> Payments { get; set; }
    
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options, IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
        _domainEventDispatcher = domainEventDispatcher;
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
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries<Entity>();
        
        foreach (var entry in entries)
        {
            await _domainEventDispatcher.DispatchAsync(entry.Entity.GetEvents().ToArray());
            
            entry.Entity.ClearEvents();
        }

        return await base.SaveChangesAsync();
    }
}