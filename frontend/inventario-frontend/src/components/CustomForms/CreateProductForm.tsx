import { Box, Button, Flex, Heading, Image } from "@chakra-ui/react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useApi } from "@/hooks/useApi";
import {
  createProductSchema,
  CreateProductValues,
} from "@/models/Products/productSchema";
import { CustomInput } from "./components/CustomInput";
import { toaster } from "../ui/toaster";
import { EmptyCreateProduct } from "@/models/Products/product.model";
import { ImageUpload } from "../ImageUpload";
import axiosClient from "@/api/clients/axiosClient";
import { useNavigate } from "react-router-dom";
import { URLS } from "@/api/url";

interface Props {
  initialData?: CreateProductValues & { id: string };
  isEdit?: boolean;
}

export const CreateProductForm = ({ initialData, isEdit }: Props) => {
  const navigate = useNavigate();
  const { loading } = useApi();

  const defaultValues = initialData || EmptyCreateProduct;

  const {
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateProductValues>({
    resolver: zodResolver(createProductSchema),
    mode: "onBlur",
    defaultValues: defaultValues,
  });

  const onSubmit = async (data: CreateProductValues) => {
    data.image = data.image[0];
    const formData = new FormData();
    formData.append("name", data.name);
    formData.append("description", data.description);
    formData.append("category", data.category);
    formData.append("price", data.price.toString());
    formData.append("stock", data.stock.toString());
    formData.append("image", data.image, data.image.name);
    const response = isEdit
      ? await axiosClient.put(`/products/${initialData?.id}`, formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        })
      : await axiosClient.post("/products", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });

    if (!response || (response.status && response.status !== 200)) {
      toaster.error({
        title: `Error al ${isEdit ? "Editar" : "Crear"} el Producto`,
      });
      return;
    }

    toaster.success({
      title: isEdit ? "Producto Actualizado" : "Producto Creado",
    });
    reset();
    navigate("/");
  };

  const handleVolver = () => {
    navigate("/");
  };

  return (
    <Box p={4} justifyItems="center">
      <Heading mb={4}>{isEdit ? "Editar Producto" : "Crear Producto"}</Heading>
      <form onSubmit={handleSubmit(onSubmit)}>
        <CustomInput<CreateProductValues>
          name="name"
          control={control}
          label="Nombre del Producto"
          type="text"
          error={errors.name}
        />
        <CustomInput<CreateProductValues>
          name="description"
          control={control}
          label="Descripcion del Producto"
          type="text"
          error={errors.description}
        />
        <CustomInput<CreateProductValues>
          name="category"
          control={control}
          label="Categoria del Producto"
          type="text"
          error={errors.category}
        />
        <CustomInput<CreateProductValues>
          name="price"
          control={control}
          label="Precio del Producto"
          type="number"
          error={errors.price}
        />
        <CustomInput<CreateProductValues>
          name="stock"
          control={control}
          label="Stock del Producto"
          type="number"
          error={errors.stock}
        />
        <Flex gap={5} m={4}>
          {isEdit ? (
            <Image
              src={`${URLS.imageUrls}${initialData?.image}`}
              aspectRatio={4 / 3}
              width="200px"
            />
          ) : null}
          <ImageUpload<CreateProductValues>
            name="image"
            control={control}
            error={errors.image}
          />
        </Flex>

        <Flex justify="space-between" m={4}>
          <Button type="submit" bgColor="teal.600" loading={loading}>
            {isEdit ? "Editar" : "Crear"}
          </Button>
          <Button loading={loading} onClick={handleVolver}>
            Volver
          </Button>
        </Flex>
      </form>
    </Box>
  );
};
