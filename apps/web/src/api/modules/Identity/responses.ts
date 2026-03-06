export type RegisterResponse = { registerSuccess: boolean };
export type LoginResponse = { accessToken: string; refreshToken: string };
export type ForgetPasswordResponse = {
  refreshPasswordSuccess: boolean;
};
