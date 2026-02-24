import { Button } from "primereact/button";
import "./custom-btn.scss";
import type { CustomBtnProps } from "./types";

const CustomBtn = (props: CustomBtnProps) => {
  const { label, onClick } = props;
  return (
    <Button className="custom-btn" label={label} onClick={onClick}></Button>
  );
};

export default CustomBtn;
