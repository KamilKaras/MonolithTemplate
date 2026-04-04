import type { AxiosError, AxiosInstance, AxiosRequestConfig } from "axios";
import axios from "axios";
import qs from "qs";
import type { ApiError, ProblemDetails } from "../shared/errors/types";
import { getToken } from "../store/store";
import type { RequestParams } from "./types";

class ApiClient {
  private defaultErrorMessage = "Wystąpił błąd podczas komunikacji z serwerem!";

  private api: AxiosInstance;

  constructor(baseURL: string) {
    this.api = axios.create({
      baseURL,
      withCredentials: true,
      timeout: 30_000,
    });

    this.api.interceptors.request.use((config) => {
      const token = getToken();
      if (token) {
        config.headers = config.headers ?? {};
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    });

    this.api.interceptors.response.use(
      (response) => response,
      (error) => {
        const mappedError = this.mapError(error);

        if (mappedError.status === 401) {
          localStorage.removeItem("access_token");
          window.location.href = "/login";
        }

        return Promise.reject(mappedError);
      },
    );
  }

  private mapError(error: unknown): ApiError {
    if (axios.isCancel(error)) {
      return {
        status: 499,
        title: "Żądanie zostało anulowane.",
      };
    }

    if (!axios.isAxiosError(error)) {
      return {
        status: 500,
        title: this.defaultErrorMessage,
      };
    }

    const ax = error as AxiosError<ProblemDetails>;
    const status = ax.response?.status;

    if (!ax.response) {
      return {
        status: 500,
        title: "Brak odpowiedzi z serwera. Sprawdź połączenie.",
      };
    }

    const data = ax.response.data;

    return {
      status,
      title: data?.title ?? data?.message ?? this.defaultErrorMessage,
      detail: data?.detail,
      errors: data?.errors,
    };
  }

  private async request<T>(
    url: string,
    method: "get" | "post" | "patch" | "delete",
    params: RequestParams,
    config: AxiosRequestConfig = {},
  ): Promise<T> {
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
    return data;
  }

  get<T>(
    url: string,
    params: RequestParams,
    config: AxiosRequestConfig = {},
  ): Promise<T> {
    return this.request(url, "get", params, config);
  }

  patch<T>(
    url: string,
    params: RequestParams,
    config: AxiosRequestConfig = {},
  ): Promise<T> {
    return this.request(url, "patch", params, config);
  }

  post<T>(
    url: string,
    params: RequestParams,
    config: AxiosRequestConfig = {},
  ): Promise<T> {
    return this.request(url, "post", params, config);
  }

  delete<T>(
    url: string,
    params: RequestParams,
    config: AxiosRequestConfig = {},
  ): Promise<T> {
    return this.request(url, "delete", params, config);
  }
}

export const apiClient = new ApiClient(import.meta.env.VITE_API_URL);
