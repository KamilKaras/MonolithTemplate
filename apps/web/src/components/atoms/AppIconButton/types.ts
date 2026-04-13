import { PrimeIcons } from "primereact/api";

export type AppIconButtonProps = {
  icon: keyof typeof PrimeIcons;
  onClick: () => void;

  label?: string;
};
