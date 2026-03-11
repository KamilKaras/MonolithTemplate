import type { CSSProperties } from "react";

export type DialogOptions = {
  header?: React.ReactNode;
  content?: React.ReactNode;
  className?: string;
  style?: CSSProperties;
  showCloseIcon?: boolean;
  blockScroll?: boolean;
  onHide?: () => void;
};

export type DialogContextType = {
  openDialog: (options: DialogOptions) => void;
  closeDialog: () => void;
};

export const defaultState: DialogOptions & { visible: boolean } = {
  visible: false,
  header: null,
  content: null,
  className: "",
  style: { width: "40%" },
  showCloseIcon: true,
  blockScroll: true,
};
