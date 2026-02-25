export interface AppInputTextProps {
  value: string | null;
  onChange: (value: string) => void;

  error?: string;
  hint?: string;
  required?: boolean;
  label?: string;
  placeholder?: string;
}
