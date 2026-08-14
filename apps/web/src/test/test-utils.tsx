import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { render, renderHook } from "@testing-library/react";
import type { PropsWithChildren, ReactElement } from "react";
import { MemoryRouter } from "react-router-dom";

export const createTestQueryClient = () => {
  return new QueryClient({
    defaultOptions: {
      queries: {
        retry: false,
        refetchOnWindowFocus: false,
      },
      mutations: {
        retry: false,
      },
    },
  });
};

type RenderOptions = {
  route?: string;
  queryClient?: QueryClient;
};

const createWrapper = (queryClient: QueryClient, route: string) => {
  return function Wrapper({ children }: PropsWithChildren) {
    return (
      <QueryClientProvider client={queryClient}>
        <MemoryRouter initialEntries={[route]}>{children}</MemoryRouter>
      </QueryClientProvider>
    );
  };
};

export const renderWithProviders = (
  ui: ReactElement,
  { route = "/", queryClient = createTestQueryClient() }: RenderOptions = {},
) => {
  return render(ui, {
    wrapper: createWrapper(queryClient, route),
  });
};

export const renderHookWithProviders = <TResult, TProps>(
  callback: (initialProps: TProps) => TResult,
  { route = "/", queryClient = createTestQueryClient() }: RenderOptions = {},
) => {
  return renderHook(callback, {
    wrapper: createWrapper(queryClient, route),
  });
};
