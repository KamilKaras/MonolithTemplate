import { InputText } from "primereact/inputtext";
import "./app-input-text.scss";
import type { AppInputTextProps } from "./types";

const AppInputText = (props: AppInputTextProps) => {
  const { value, onChange, placeholder, required, error, hint } = props;

  const labelClass = "app-input-text label" + (error ? " error" : "");
  return (
    <div className="app-input-text">
      <InputText
        required={required}
        invalid={Boolean(error)}
        value={value ?? ""}
        onChange={(e) => onChange(e.target.value)}
        placeholder={placeholder}
      />
      <small className={labelClass}>{error ? error : hint}</small>
    </div>
  );
};

export default AppInputText;
