// Dialog/DialogProvider.tsx
import { Dialog } from "primereact/dialog";
import { useCallback, useMemo, useState, type ReactNode } from "react";
import { DialogContext } from "./hooks/useAppDialog";
import { defaultState, type DialogOptions } from "./types";

export function DialogProvider({ children }: { children: ReactNode }) {
  const [state, setState] = useState(defaultState);

  const closeDialog = useCallback(() => {
    setState((prev) => {
      prev.onHide?.();
      return { ...prev, visible: false };
    });
  }, []);

  const openDialog = useCallback((options: DialogOptions) => {
    setState({
      ...defaultState,
      ...options,
      visible: true,
    });
  }, []);

  const updateDialog = useCallback((options: Partial<DialogOptions>) => {
    setState((prev) => ({
      ...prev,
      ...options,
    }));
  }, []);

  const value = useMemo(
    () => ({ openDialog, closeDialog, updateDialog }),
    [openDialog, closeDialog, updateDialog],
  );

  return (
    <DialogContext.Provider value={value}>
      {children}

      <Dialog
        visible={state.visible}
        onHide={closeDialog}
        header={state.header}
        style={state.style}
        className={state.className}
        showCloseIcon={state.showCloseIcon}
        blockScroll={state.blockScroll}
      >
        {state.content}
      </Dialog>
    </DialogContext.Provider>
  );
}
