import { PrimeIcons } from "primereact/api";
import { Button } from "primereact/button";
import type { AppIconButtonProps } from "./types";

export const AppIconButton = ({
  icon,
  label,
  tooltip,
  disabled,
  ariaLabel,
  onClick,
}: AppIconButtonProps) => {
  const tooltipId = `btn-${icon}`;

  return (
    <>
      <Button
        icon={PrimeIcons[icon]}
        onClick={onClick}
        disabled={disabled}
        aria-label={ariaLabel}
        className={`p-button-text ${tooltipId}`}
        tooltip={tooltip}
        tooltipOptions={{ position: "bottom" }}
      >
        {label}
      </Button>
    </>
  );
};
