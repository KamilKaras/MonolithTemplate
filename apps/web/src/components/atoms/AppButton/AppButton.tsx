import { Button } from "primereact/button";
import "./app-button.scss";
import type { AppButtonProps } from "./types";

const AppButton = (props: AppButtonProps) => {
  const { label, onClick } = props;
  return (
    <Button
      type="submit"
      className="app-button"
      label={label}
      onClick={onClick}
    ></Button>
  );
};

export default AppButton;
