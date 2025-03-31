import { Toaster } from "@/components/ui/toaster";
import { Outlet } from "react-router-dom";

export const Guard = () => {
  return (
    <>
      <Toaster />
      <Outlet />
    </>
  );
};
