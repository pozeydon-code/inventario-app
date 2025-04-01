import { z } from "zod";

export const createTransactionSchema = z.object({
  productId: z.string().uuid({ message: "ID de producto no es valido" }),
  date: z.date(),
  type: z.enum(["Buy", "Sell"]),
  quantity: z.preprocess(
    (val) => Number(val),
    z.number().int().nonnegative("Debe ser un número positivo"),
  ),
  unitPrice: z.preprocess(
    (val) => Number(val),
    z.number().int().nonnegative("Debe ser un número positivo"),
  ),
  detail: z.string().optional(),
});

export type CreateTransactionValues = z.infer<typeof createTransactionSchema>;
