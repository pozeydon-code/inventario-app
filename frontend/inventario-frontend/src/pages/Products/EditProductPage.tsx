import { CreateProductForm } from "@/components/CustomForms/CreateProductForm";
import { useApi } from "@/hooks/useApi";
import { Product } from "@/models/Products/product.model";
import { useEffect } from "react";
import { useParams } from "react-router-dom";

export const EditProductPage = () => {
  const { id } = useParams<{ id: string }>();
  const { data, sendRequest } = useApi<Product>();

  useEffect(() => {
    sendRequest({ method: "get", url: `/products/${id}` });
  }, [id]);

  if (!data) return null;

  return <CreateProductForm initialData={data} isEdit={true} />;
};
