import { screen } from "@testing-library/react";
import { beforeEach, describe, expect, it, vi } from "vitest";
import { useAuth } from "../../shared/hooks/useAuth";
import { renderWithProviders } from "../../test/test-utils";
import AppRouter from "./router";

vi.mock("../../shared/hooks/useAuth", () => ({
  useAuth: vi.fn(),
}));

const mockedUseAuth = vi.mocked(useAuth);

describe("AppRouter", () => {
  beforeEach(() => {
    mockedUseAuth.mockReset();
    mockedUseAuth.mockReturnValue({
      isLoading: false,
      isAuthenticated: true,
      hasAuthCheckError: false,
      isUnauthorized: false,
      user: { user: { id: "1", name: "user", roles: [] } },
    });
  });

  it("renders authenticated private pages inside the authenticated shell", () => {
    renderWithProviders(<AppRouter />, { route: "/home" });

    expect(screen.getByText("MonolithTemplate")).toBeInTheDocument();
    expect(screen.getByText("Signed in as")).toBeInTheDocument();
    expect(screen.getByText("Welcome back, user.")).toBeInTheDocument();
  });
});
