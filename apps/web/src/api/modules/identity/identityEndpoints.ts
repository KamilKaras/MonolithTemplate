import { apiClient } from "../../client";
import type {
  ConfirmEmailRequest,
  ForgetPasswordRequest,
  LoginRequest,
  RegisterRequest,
  SetNewPasswordRequest,
} from "./requests";
import type {
  ConfirmEmailResponse,
  LoginResponse,
  MeResponse,
  RegisterResponse,
} from "./responses";

export const identityEndpoints = {
  me: () => apiClient.get<MeResponse>("/identity/me", undefined),

  register: (request: RegisterRequest) =>
    apiClient.post<RegisterResponse>("/identity/register", request),
  login: (request: LoginRequest) =>
    apiClient.post<LoginResponse>("/identity/login", request),
  forgetPassword: (request: ForgetPasswordRequest) =>
    apiClient.post("/identity/forgot-password", request),
  resetPassword: (request: SetNewPasswordRequest) =>
    apiClient.post("/identity/reset-password", request),
  confirmEmail: (request: ConfirmEmailRequest) =>
    apiClient.post<ConfirmEmailResponse>("/identity/confirm-email", request),
  logout: () => apiClient.post("/identity/logout", undefined),
};
