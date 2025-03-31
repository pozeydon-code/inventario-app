using Domain.Models;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            TransactionId => TransactionId.Value,
            value => new TransactionId(value));

        builder.Property(c => c.Date).IsRequired();

        builder.Property(c => c.ProductId).IsRequired();

        builder.Property(c => c.Quantity).HasConversion(
            quantity => quantity.Value,
            value => Quantity.Create(value)!
            );

        builder.Property(c => c.UnitPrice).HasConversion(
            unitPrice => unitPrice.Value,
            value => UnitPrice.Create(value)!).HasColumnType("decimal(18,2)");

        builder.Property(c => c.Detail).HasConversion(
            detail => detail.Value,
            value => Detail.Create(value)!).HasMaxLength(255);
    }
}
