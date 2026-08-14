export interface AppPasswordTextProps {
  value: string | null;
  onChange: (value: string | null) => void;

  error?: string;
  hint?: string;
  label?: string;
  placeholder?: string;
  autoComplete?: string;
}
