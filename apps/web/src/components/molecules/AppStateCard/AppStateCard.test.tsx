import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { describe, expect, it, vi } from "vitest";
import AppStateCard from "./AppStateCard";

describe("AppStateCard", () => {
  it("renders the provided state copy", () => {
    render(<AppStateCard title="Unavailable" message="Try again later." />);

    expect(screen.getByText("Unavailable")).toBeInTheDocument();
    expect(screen.getByText("Try again later.")).toBeInTheDocument();
  });

  it("renders an optional action and invokes it when clicked", async () => {
    const user = userEvent.setup();
    const onAction = vi.fn();

    render(
      <AppStateCard
        title="Need action"
        message="Click through."
        actionLabel="Retry"
        onAction={onAction}
      />,
    );

    await user.click(screen.getByRole("button", { name: "Retry" }));

    expect(onAction).toHaveBeenCalledTimes(1);
  });

  it("renders an optional secondary action and invokes it when clicked", async () => {
    const user = userEvent.setup();
    const onSecondaryAction = vi.fn();

    render(
      <AppStateCard
        title="Need fallback"
        message="Try another path."
        secondaryActionLabel="Back"
        onSecondaryAction={onSecondaryAction}
      />,
    );

    await user.click(screen.getByRole("button", { name: "Back" }));

    expect(onSecondaryAction).toHaveBeenCalledTimes(1);
  });
});
