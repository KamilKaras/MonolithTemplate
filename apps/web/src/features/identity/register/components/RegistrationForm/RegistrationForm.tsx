import { Form, FormikProvider, useFormik } from "formik";
import { Link } from "react-router-dom";
import * as Yup from "yup";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import AppInputText from "../../../../../components/molecules/AppInputText/AppInputText";
import AppPasswordText from "../../../../../components/molecules/AppPasswordText/AppPasswordText";
import { useRegister } from "../../hooks/useRegister";
import {
  registrationInitialFormValues,
  type RegistrationFormValues,
} from "../../types";
import "./registration-form.scss";

const RegistrationForm = () => {
  const registrationSchema = Yup.object({
    userName: Yup.string()
      .min(2, "Pole wymaga minimum 2 znaków")
      .required("Pole wymagane"),
    email: Yup.string().email("Niepoprawny email").required("Pole wymagane"),
    password: Yup.string().min(8, "Min 8 znaków").required("Pole wymagane"),
    confirmPassword: Yup.string()
      .oneOf([Yup.ref("password")], "Hasła muszą być takie same")
      .min(8, "Min 8 znaków")
      .required("Pole wymagane"),
  });

  const formik = useFormik<RegistrationFormValues>({
    initialValues: registrationInitialFormValues,
    validationSchema: registrationSchema,
    onSubmit: async (values) => {
      await mutateAsync({
        userName: values.userName!,
        confirmPassword: values.confirmPassword!,
        password: values.password!,
        email: values.email!,
      });
    },
  });

  const { isPending, mutateAsync } = useRegister(() => formik.resetForm());

  return (
    <FormikProvider value={formik}>
      <Form className="registration-form auth-form" noValidate>
        <AppInputText
          label="Full name"
          placeholder="Alex Morgan"
          value={formik.values.userName}
          onChange={(v) => formik.setFieldValue("userName", v)}
          error={formik.errors.userName}
          autoComplete="name"
          required
        />
        <AppInputText
          label="Email address"
          placeholder="you@example.com"
          value={formik.values.email}
          onChange={(v) => formik.setFieldValue("email", v)}
          error={formik.errors.email}
          autoComplete="email"
          required
        />
        <AppPasswordText
          label="Password"
          placeholder="Create a secure password"
          value={formik.values.password}
          onChange={(v) => formik.setFieldValue("password", v)}
          error={formik.errors.password}
          hint="Use at least 8 characters."
        />
        <AppPasswordText
          label="Confirm password"
          placeholder="Repeat the password"
          value={formik.values.confirmPassword}
          onChange={(v) => formik.setFieldValue("confirmPassword", v)}
          error={formik.errors.confirmPassword}
        />
        <div className="registration-form__actions">
          <AppButton
            className="auth-form__submit"
            loading={isPending}
            type="submit"
            label="Create account"
          />
        </div>
        <p className="auth-form__supporting-copy">
          Already registered? <Link to="/login">Sign in</Link>
        </p>
      </Form>
    </FormikProvider>
  );
};

export default RegistrationForm;
