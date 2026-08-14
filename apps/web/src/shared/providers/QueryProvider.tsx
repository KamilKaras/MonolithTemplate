import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import type { ReactNode } from "react";
import { registerSessionRecovery } from "../auth/sessionRecovery";
import { getErrorMessage, isApiError } from "../errors/functions";
import { toastService } from "../toast/ToastService";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
    },

    mutations: {
      retry: 0,
      onError: (error) => {
        if (isApiError(error) && error.status === 401) {
          return;
        }

        if (isApiError(error) && error.status === 403) {
          toastService.error("Nie masz uprawnień do wykonania tej akcji.");
          return;
        }

        toastService.error(getErrorMessage(error));
      },
    },
  },
});

registerSessionRecovery(queryClient);

type QueryProviderProps = {
  children: ReactNode;
};

const QueryProvider = ({ children }: QueryProviderProps) => {
  return (
    <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
  );
};

export default QueryProvider;
