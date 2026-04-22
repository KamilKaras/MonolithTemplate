import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from "yup";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import AppPasswordText from "../../../../../components/molecules/AppPasswordText/AppPasswordText";
import { useUserParams } from "../../../../../shared/hooks/useSearchParams";
import { useResetPassword } from "../../hooks/useResetPassword";
import {
  newPasswordInitialFormValues,
  type NewPasswordFormValues,
} from "../../types";

const ResetPasswordForm = () => {
  const { token, userId } = useUserParams();

  const loginSchema = Yup.object({
    password: Yup.string().min(8, "Min 8 znaków").required("Pole wymagane"),
    confirmPassword: Yup.string()
      .oneOf([Yup.ref("password")], "Hasła muszą być takie same")
      .min(8, "Min 8 znaków")
      .required("Pole wymagane"),
  });

  const formik = useFormik<NewPasswordFormValues>({
    initialValues: newPasswordInitialFormValues,
    validationSchema: loginSchema,
    onSubmit: (values) => {
      mutateAsync({
        password: values.password!,
        confirmPassword: values.confirmPassword!,
        token: token!,
        userId: userId!,
      });
    },
  });

  const { isPending, mutateAsync } = useResetPassword();

  return (
    <FormikProvider value={formik}>
      <Form className="forgot-password-form" noValidate>
        <AppPasswordText
          placeholder="Hasło"
          value={formik.values.password}
          onChange={(v) => formik.setFieldValue("password", v)}
          error={formik.errors.password}
        />
        <AppPasswordText
          placeholder="Potwierdź hasło"
          value={formik.values.confirmPassword}
          onChange={(v) => formik.setFieldValue("confirmPassword", v)}
          error={formik.errors.confirmPassword}
        />

        <div className="login-form buttons-container">
          <AppButton loading={isPending} type="submit" label="Resetuj hasło" />
        </div>
      </Form>
    </FormikProvider>
  );
};

export default ResetPasswordForm;
