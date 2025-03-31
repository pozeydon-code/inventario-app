import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import MainProvider from "./MainProvider";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <MainProvider />
  </StrictMode>,
);
