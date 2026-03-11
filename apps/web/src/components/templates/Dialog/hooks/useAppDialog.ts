import { createContext, useContext } from "react";
import type { DialogContextType } from "../types";
export const DialogContext = createContext<DialogContextType | null>(null);

export function useAppDialog() {
  const ctx = useContext(DialogContext);

  if (!ctx) {
    throw new Error("useAppDialog nie może być poza DialogProvider");
  }

  return ctx;
}
