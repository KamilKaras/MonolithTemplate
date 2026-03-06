import { Button } from "primereact/button";
import "./app-button.scss";
import type { AppButtonProps } from "./types";

const AppButton = (props: AppButtonProps) => {
  const { label, onClick, className = "", type = "button" } = props;

  const appButtonClass = className + " " + "app-button";

  return (
    <Button
      type={type}
      className={appButtonClass}
      label={label}
      onClick={onClick}
    ></Button>
  );
};

export default AppButton;
