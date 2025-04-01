import { Filters, EmptyFilters } from "@/models/Transactions/transaction.model";
import {
  Button,
  Field,
  Flex,
  HStack,
  Input,
  NativeSelect,
} from "@chakra-ui/react";
import { useState } from "react";

interface Props {
  onFilter: (filters: Filters) => void;
}

export const FilterControls = ({ onFilter }: Props) => {
  const [filters, setFilters] = useState<Filters>(EmptyFilters);

  const handleChange = (field: keyof Filters, value: string) => {
    setFilters((prev) => ({ ...prev, [field]: value }));
  };

  const handleSubmit = () => {
    onFilter(filters);
  };
  return (
    <Flex mb={4}>
      <form style={{ width: "100%" }}>
        <HStack width="full">
          <Field.Root>
            <Field.Label>Producto</Field.Label>
            <Input
              placeholder="ID del producto"
              onChange={(e) => handleChange("productId", e.target.value)}
            />
          </Field.Root>

          <Field.Root>
            <Field.Label>Tipo</Field.Label>
            <NativeSelect.Root>
              <NativeSelect.Field
                placeholder="Todos"
                onChange={(e) => handleChange("type", e.target.value)}
              >
                <option value="Buy">Compra</option>
                <option value="Sell">Venta</option>
              </NativeSelect.Field>
            </NativeSelect.Root>
          </Field.Root>

          <Field.Root>
            <Field.Label>Desde</Field.Label>
            <Input
              type="date"
              onChange={(e) => handleChange("startDate", e.target.value)}
            />
          </Field.Root>

          <Field.Root>
            <Field.Label>Hasta</Field.Label>
            <Input
              type="date"
              onChange={(e) => handleChange("endDate", e.target.value)}
            />
          </Field.Root>
          <Button mt={6} onClick={handleSubmit} colorScheme="teal">
            Aplicar filtros
          </Button>
        </HStack>
      </form>
    </Flex>
  );
};
