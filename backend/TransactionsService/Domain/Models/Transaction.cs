using Domain.ValueObjects;

namespace Domain.Models;

public enum TransactionType
{
    Buy,
    Sell
}
public sealed class Transaction
{
    #region Propiedades
    public TransactionId Id { get; private set; }
    public DateTime Date { get; private set; }
    public TransactionType Type { get; private set; }
    public Guid ProductId { get; private set; }
    public Quantity Quantity { get; private set; }
    public UnitPrice UnitPrice { get; private set; }
    public decimal TotalPrice => UnitPrice * Quantity;
    public Detail Detail { get; private set; }
    #endregion

    #region Constructores
    private Transaction()
    {

    }

    public Transaction(TransactionId id, DateTime date, TransactionType type, Guid productoId, Quantity quantity, UnitPrice unitPrice, Detail detail)
    {
        Id = id;
        Date = date;
        Type = type;
        ProductId = productoId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Detail = detail;
    }

    public static Transaction UpdateTransaction(Guid id, DateTime date, TransactionType type, Guid productoId, Quantity quantity, UnitPrice unitPrice, Detail detail)
    {
        return new Transaction(new TransactionId(id), date, type, productoId, quantity, unitPrice, detail);
    }
    
    #endregion

}
