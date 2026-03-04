import type { AxiosError, AxiosInstance, AxiosRequestConfig } from "axios";
import axios from "axios";
import qs from "qs";
import type { FetchResult } from "./types";

class ApiClient {
  private _errorMessage = "Wystąpił błąd podczas komunikacji z serwerem!";

  private api: AxiosInstance;

  constructor(baseURL: string) {
    this.api = axios.create({
      baseURL,
      withCredentials: true,
      timeout: 30_000,
    });

    this.api.interceptors.request.use((config) => {
      const token = localStorage.getItem("access_token");
      if (token) {
        config.headers = config.headers ?? {};
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    });
  }

  private mapError(error: unknown): string {
    if (axios.isCancel?.(error)) {
      return "Żądanie zostało anulowane.";
    }

    if (!axios.isAxiosError(error)) {
      return this._errorMessage;
    }

    const ax = error as AxiosError;

    if (!ax.response) {
      return "Brak odpowiedzi z serwera. Sprawdź połączenie.";
    }
    const message = axios.isAxiosError(error) && error.response?.data?.message;

    return message;
  }

  private async request<T>(
    url: string,
    method: "get" | "post" | "patch" | "delete",
    params: string | number | object | null | undefined,
    config: AxiosRequestConfig = {},
  ): Promise<FetchResult<T>> {
    try {
      const isQuery = method === "get" || method === "delete";
      const requestConfig: AxiosRequestConfig = {
        ...config,
        url,
        method,
        params: isQuery ? params : undefined,
        data: !isQuery ? params : undefined,
        paramsSerializer: isQuery
          ? {
              serialize: (params) =>
                qs.stringify(params, { skipNulls: true, allowDots: true }),
            }
          : undefined,
      };
      const { data } = await this.api.request<T>(requestConfig);
      return { ok: true, data };
    } catch (err) {
      return { ok: false, error: this.mapError(err) };
    }
  }

  get<T>(
    url: string,
    params: string | number | object | null | undefined = undefined,
    config: AxiosRequestConfig = {},
  ): Promise<FetchResult<T>> {
    return this.request(url, "get", params, config);
  }

  patch<T>(
    url: string,
    params: string | number | object | null | undefined = undefined,
    config: AxiosRequestConfig = {},
  ): Promise<FetchResult<T>> {
    return this.request(url, "patch", params, config);
  }

  post<T>(
    url: string,
    params: string | number | object | null | undefined = undefined,
    config: AxiosRequestConfig = {},
  ): Promise<FetchResult<T>> {
    return this.request(url, "post", params, config);
  }

  delete<T>(
    url: string,
    params: string | number | object | null | undefined = undefined,
    config: AxiosRequestConfig = {},
  ): Promise<FetchResult<T>> {
    return this.request(url, "delete", params, config);
  }
}

export const apiClient = new ApiClient(import.meta.env.VITE_API_URL);
