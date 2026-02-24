import type { ReactNode } from "react";

export interface AppTabViewerProps {
  tabs: TabPanelProps[];
}

export type TabPanelProps = {
  header: string;
  content: ReactNode;
};
