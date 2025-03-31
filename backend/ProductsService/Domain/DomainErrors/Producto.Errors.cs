namespace Domain.DomainErrors;

public static partial class Errors
{
    public static class Products
    {
        public static Error StockWithBadFormat => Error.Validation("Product.Stock", "El numero de stock no tiene un formato valido");
        public static Error PriceWithBadFormat => Error.Validation("Product.Price", "El precio no tiene un formato valido");
        public static Error NameWithBadFormat => Error.Validation("Product.Name", "El nombre no tiene un formato valido");
        public static Error DescriptionWithBadFormat => Error.Validation("Product.Description", "La descripcion no tiene un formato valido");
        public static Error CategoryWithBadFormat => Error.Validation("Product.Category", "La categoria no tiene un formato valido");
        public static Error ImageWithBadFormat => Error.Validation("Product.Thumbnail", "El url de la imagen no tiene un formato valido");
        public static Error NotFound => Error.NotFound("Product.NotFound", "El producto con el id proporcionado no ha sido encontrado.");
        public static Error ImageNotFound => Error.NotFound("Product.ImageNotFound", "La Imagen con el id proporcionado no ha sido encontrado.");

    }
}
