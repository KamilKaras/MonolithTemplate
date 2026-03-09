export interface AppButtonProps {
  label: string;
  onClick?: () => void;

  loading?: boolean;
  className?: string;
  type?: "submit" | "reset" | "button" | undefined;
}
