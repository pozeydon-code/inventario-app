import { BrowserRouter, Route } from "react-router-dom";
import { RoutesWithNotFound } from "./components/RoutesWithNotFound";
import { ProductListPage } from "./pages/Products/ProductListPage";
import { CreateProductForm } from "./components/CustomForms/CreateProductForm";
import { EditProductPage } from "./pages/Products/EditProductPage";
import { Guard } from "./guard/Guard";

export const AppRouter = () => {
  return (
    <>
      <BrowserRouter>
        <RoutesWithNotFound>
          <Route element={<Guard />}>
            <Route path="/*" element={<ProductListPage />} />
            <Route path="/products/create" element={<CreateProductForm />} />
            <Route path="/products/edit/:id" element={<EditProductPage />} />
          </Route>
        </RoutesWithNotFound>
      </BrowserRouter>
    </>
  );
};
