import { AppRouter } from "@/AppRouter";
import { theme } from "@/lib/theme";
import { ChakraProvider } from "@chakra-ui/react";
import { AnimatePresence } from "framer-motion";

export default function MainProvider() {
  return (
    <ChakraProvider value={theme}>
      <AnimatePresence mode="wait" initial={true}>
        <AppRouter />
      </AnimatePresence>
    </ChakraProvider>
  );
}
