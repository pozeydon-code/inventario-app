import { Box, BoxProps, Button, Heading, Icon, Text } from "@chakra-ui/react";

interface Props extends BoxProps {
  title?: string;
  description?: string;
  onActionName?: string;
  onAction?: () => void;
}

export const EmptyState = ({
  title = "No Existen Datos",
  description = "No hay nada aqui aún",
  onActionName = "",
  onAction,
  ...rest
}: Props) => {
  return (
    <Box textAlign="center" py={10} px={6} {...rest}>
      <Heading as="h1" mt={6}>
        {title}
      </Heading>
      <Text mt={2} color="gray.600">
        {description}
      </Text>
      {onAction && (
        <Button colorScheme="teal" mt={6} onClick={onAction}>
          {onActionName}
        </Button>
      )}
    </Box>
  );
};
