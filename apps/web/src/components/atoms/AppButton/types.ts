export interface AppButtonProps {
  label: string;
  onClick: () => void;

  className?: string;
  type?: "submit" | "reset" | "button" | undefined;
}
