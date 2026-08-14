import { QueryClientProvider } from "@tanstack/react-query";
import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { MemoryRouter, Route, Routes, useLocation } from "react-router-dom";
import { beforeEach, describe, expect, it, vi } from "vitest";
import { createTestQueryClient } from "../../../../test/test-utils";
import { USER_CREDENTIALS_QUERY_KEY } from "../../../identity/me/hooks/types";
import { useLogout } from "./useLogout";

const logoutMock = vi.hoisted(() => vi.fn());

vi.mock("../../../../api/modules/identity/identityEndpoints", () => ({
  identityEndpoints: {
    logout: logoutMock,
  },
}));

const LocationProbe = () => {
  const location = useLocation();

  return <div data-testid="location">{location.pathname}</div>;
};

const LogoutHarness = () => {
  const { mutateAsync } = useLogout();

  return (
    <>
      <button type="button" onClick={() => void mutateAsync()}>
        Sign out
      </button>
      <LocationProbe />
    </>
  );
};

const renderLogout = () => {
  const queryClient = createTestQueryClient();
  queryClient.setQueryData([USER_CREDENTIALS_QUERY_KEY], {
    id: "user-1",
    userName: "user",
  });

  return {
    queryClient,
    ...render(
      <QueryClientProvider client={queryClient}>
        <MemoryRouter initialEntries={["/home"]}>
          <Routes>
            <Route path="/home" element={<LogoutHarness />} />
            <Route path="/login" element={<LocationProbe />} />
          </Routes>
        </MemoryRouter>
      </QueryClientProvider>,
    ),
  };
};

beforeEach(() => {
  logoutMock.mockReset();
  logoutMock.mockResolvedValue({});
});

describe("useLogout", () => {
  it("removes cached session state and navigates to login on success", async () => {
    const user = userEvent.setup();
    const { queryClient } = renderLogout();

    await user.click(screen.getByRole("button", { name: "Sign out" }));

    await waitFor(() => {
      expect(screen.getByTestId("location")).toHaveTextContent("/login");
    });

    expect(
      queryClient.getQueryData([USER_CREDENTIALS_QUERY_KEY]),
    ).toBeUndefined();
  });
});
