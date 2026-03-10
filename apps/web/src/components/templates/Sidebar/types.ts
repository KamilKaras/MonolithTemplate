import type { CSSProperties } from "react";

export type SidebarOptions = {
  header?: React.ReactNode;
  content?: React.ReactNode;
  position?: "left" | "right" | "top" | "bottom";
  className?: string;
  style?: CSSProperties;
  showCloseIcon?: boolean;
  blockScroll?: boolean;
  onHide?: () => void;
};

export type SidebarContextType = {
  openSidebar: (options: SidebarOptions) => void;
  closeSidebar: () => void;
  updateSidebar: (options: Partial<SidebarOptions>) => void;
};

export const defaultState: SidebarOptions & { visible: boolean } = {
  visible: false,
  header: null,
  content: null,
  position: "right",
  className: "",
  style: { width: "30%" },
  showCloseIcon: true,
  blockScroll: true,
};
