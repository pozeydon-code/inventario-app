import { CreateProductValues } from "./productSchema";

export interface Product {
  id: string;
  name: string;
  description: string;
  category: string;
  image: string;
  price: number;
  stock: number;
}

export const EmptyCreateProduct: CreateProductValues = {
  name: "",
  description: "",
  category: "",
  price: 0,
  stock: 0,
  image: [],
};
