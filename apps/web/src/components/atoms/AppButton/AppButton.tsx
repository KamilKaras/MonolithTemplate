import { Button } from "primereact/button";
import "./custom-btn.scss";
import type { AppButtonProps } from "./types";

const CustomBtn = (props: AppButtonProps) => {
  const { label, onClick } = props;
  return (
    <Button className="app-button" label={label} onClick={onClick}></Button>
  );
};

export default CustomBtn;
