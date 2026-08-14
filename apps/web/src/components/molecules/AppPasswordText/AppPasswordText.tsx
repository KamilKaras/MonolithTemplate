import { Password } from "primereact/password";
import { useId } from "react";
import "./app-password-text.scss";
import type { AppPasswordTextProps } from "./types";

const AppPasswordText = (props: AppPasswordTextProps) => {
  const { value, onChange, placeholder, error, hint, label, autoComplete } =
    props;
  const id = useId();
  return (
    <div className="app-password-text">
      {label ? (
        <label className="app-password-text__label" htmlFor={id}>
          {label}
        </label>
      ) : null}
      <Password
        inputId={id}
        invalid={Boolean(error)}
        required
        placeholder={placeholder}
        value={value ?? ""}
        onChange={(e) => onChange(e.target.value)}
        feedback={false}
        toggleMask
        autoComplete={autoComplete}
      />
      {error || hint ? (
        <small
          className={`app-password-text__hint${error ? " app-password-text__hint--error" : ""}`}
        >
          {error ? error : hint}
        </small>
      ) : null}
    </div>
  );
};

export default AppPasswordText;
