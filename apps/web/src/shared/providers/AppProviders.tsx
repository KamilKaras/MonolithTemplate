import { PrimeReactProvider } from "primereact/api";
import { type ReactNode } from "react";
import { BrowserRouter } from "react-router-dom";
import { DialogProvider } from "../../components/templates/Dialog/DialogProvider";
import { SidebarProvider } from "../../components/templates/Sidebar/SidebarProvider";
import QueryProvider from "./QueryProvider";

type AppProvidersProps = {
  children: ReactNode;
};

const AppProviders = ({ children }: AppProvidersProps) => {
  return (
    <QueryProvider>
      <BrowserRouter>
        <PrimeReactProvider>
          <DialogProvider>
            <SidebarProvider>{children}</SidebarProvider>
          </DialogProvider>
        </PrimeReactProvider>
      </BrowserRouter>
    </QueryProvider>
  );
};

export default AppProviders;
