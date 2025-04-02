namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class Transactions
    {
        public static Error StockWithBadFormat => Error.Validation("Transaction", "El numero de stock no tiene un formato valido");
        public static Error PriceWithBadFormat => Error.Validation("Transaction", "El precio no tiene un formato valido");
        public static Error DetailWithBadFormat => Error.Validation("Transaction", "El detalle no tiene un formato valido");
        public static Error QuantityWithBadFormat => Error.Validation("Transaction", "La cantidad no tiene un formato valido");
        public static Error NotFound => Error.NotFound("Transaction", "El Transactiono con el id proporcionado no ha sido encontrado.");
        public static Error ProductNotFound => Error.NotFound("Transaction", "El Producto con el id proporcionado no ha sido encontrado.");
        public static Error OutStock => Error.Validation("Transaction", "No quedan suficientes en stock.");
        public static Error CantUpdate => Error.Validation("Transaction", "No se pudo actualizar el stock del producto.");

    }
}
