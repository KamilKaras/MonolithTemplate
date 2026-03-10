// sidebar/SidebarProvider.tsx
import { Sidebar } from "primereact/sidebar";
import { useCallback, useMemo, useState, type ReactNode } from "react";
import { SidebarContext } from "./hooks/useAppSidebar";
import { defaultState, type SidebarOptions } from "./types";

export function SidebarProvider({ children }: { children: ReactNode }) {
  const [state, setState] = useState(defaultState);

  const closeSidebar = useCallback(() => {
    setState((prev) => {
      prev.onHide?.();
      return { ...prev, visible: false };
    });
  }, []);

  const openSidebar = useCallback((options: SidebarOptions) => {
    setState({
      ...defaultState,
      ...options,
      visible: true,
    });
  }, []);

  const updateSidebar = useCallback((options: Partial<SidebarOptions>) => {
    setState((prev) => ({
      ...prev,
      ...options,
    }));
  }, []);

  const value = useMemo(
    () => ({ openSidebar, closeSidebar, updateSidebar }),
    [openSidebar, closeSidebar, updateSidebar],
  );

  return (
    <SidebarContext.Provider value={value}>
      {children}

      <Sidebar
        visible={state.visible}
        onHide={closeSidebar}
        position={state.position}
        header={state.header}
        style={state.style}
        className={state.className}
        showCloseIcon={state.showCloseIcon}
        blockScroll={state.blockScroll}
      >
        {state.content}
      </Sidebar>
    </SidebarContext.Provider>
  );
}
