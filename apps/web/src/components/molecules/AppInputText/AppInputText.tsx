import { InputText } from "primereact/inputtext";
import type { AppInputTextProps } from "./types";

const AppInputText = (props: AppInputTextProps) => {
  const { value, onChange, placeholder } = props;
  return (
    <div className="app-input-text">
      <InputText
        value={value}
        onChange={(e) => onChange(e.target.value)}
        placeholder={placeholder}
      />
    </div>
  );
};

export default AppInputText;
