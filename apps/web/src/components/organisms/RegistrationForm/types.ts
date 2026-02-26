export type RegistrationFormValues = {
  userName: string | null;
  email: string | null;
  password: string | null;
  confirmPassword: string | null;
};

export const registrationInitialFormValues: RegistrationFormValues = {
  userName: null,
  email: null,
  password: null,
  confirmPassword: null,
};
