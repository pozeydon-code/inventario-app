import { toaster } from "@/components/ui/toaster";
import { Tooltip } from "@/components/ui/tooltip";
import { useApi } from "@/hooks/useApi";
import { PageResponse } from "@/models/pageResponse.model";
import {
  EmptyFilters,
  Filters,
  Transaction,
} from "@/models/Transactions/transaction.model";
import {
  Box,
  Button,
  Heading,
  HStack,
  Spinner,
  Table,
  Text,
  IconButton,
  Flex,
} from "@chakra-ui/react";
import { Pencil, Trash2 } from "lucide-react";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FilterControls } from "./components/FilterControls";
import { URLS } from "@/api/url";

export const TransactionListPage = () => {
  const navigate = useNavigate();
  const [page, setPage] = useState<number>(1);
  const [pageSize] = useState<number>(10);
  const [filters, setFilters] = useState<Filters>(EmptyFilters);

  const { sendRequest, data, loading, error } = useApi<
    PageResponse<Transaction> | any
  >();

  const totalCount = data?.totalCount ?? 0;
  const totalPages = Math.ceil(totalCount / pageSize);

  useEffect(() => {
    sendRequest({
      method: "get",
      url: URLS.getTransactionsPaged,
      params: {
        page,
        pageSize,
        ...filters,
      },
    });
  }, [page, pageSize, filters]);

  const handlePrevious = () => {
    if (page > 1) setPage((actualValue) => actualValue - 1);
  };

  const handleNext = () => {
    if (page < totalPages) setPage((actualValue) => actualValue + 1);
  };

  const handleDelete = async (id: string) => {
    const confirm = window.confirm(
      "¿Estás seguro que deseas eliminar esta transacción?",
    );
    if (!confirm) return;

    const response = await sendRequest({
      method: "delete",
      url: `${URLS.getTransactions}/${id}`,
    });

    if (!response || response.status || response.status !== 200) {
      toaster.success({
        title: "Producto eliminado",
      });

      await sendRequest({
        method: "get",
        url: `${URLS.getTransactionsPaged}`,
        params: {
          page,
          pageSize,
        },
      });
    } else {
      toaster.error({
        title: "Error al Eliminar la Transaccion",
      });
    }
  };

  const handleCreateTransactions = () => {
    navigate("/transactions/create");
  };

  const handleMoveToProducts = () => {
    navigate("/");
  };

  return (
    <Box p={4}>
      <Flex gap={10}>
        <Heading mb={4}>Historial de Transacciones</Heading>
        <Button onClick={handleMoveToProducts}>Productos</Button>
      </Flex>
      {loading ? (
        <Spinner />
      ) : error ? (
        <Text color="red.500">Error al cargar Transacciones</Text>
      ) : (
        <>
          <Flex justify="space-between">
            <FilterControls
              onFilter={(f) => {
                setPage(1);
                setFilters(f);
              }}
            />
            <Button onClick={handleCreateTransactions}>
              Crear Nueva Transaccion
            </Button>
          </Flex>
          <Table.Root stickyHeader>
            <Table.Header>
              <Table.Row>
                <Table.ColumnHeader>Fecha</Table.ColumnHeader>
                <Table.ColumnHeader>Tipo</Table.ColumnHeader>
                <Table.ColumnHeader>Producto ID</Table.ColumnHeader>
                <Table.ColumnHeader>Cantidad</Table.ColumnHeader>
                <Table.ColumnHeader>Precio</Table.ColumnHeader>
                <Table.ColumnHeader>Total</Table.ColumnHeader>
                <Table.ColumnHeader>Detalle</Table.ColumnHeader>
                <Table.ColumnHeader></Table.ColumnHeader>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {data?.items?.map((transaction: Transaction) => (
                <Table.Row key={transaction.id}>
                  <Table.Cell>
                    {new Date(transaction.date).toLocaleDateString()}
                  </Table.Cell>
                  <Table.Cell>
                    {transaction.type == "Sell" ? "Vendido" : "Comprado"}
                  </Table.Cell>
                  <Table.Cell> {transaction.productId} </Table.Cell>
                  <Table.Cell> {transaction.quantity} </Table.Cell>
                  <Table.Cell> {transaction.unitPrice} </Table.Cell>
                  <Table.Cell>
                    {transaction.unitPrice * transaction.quantity}
                  </Table.Cell>
                  <Table.Cell> {transaction.detail} </Table.Cell>
                  <Table.Cell>
                    <Tooltip content="Editar">
                      <IconButton
                        bgColor="teal.600"
                        aria-label="Editar"
                        size="sm"
                        onClick={() =>
                          navigate(`/transactions/edit/${transaction.id}`)
                        }
                        mr={2}
                      >
                        <Pencil />
                      </IconButton>
                    </Tooltip>

                    <Tooltip content="Eliminar">
                      <IconButton
                        bgColor="red.400"
                        aria-label="Eliminar"
                        size="sm"
                        onClick={() => handleDelete(transaction.id)}
                        mr={2}
                      >
                        <Trash2 />
                      </IconButton>
                    </Tooltip>
                  </Table.Cell>
                </Table.Row>
              ))}
            </Table.Body>
          </Table.Root>
          <HStack gap={4} mt={4} justify="center">
            <Button onClick={handlePrevious} disabled={page === 1}>
              Anterior
            </Button>
            <Text>
              Pagina {page} de {totalPages}
            </Text>
            <Button onClick={handleNext} disabled={page === totalPages}>
              Siguiente
            </Button>
          </HStack>
        </>
      )}
    </Box>
  );
};
