export type RegisterResponse = { registerSuccess: boolean };
export type LoginResponse = {
  token: string;
  userId: string;
};
export type ForgetPasswordResponse = {
  refreshPasswordSuccess: boolean;
};
export type ConfirmEmailResponse = {
  confirmed: boolean;
};

export type MeResponse = {
  user: {
    id: string;
    name: string;
    roles: [];
  };
};
