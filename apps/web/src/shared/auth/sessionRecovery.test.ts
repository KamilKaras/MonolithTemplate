import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { renderHook, waitFor } from "@testing-library/react";
import type { InternalAxiosRequestConfig } from "axios";
import { act, createElement, type PropsWithChildren } from "react";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";
import { USER_CREDENTIALS_QUERY_KEY } from "../../features/identity/me/hooks/types";
import { useMe } from "../../features/identity/me/hooks/useMe";

afterEach(() => {
  vi.restoreAllMocks();
});

beforeEach(() => {
  vi.resetModules();
});

const createQueryClient = () =>
  new QueryClient({
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

const createRequestConfig = (url: string): InternalAxiosRequestConfig =>
  ({
    url,
  }) as InternalAxiosRequestConfig;

const meMock = vi.hoisted(() => vi.fn());

vi.mock("../../api/modules/identity/identityEndpoints", () => ({
  identityEndpoints: {
    me: meMock,
  },
}));

const renderLiveMeObserver = (queryClient: QueryClient) => {
  const Wrapper = ({ children }: PropsWithChildren) =>
    createElement(QueryClientProvider, { client: queryClient }, children);

  return renderHook(() => useMe(), {
    wrapper: Wrapper,
  });
};

describe("sessionRecovery", () => {
  beforeEach(() => {
    meMock.mockReset();
  });

  it("ignores /identity/me 401 without clearing cached session state", async () => {
    const { registerSessionRecovery, handleUnauthorizedResponse } =
      await import("./sessionRecovery");
    const { toastService } = await import("../toast/ToastService");
    const infoSpy = vi.spyOn(toastService, "info").mockImplementation(() => {});
    const queryClient = createQueryClient();

    registerSessionRecovery(queryClient);
    queryClient.setQueryData([USER_CREDENTIALS_QUERY_KEY], { id: "user-1" });

    handleUnauthorizedResponse(createRequestConfig("/identity/me"));

    expect(queryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY])).toEqual({
      id: "user-1",
    });
    expect(infoSpy).not.toHaveBeenCalled();
  });

  it("keeps a mounted /identity/me observer stable when /identity/me returns 401", async () => {
    const queryClient = createQueryClient();
    const invalidateSpy = vi.spyOn(queryClient, "invalidateQueries");
    meMock.mockRejectedValueOnce({ status: 401 });

    const { result } = renderLiveMeObserver(queryClient);

    await waitFor(() => {
      expect(result.current.isError).toBe(true);
    });

    expect(meMock).toHaveBeenCalledTimes(1);

    const { registerSessionRecovery, handleUnauthorizedResponse } =
      await import("./sessionRecovery");

    registerSessionRecovery(queryClient);

    act(() => {
      handleUnauthorizedResponse(createRequestConfig("/identity/me"));
    });

    expect(invalidateSpy).not.toHaveBeenCalled();

    await waitFor(() => {
      expect(meMock).toHaveBeenCalledTimes(1);
    });
  });

  it("clears cached session state for protected 401 responses", async () => {
    const { registerSessionRecovery, handleUnauthorizedResponse } =
      await import("./sessionRecovery");
    const { toastService } = await import("../toast/ToastService");
    const infoSpy = vi.spyOn(toastService, "info").mockImplementation(() => {});
    const queryClient = createQueryClient();

    registerSessionRecovery(queryClient);
    queryClient.setQueryData([USER_CREDENTIALS_QUERY_KEY], { id: "user-1" });

    handleUnauthorizedResponse(createRequestConfig("/orders/123"));

    expect(
      queryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY]),
    ).toBeUndefined();
    expect(infoSpy).toHaveBeenCalledTimes(1);
  });

  it("clears a mounted auth observer for protected 401 responses without refetch churn", async () => {
    const queryClient = createQueryClient();
    const invalidateSpy = vi.spyOn(queryClient, "invalidateQueries");
    meMock.mockResolvedValueOnce({
      user: {
        id: "user-1",
        name: "User",
        roles: [],
      },
    });

    renderLiveMeObserver(queryClient);

    await waitFor(() => {
      expect(queryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY])).toEqual({
        user: {
          id: "user-1",
          name: "User",
          roles: [],
        },
      });
    });

    const { registerSessionRecovery, handleUnauthorizedResponse } =
      await import("./sessionRecovery");

    registerSessionRecovery(queryClient);

    act(() => {
      handleUnauthorizedResponse(createRequestConfig("/orders/123"));
    });

    await waitFor(() => {
      expect(
        queryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY]),
      ).toBeUndefined();
    });

    expect(invalidateSpy).not.toHaveBeenCalled();
    expect(meMock).toHaveBeenCalledTimes(1);
  });

  it("converges multiple protected 401 responses into one recovery notification", async () => {
    const { registerSessionRecovery, handleUnauthorizedResponse } =
      await import("./sessionRecovery");
    const { toastService } = await import("../toast/ToastService");
    const infoSpy = vi.spyOn(toastService, "info").mockImplementation(() => {});
    const queryClient = createQueryClient();

    registerSessionRecovery(queryClient);
    queryClient.setQueryData([USER_CREDENTIALS_QUERY_KEY], { id: "user-1" });

    handleUnauthorizedResponse(createRequestConfig("/orders/1"));
    handleUnauthorizedResponse(createRequestConfig("/orders/2"));
    handleUnauthorizedResponse(createRequestConfig("/orders/3"));

    expect(
      queryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY]),
    ).toBeUndefined();
    expect(infoSpy).toHaveBeenCalledTimes(1);
  });

  it("converges multiple protected 401 responses with a live auth observer mounted", async () => {
    const queryClient = createQueryClient();
    const { toastService } = await import("../toast/ToastService");
    const infoSpy = vi.spyOn(toastService, "info").mockImplementation(() => {});
    meMock.mockResolvedValueOnce({
      user: {
        id: "user-1",
        name: "User",
        roles: [],
      },
    });

    renderLiveMeObserver(queryClient);

    await waitFor(() => {
      expect(queryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY])).toEqual({
        user: {
          id: "user-1",
          name: "User",
          roles: [],
        },
      });
    });

    const { registerSessionRecovery, handleUnauthorizedResponse } =
      await import("./sessionRecovery");

    registerSessionRecovery(queryClient);

    act(() => {
      handleUnauthorizedResponse(createRequestConfig("/orders/1"));
      handleUnauthorizedResponse(createRequestConfig("/orders/2"));
      handleUnauthorizedResponse(createRequestConfig("/orders/3"));
    });

    expect(meMock).toHaveBeenCalledTimes(1);
    expect(infoSpy).toHaveBeenCalledTimes(1);
    expect(
      queryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY]),
    ).toBeUndefined();
  });
});
