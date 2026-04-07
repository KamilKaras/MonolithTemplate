export type AuthState = {
  session: SessionProps | undefined;
};

export const initialAuthState: AuthState = {
  session: undefined,
};

export type SessionProps = {
  token: string;
};
