export type RegisterRequest = {
  userName: string;
  email: string;
  password: string;
  confirmPassword: string;
};
export type LoginRequest = { email: string; password: string };
export type ForgetPasswordRequest = { email: string };
