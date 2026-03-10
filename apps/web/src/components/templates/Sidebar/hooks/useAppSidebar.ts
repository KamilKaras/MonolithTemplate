import { createContext, useContext } from "react";
import type { SidebarContextType } from "../types";
export const SidebarContext = createContext<SidebarContextType | null>(null);

export function useAppSidebar() {
  const ctx = useContext(SidebarContext);

  if (!ctx) {
    throw new Error("useAppSidebar nie może być poza SidebarProvider");
  }

  return ctx;
}
