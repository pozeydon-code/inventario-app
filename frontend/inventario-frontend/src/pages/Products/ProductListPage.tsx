import {
  Box,
  Button,
  Heading,
  HStack,
  Spinner,
  Table,
  Text,
  Input,
  InputGroup,
  IconButton,
  Flex,
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { useApi } from "@/hooks/useApi";
import { PageResponse } from "@/models/pageResponse.model";
import { Product } from "@/models/Products/product.model";
import { Tooltip } from "@/components/ui/tooltip";
import { Pencil, Trash2 } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { toaster } from "@/components/ui/toaster";

export const ProductListPage = () => {
  const navigate = useNavigate();
  const [page, setPage] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(10);
  const [search, setSearch] = useState<string>("");
  const [debouncedSearch, setDebouncedSearch] = useState("");

  const { sendRequest, data, loading, error } = useApi<PageResponse<Product>>();

  const totalCount = data?.totalCount ?? 0;
  const totalPages = Math.ceil(totalCount / pageSize);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setPage(1);
      setDebouncedSearch(search);
    }, 500);

    return () => clearTimeout(timeout);
  }, [search]);

  useEffect(() => {
    sendRequest({
      method: "get",
      url: "/products",
      params: {
        page,
        pageSize,
        search: debouncedSearch,
      },
    });
  }, [page, pageSize, debouncedSearch]);

  const handlePrevious = () => {
    if (page > 1) setPage((actualValue) => actualValue - 1);
  };

  const handleNext = () => {
    if (page < totalPages) setPage((actualValue) => actualValue + 1);
  };

  const handleDelete = async (id: string) => {
    const confirm = window.confirm(
      "¿Estás seguro que deseas eliminar este producto?",
    );
    if (!confirm) return;

    const deleted = await sendRequest({
      method: "delete",
      url: `/products/${id}`,
    });

    if (deleted) {
      toaster.success({
        title: "Producto eliminado",
      });

      await sendRequest({
        method: "get",
        url: "/products",
        params: {
          page,
          pageSize,
          search: debouncedSearch,
        },
      });
    }
  };

  const handleCrearProducto = () => {
    navigate("/products/create/");
  };

  return (
    <Box p={4}>
      <Heading mb={4}>Listado de Productos</Heading>
      {loading ? (
        <Spinner />
      ) : error ? (
        <Text color="red.500">Error al cargar Productos</Text>
      ) : (
        <>
          <Flex justify="space-between" w="80%">
            <InputGroup mb={4} maxW="400px">
              <Input
                placeholder="Buscar producto por nombre"
                value={search}
                onChange={(e) => setSearch(e.target.value)}
              />
            </InputGroup>
            <Button onClick={handleCrearProducto}>Crear Nuevo Producto</Button>
          </Flex>
          <Table.Root stickyHeader>
            <Table.Header>
              <Table.Row>
                <Table.ColumnHeader>Nombre</Table.ColumnHeader>
                <Table.ColumnHeader>Descripcion</Table.ColumnHeader>
                <Table.ColumnHeader>Categoria</Table.ColumnHeader>
                <Table.ColumnHeader>Precio</Table.ColumnHeader>
                <Table.ColumnHeader>Stock</Table.ColumnHeader>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {data?.items?.map((product) => (
                <Table.Row key={product.id}>
                  <Table.Cell> {product.name} </Table.Cell>
                  <Table.Cell> {product.description} </Table.Cell>
                  <Table.Cell> {product.category} </Table.Cell>
                  <Table.Cell> {product.price} </Table.Cell>
                  <Table.Cell> {product.stock} </Table.Cell>
                  <Table.Cell>
                    <Tooltip content="Editar">
                      <IconButton
                        bgColor="teal.600"
                        aria-label="Editar"
                        size="sm"
                        onClick={() => navigate(`/products/edit/${product.id}`)}
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
                        onClick={() => handleDelete(product.id)}
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
