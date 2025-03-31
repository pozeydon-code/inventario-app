import { Container } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import { EmptyState } from "./EmptyState";

export const NotFound = () => {
  const navigate = useNavigate();

  return (
    <>
      <Container>
        <EmptyState
          title="Pagina No Encontrada"
          description="La página que estas buscando no se ha encontrado"
          onActionName="Regresar al Inicio"
          onAction={() => navigate("/")}
        />
      </Container>
    </>
  );
};
