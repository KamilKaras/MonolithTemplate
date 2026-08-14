import { InputText } from "primereact/inputtext";
import { useId } from "react";
import "./app-input-text.scss";
import type { AppInputTextProps } from "./types";

const AppInputText = (props: AppInputTextProps) => {
  const {
    value,
    onChange,
    placeholder,
    required,
    error,
    hint,
    label,
    autoComplete,
  } = props;
  const id = useId();

  return (
    <div className="app-input-text">
      {label ? (
        <label className="app-input-text__label" htmlFor={id}>
          {label}
          {required ? <span aria-hidden="true"> *</span> : null}
        </label>
      ) : null}
      <InputText
        id={id}
        required={required}
        invalid={Boolean(error)}
        value={value ?? ""}
        onChange={(e) => onChange(e.target.value)}
        placeholder={placeholder}
        autoComplete={autoComplete}
      />
      {error || hint ? (
        <small
          className={`app-input-text__hint${error ? " app-input-text__hint--error" : ""}`}
        >
          {error ? error : hint}
        </small>
      ) : null}
    </div>
  );
};

export default AppInputText;
