import type { Toast } from "primereact/toast";

let toastRef: Toast | null = null;

export const setToastRef = (ref: Toast | null) => {
  toastRef = ref;
};

export const toastService = {
  success(message: string, life?: number) {
    toastRef?.show({
      severity: "success",
      summary: "Sukces",
      detail: message,
      life: life ?? 3000,
    });
  },

  error(message: string) {
    toastRef?.show({
      severity: "error",
      summary: "Błąd",
      detail: message,
      life: 4000,
    });
  },

  info(message: string) {
    toastRef?.show({
      severity: "info",
      summary: "Informacja",
      detail: message,
      life: 3000,
    });
  },
};
