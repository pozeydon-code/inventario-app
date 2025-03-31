using System.Runtime.InteropServices;

namespace Domain.ValueObjects;

public partial record StockQuantity
{
    public int Value { get; init; }
    private StockQuantity(int value) => Value = value;

    public static StockQuantity? Create(int value)
    {
        if (value < 0)
            return null;

        return new StockQuantity(value);
    }

    public StockQuantity Reduce(int amount)
    {
        if (amount < 0) throw new ArgumentException("Monto debe ser positivo");
        if (amount > Value)
            throw new InvalidOperationException("No hay stock suficiente para la reduccion");

        return new StockQuantity(Value - amount);
    }

    public StockQuantity Increase(int amount)
    {
        if (amount < 0) throw new ArgumentException("Monto debe ser positivo");
        return new StockQuantity(Value + amount);
    }

    public StockQuantity SetStock(int amount){
        if(amount < 0 ) throw new ArgumentException("Monto debe ser mayor a 0");
        return new StockQuantity(amount);
    }

    public static implicit operator int(StockQuantity quantity) => quantity.Value;
    public override string ToString() => Value.ToString();
}
