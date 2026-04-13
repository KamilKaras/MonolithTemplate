import { PrimeIcons } from "primereact/api";
import type { AppIconButtonProps } from "./types";

const AppIconButton = (props: AppIconButtonProps) => {
  const { icon, label, onClick } = props;
  return (
    <div className={PrimeIcons[icon]} onClick={onClick}>
      {label}
    </div>
  );
};

export default AppIconButton;
