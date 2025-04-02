namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class Products
    {
        public static Error StockWithBadFormat => Error.Validation("Product", "El numero de stock no tiene un formato valido");
        public static Error PriceWithBadFormat => Error.Validation("Product", "El precio no tiene un formato valido");
        public static Error NameWithBadFormat => Error.Validation("Product", "El nombre no tiene un formato valido");
        public static Error DescriptionWithBadFormat => Error.Validation("Product", "La descripcion no tiene un formato valido");
        public static Error CategoryWithBadFormat => Error.Validation("Product", "La categoria no tiene un formato valido");
        public static Error ImageWithBadFormat => Error.Validation("Product", "El url de la imagen no tiene un formato valido");
        public static Error NotFound => Error.NotFound("Product", "El producto con el id proporcionado no ha sido encontrado.");
        public static Error ImageNotFound => Error.NotFound("Product", "La Imagen con el id proporcionado no ha sido encontrado.");

    }
}
