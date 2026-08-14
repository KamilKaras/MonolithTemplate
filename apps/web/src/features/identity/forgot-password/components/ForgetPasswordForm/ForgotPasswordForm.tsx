import { Form, FormikProvider, useFormik } from "formik";
import { Link } from "react-router-dom";
import * as Yup from "yup";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import AppInputText from "../../../../../components/molecules/AppInputText/AppInputText";
import { useForgetPassword } from "../../hooks/useForgetPassword";
import {
  forgetPasswordInitialFormValues,
  type ForgerPasswordFormValues,
} from "../../types";
import "./forgot-password-form.scss";

const ForgotPasswordForm = () => {
  const loginSchema = Yup.object({
    email: Yup.string()
      .email("Niepoprawny email")
      .required("Email jest wymagany"),
  });

  const formik = useFormik<ForgerPasswordFormValues>({
    initialValues: forgetPasswordInitialFormValues,
    validationSchema: loginSchema,
    onSubmit: (values) => {
      mutateAsync({
        email: values.email!,
      });
    },
  });

  const { isPending, mutateAsync } = useForgetPassword(() =>
    formik.resetForm(),
  );

  return (
    <FormikProvider value={formik}>
      <Form className="forgot-password-form auth-form" noValidate>
        <AppInputText
          label="Email address"
          placeholder="you@example.com"
          value={formik.values.email}
          onChange={(v) => formik.setFieldValue("email", v)}
          error={formik.errors.email}
          autoComplete="email"
          required
        />

        <div className="forgot-password-form__actions">
          <AppButton
            className="auth-form__submit"
            loading={isPending}
            type="submit"
            label="Send recovery email"
          />
        </div>
        <p className="auth-form__supporting-copy">
          Remembered it already? <Link to="/login">Back to sign in</Link>
        </p>
      </Form>
    </FormikProvider>
  );
};

export default ForgotPasswordForm;
