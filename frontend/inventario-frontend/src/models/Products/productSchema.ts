import { z } from "zod";

export const createProductSchema = z.object({
  name: z.string().nonempty("El nombre es requerido"),
  description: z.string().nonempty("La descripcion es requerida"),
  category: z.string().nonempty("La categoria es requerida"),
  price: z.preprocess(
    (val) => Number(val),
    z
      .number()
      .min(0.01, "El Precio debe ser mayor a 0")
      .int()
      .nonnegative("Debe ser un número positivo"),
  ),
  stock: z.preprocess(
    (val) => Number(val),
    z.number().int().nonnegative("Debe ser un número positivo"),
  ),
  image: z
    .any()
    .optional()
    .refine(
      (files) => {
        if (!files || files.length === 0) return true;
        return files[0].type?.startsWith("image/");
      },
      {
        message: "Debe ser un archivo de imagen",
      },
    ),
});

export type CreateProductValues = z.infer<typeof createProductSchema>;
