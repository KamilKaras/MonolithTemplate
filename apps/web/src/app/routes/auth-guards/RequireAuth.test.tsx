import { render, screen } from "@testing-library/react";
import { MemoryRouter, Route, Routes, useLocation } from "react-router-dom";
import { beforeEach, describe, expect, it, vi } from "vitest";
import { useAuth } from "../../../shared/hooks/useAuth";
import { RequireAuth } from "./RequireAuth";

vi.mock("../../../shared/hooks/useAuth", () => ({
  useAuth: vi.fn(),
}));

const mockedUseAuth = vi.mocked(useAuth);

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

const renderGuard = (entry: string) => {
  return render(
    <MemoryRouter initialEntries={[entry]}>
      <Routes>
        <Route element={<RequireAuth />}>
          <Route path="/protected" element={<div>secret</div>} />
        </Route>
        <Route path="/login" element={<LocationProbe />} />
        <Route path="/problem" element={<div>problem</div>} />
      </Routes>
    </MemoryRouter>,
  );
};

beforeEach(() => {
  mockedUseAuth.mockReset();
});

describe("RequireAuth", () => {
  it("renders protected content for authenticated users", () => {
    mockedUseAuth.mockReturnValue({
      isLoading: false,
      isAuthenticated: true,
      hasAuthCheckError: false,
      isUnauthorized: false,
      user: { user: { id: "1", name: "user", roles: [] } },
    });

    renderGuard("/protected");

    expect(screen.getByText("secret")).toBeInTheDocument();
  });

  it("redirects unauthenticated users to login with a safe returnUrl", () => {
    mockedUseAuth.mockReturnValue({
      isLoading: false,
      isAuthenticated: false,
      hasAuthCheckError: false,
      isUnauthorized: true,
      user: null,
    });

    renderGuard("/protected?tab=history#notes");

    expect(screen.getByTestId("location")).toHaveTextContent(
      "/login?returnUrl=%2Fprotected%3Ftab%3Dhistory%23notes",
    );
  });

  it("shows the loader while authentication is unresolved", () => {
    mockedUseAuth.mockReturnValue({
      isLoading: true,
      isAuthenticated: false,
      hasAuthCheckError: false,
      isUnauthorized: false,
      user: null,
    });

    renderGuard("/protected");

    expect(screen.getByText("Trwa ładowanie...")).toBeInTheDocument();
  });

  it("routes auth-check failures to the problem page", () => {
    mockedUseAuth.mockReturnValue({
      isLoading: false,
      isAuthenticated: false,
      hasAuthCheckError: true,
      isUnauthorized: false,
      user: null,
    });

    renderGuard("/protected");

    expect(screen.getByText("problem")).toBeInTheDocument();
  });
});
