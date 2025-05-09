﻿using Application.Data;
using Domain.Models;
using Domain.Primitives;

namespace Infrastructure;

public class TransactionDbContext : DbContext, ITransactionDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public TransactionDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(e => e.GetDomainEvents());

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
