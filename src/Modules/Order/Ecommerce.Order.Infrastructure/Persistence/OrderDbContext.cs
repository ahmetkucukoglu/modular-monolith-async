using Ecommerce.Order.Core.Aggregates;
using Ecommerce.Shared.Abstractions.DDD;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.Infrastructure.Persistence;

public class OrderDbContext : DbContext
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    
    public DbSet<Core.Aggregates.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public OrderDbContext(DbContextOptions<OrderDbContext> options, IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Aggregates.Order>().Property(x => x.Id)
            .HasConversion(x => x.Value, id => new (id));
        
        modelBuilder.Entity<OrderItem>().Property(x => x.Id)
            .HasConversion(x => x.Value, id => new (id));
        
        modelBuilder.Entity<OrderItem>().OwnsOne(x => x.Price,
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