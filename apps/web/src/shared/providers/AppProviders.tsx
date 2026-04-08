import { PrimeReactProvider } from "primereact/api";
import { type ReactNode } from "react";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import { DialogProvider } from "../../components/templates/Dialog/DialogProvider";
import { SidebarProvider } from "../../components/templates/Sidebar/SidebarProvider";
import { store } from "../../store/store";
import { AuthGate } from "./AuthGate";
import QueryProvider from "./QueryProvider";

type AppProvidersProps = {
  children: ReactNode;
};

const AppProviders = ({ children }: AppProvidersProps) => {
  return (
    <AuthGate>
      <Provider store={store}>
        <QueryProvider>
          <BrowserRouter>
            <PrimeReactProvider>
              <DialogProvider>
                <SidebarProvider>{children}</SidebarProvider>
              </DialogProvider>
            </PrimeReactProvider>
          </BrowserRouter>
        </QueryProvider>
      </Provider>
    </AuthGate>
  );
};

export default AppProviders;
