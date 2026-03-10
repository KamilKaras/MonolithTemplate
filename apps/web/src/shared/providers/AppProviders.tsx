import { type ReactNode } from "react";
import { BrowserRouter } from "react-router-dom";
import { SidebarProvider } from "../../components/templates/Sidebar/SidebarProvider";
import QueryProvider from "./QueryProvider";

type AppProvidersProps = {
  children: ReactNode;
};

const AppProviders = ({ children }: AppProvidersProps) => {
  return (
    <>
      <QueryProvider>
        <BrowserRouter>
          <SidebarProvider>{children}</SidebarProvider>
        </BrowserRouter>
      </QueryProvider>
    </>
  );
};

export default AppProviders;
