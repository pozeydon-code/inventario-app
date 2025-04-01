import {
  createTransactionSchema,
  CreateTransactionValues,
} from "@/models/Transactions/transaction.schema";
import { CustomInput } from "./components/CustomInput";
import { Box, Button, Flex, Heading } from "@chakra-ui/react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { CustomSelect } from "./components/CustomSelect";
import { CustomTextArea } from "./components/CustomTextArea";
import { useNavigate } from "react-router-dom";
import { useApi } from "@/hooks/useApi";
import { EmptyCreateTransaction } from "@/models/Transactions/transaction.model";
import { URLS } from "@/api/url";
import { toaster } from "../ui/toaster";

interface Props {
  initialData?: CreateTransactionValues & { id: string };
  isEdit?: boolean;
}

export const CreateTransactionForm = ({ initialData, isEdit }: Props) => {
  const navigate = useNavigate();
  const { loading, sendRequest } = useApi();

  const defaultValues = initialData || EmptyCreateTransaction;
  const {
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateTransactionValues>({
    resolver: zodResolver(createTransactionSchema),
    mode: "onBlur",
    defaultValues: defaultValues,
  });

  const onSubmit = async (data: CreateTransactionValues) => {
    console.log(data);
    const response = await sendRequest({
      method: isEdit ? "put" : "post",
      url: isEdit
        ? `${URLS.getTransactions}/${initialData?.id}`
        : URLS.getTransactions,
      body: data,
    });

    if (!response && response.status !== "200") {
      toaster.error({
        title: `Error al ${isEdit ? "Editar" : "Crear"} la Transaccion`,
      });
      return;
    }

    toaster.success({
      title: isEdit ? "Transaccion Actualizada" : "Transaccion Creada",
    });
    reset();
    navigate("/transactions/");
  };

  const handleBack = () => {
    navigate("/transactions/");
  };
  return (
    <Box p={4} justifyItems="center">
      <Heading mb={4}>Crear Producto</Heading>
      <form onSubmit={handleSubmit(onSubmit)}>
        <CustomInput<CreateTransactionValues>
          name="productId"
          control={control}
          label="ID del Producto"
          type="text"
          error={errors.productId}
        />

        <CustomSelect<CreateTransactionValues>
          name="type"
          control={control}
          label="Tipo"
          error={errors.type}
        >
          <option value="Buy">Compra</option>
          <option value="Sell">Venta</option>
        </CustomSelect>

        <CustomInput<CreateTransactionValues>
          name="quantity"
          control={control}
          label="Cantidad"
          type="text"
          error={errors.quantity}
        />
        <CustomInput<CreateTransactionValues>
          name="unitPrice"
          control={control}
          label="Precio Unitario"
          type="text"
          error={errors.unitPrice}
        />

        <CustomTextArea
          name="detail"
          control={control}
          label="Detalle"
          error={errors.detail}
        />

        <Flex justify="space-between" m={4}>
          <Button type="submit" bgColor="teal.600" loading={loading}>
            {isEdit ? "Editar" : "Crear"}
          </Button>
          <Button loading={loading} onClick={handleBack}>
            Volver
          </Button>
        </Flex>
      </form>
    </Box>
  );
};
