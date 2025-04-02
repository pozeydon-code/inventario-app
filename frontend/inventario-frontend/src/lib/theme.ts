import { createSystem, defaultConfig } from "@chakra-ui/react";

export const theme = createSystem(defaultConfig, {
  theme: {
    tokens: {
      colors: {
        background: {
          light: { value: "#E9f6d0" },
          dark: { value: "#021e2f" },
        },
      },
    },
  },
  globalCss: {},
});
