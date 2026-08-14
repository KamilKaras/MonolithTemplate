import { PrimeIcons } from "primereact/api";

export type AppIconButtonProps = {
  icon: keyof typeof PrimeIcons;
  onClick: () => void;

  disabled?: boolean;
  label?: string;
  tooltip?: string;
  ariaLabel?: string;
};
