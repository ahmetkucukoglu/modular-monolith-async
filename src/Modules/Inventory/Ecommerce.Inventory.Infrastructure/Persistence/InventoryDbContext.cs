using Ecommerce.Shared.Abstractions.DDD;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Inventory.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    
    public DbSet<Core.Aggregates.Inventory> Inventories { get; set; }
    
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options, IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Aggregates.Inventory>().Property(x => x.Id)
            .HasConversion(x => x.Value, id => new (id));
        
        modelBuilder.Entity<Core.Aggregates.Inventory>().Property(x => x.Sku)
            .HasConversion(x => x.Sku, id => new (id));
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