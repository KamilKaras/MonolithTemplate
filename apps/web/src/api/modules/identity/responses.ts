export type RegisterResponse = { registerSuccess: boolean };
export type LoginResponse = void;

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
