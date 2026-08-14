import { render, screen } from "@testing-library/react";
import { MemoryRouter, Route, Routes, useLocation } from "react-router-dom";
import { beforeEach, describe, expect, it, vi } from "vitest";
import { useAuth } from "../../../shared/hooks/useAuth";
import { RequireGuest } from "./RequireGuest";

vi.mock("../../../shared/hooks/useAuth", () => ({
  useAuth: vi.fn(),
}));

const mockedUseAuth = vi.mocked(useAuth);

const LocationProbe = () => {
  const location = useLocation();

  return <div data-testid="location">{location.pathname}</div>;
};

const renderGuard = (entry: string) => {
  return render(
    <MemoryRouter initialEntries={[entry]}>
      <Routes>
        <Route element={<RequireGuest />}>
          <Route path="/login" element={<div>login</div>} />
          <Route path="/confirm" element={<div>confirm</div>} />
        </Route>
        <Route path="/home" element={<LocationProbe />} />
      </Routes>
    </MemoryRouter>,
  );
};

beforeEach(() => {
  mockedUseAuth.mockReset();
});

describe("RequireGuest", () => {
  it("renders guest content for unauthenticated users", () => {
    mockedUseAuth.mockReturnValue({
      isLoading: false,
      isAuthenticated: false,
      hasAuthCheckError: false,
      isUnauthorized: true,
      user: null,
    });

    renderGuard("/login");

    expect(screen.getByText("login")).toBeInTheDocument();
  });

  it("redirects authenticated users to the authenticated landing route", () => {
    mockedUseAuth.mockReturnValue({
      isLoading: false,
      isAuthenticated: true,
      hasAuthCheckError: false,
      isUnauthorized: false,
      user: { user: { id: "1", name: "user", roles: [] } },
    });

    renderGuard("/login");

    expect(screen.getByTestId("location")).toHaveTextContent("/home");
  });

  it("does not redirect while authentication is being resolved", () => {
    mockedUseAuth.mockReturnValue({
      isLoading: true,
      isAuthenticated: false,
      hasAuthCheckError: false,
      isUnauthorized: false,
      user: null,
    });

    renderGuard("/login");

    expect(screen.getByText("Trwa ładowanie...")).toBeInTheDocument();
  });
});
