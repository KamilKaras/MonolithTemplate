import { Password } from "primereact/password";
import type { AppPasswordTextProps } from "./types";

const AppPasswordText = (props: AppPasswordTextProps) => {
  const { value, onChange, placeholder } = props;
  return (
    <Password
      placeholder={placeholder}
      value={value}
      onChange={(e) => onChange(e.target.value)}
      feedback={false}
    />
  );
};

export default AppPasswordText;
