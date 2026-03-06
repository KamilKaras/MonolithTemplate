import { Password } from "primereact/password";
import "./app-password-text.scss";
import type { AppPasswordTextProps } from "./types";

const AppPasswordText = (props: AppPasswordTextProps) => {
  const { value, onChange, placeholder, error, hint } = props;
  const labelClass = "app-password-text label" + (error ? " error" : "");
  return (
    <div className="app-password-text">
      <Password
        invalid={Boolean(error)}
        required
        placeholder={placeholder}
        value={value ?? ""}
        onChange={(e) => onChange(e.target.value)}
        feedback={false}
      />
      <small className={labelClass}>{error ? error : hint}</small>
    </div>
  );
};

export default AppPasswordText;
