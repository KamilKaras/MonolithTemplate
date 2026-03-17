import { apiClient } from "../../client";
import type {
  ConfirmEmailRequest,
  ForgetPasswordRequest,
  LoginRequest,
  RegisterRequest,
} from "./requests";
import type {
  ConfirmEmailResponse,
  ForgetPasswordResponse,
  LoginResponse,
  RegisterResponse,
} from "./responses";

export const identityApi = {
  register: (dto: RegisterRequest) =>
    apiClient.post<RegisterResponse>("/identity/register", dto),
  login: (dto: LoginRequest) =>
    apiClient.post<LoginResponse>("/identity/login", dto),
  forgetPassword: (dto: ForgetPasswordRequest) =>
    apiClient.post<ForgetPasswordResponse>("/identity/forget-password", dto),
  confirmEmail: (dto: ConfirmEmailRequest) =>
    apiClient.post<ConfirmEmailResponse>("/identity/confirm-email", dto),
};
