import { QueryClientProvider } from "@tanstack/react-query";
import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter, Route, Routes, useLocation } from "react-router-dom";
import { beforeEach, describe, expect, it, vi } from "vitest";
import { createTestQueryClient } from "../../../../test/test-utils";
import { useLogin } from "./useLogin";

const loginMock = vi.hoisted(() => vi.fn());

vi.mock("../../../../api/modules/identity/identityEndpoints", () => ({
  identityEndpoints: {
    login: loginMock,
  },
}));

const LocationProbe = () => {
  const location = useLocation();

  return (
    <div data-testid="location">
      {location.pathname}
      {location.search}
      {location.hash}
    </div>
  );
};

const LoginHarness = () => {
  const { mutateAsync } = useLogin();

  return (
    <>
      <button
        type="button"
        onClick={() => {
          void mutateAsync({
            email: "user@example.com",
            password: "secret123",
          });
        }}
      >
        Sign in
      </button>
      <LocationProbe />
    </>
  );
};

const renderLogin = (entry: string) => {
  const queryClient = createTestQueryClient();

  return {
    queryClient,
    ...render(
      <QueryClientProvider client={queryClient}>
        <MemoryRouter initialEntries={[entry]}>
          <Routes>
            <Route path="/login" element={<LoginHarness />} />
            <Route path="/home" element={<LocationProbe />} />
            <Route path="/orders/123" element={<LocationProbe />} />
          </Routes>
        </MemoryRouter>
      </QueryClientProvider>,
    ),
  };
};

beforeEach(() => {
  loginMock.mockReset();
  loginMock.mockResolvedValue({});
});

describe("useLogin", () => {
  it("navigates to the authenticated landing route when no returnUrl is present", async () => {
    const user = userEvent.setup();
    renderLogin("/login");

    await user.click(screen.getByRole("button", { name: "Sign in" }));

    await waitFor(() => {
      expect(screen.getByTestId("location")).toHaveTextContent("/home");
    });
  });

  it("returns to a valid internal route with pathname, search and hash intact", async () => {
    const user = userEvent.setup();
    renderLogin("/login?returnUrl=%2Forders%2F123%3Ftab%3Dhistory%23notes");

    await user.click(screen.getByRole("button", { name: "Sign in" }));

    await waitFor(() => {
      expect(screen.getByTestId("location")).toHaveTextContent(
        "/orders/123?tab=history#notes",
      );
    });
  });

  it("falls back to the default route for malicious returnUrl values", async () => {
    const user = userEvent.setup();
    renderLogin("/login?returnUrl=https%3A%2F%2Fevil.example.com");

    await user.click(screen.getByRole("button", { name: "Sign in" }));

    await waitFor(() => {
      expect(screen.getByTestId("location")).toHaveTextContent("/home");
    });
  });
});
