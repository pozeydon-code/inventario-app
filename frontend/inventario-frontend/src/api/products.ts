import axios from "axios";
import { Product } from "@/models/product.model";
import { PageResponse } from "@/models/pageResponse.model";

const BASE_URL = "http://localhost:5106/api/products/";

export const fetchProducts = async (
  page: number,
  pageSize: number,
  search?: string,
): Promise<PageResponse<Product>> => {
  const params = new URLSearchParams();
  params.append("page", page.toString());
  params.append("pageSize", pageSize.toString());

  if (search) params.append("search", search);

  const response = await axios.get<PageResponse<Product>>(
    `${BASE_URL}?${params}`,
  );

  console.log(response);
  return response.data;
};
