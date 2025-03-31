namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class Transactions
    {
        public static Error StockWithBadFormat => Error.Validation("Transaction.Stock", "El numero de stock no tiene un formato valido");
        public static Error PriceWithBadFormat => Error.Validation("Transaction.Price", "El precio no tiene un formato valido");
        public static Error DetailWithBadFormat => Error.Validation("Transaction.Detail", "El detalle no tiene un formato valido");
        public static Error QuantityWithBadFormat => Error.Validation("Transaction.Quantity", "La cantidad no tiene un formato valido");
        public static Error NotFound => Error.NotFound("Transaction.NotFound", "El Transactiono con el id proporcionado no ha sido encontrado.");
        public static Error ProductNotFound => Error.NotFound("Transaction.ProductNotFound", "El Producto con el id proporcionado no ha sido encontrado.");
        public static Error OutStock => Error.Validation("Transaction.OutStock", "No quedan mas en stock.");
        public static Error CantUpdate => Error.Validation("Transaction.CantUpdate", "No se pudo actualizar el stock del producto.");

    }
}
