export type ForgerPasswordFormValues = {
  email: string | null;
};

export const forgetPasswordInitialFormValues: ForgerPasswordFormValues = {
  email: null,
};

export type NewPasswordFormValues = {
  password: string | null;
  confirmPassword: string | null;
};

export const newPasswordInitialFormValues: NewPasswordFormValues = {
  password: null,
  confirmPassword: null,
};
