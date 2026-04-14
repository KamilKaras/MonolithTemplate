import { Form, FormikProvider, useFormik } from "formik";
import * as Yup from "yup";
import AppButton from "../../../../../components/atoms/AppButton/AppButton";
import AppInputText from "../../../../../components/molecules/AppInputText/AppInputText";
import { useForgetPassword } from "../../hooks/useForgetPassword";
import {
  forgetPasswordInitialFormValues,
  type ForgerPasswordFormValues,
} from "../../types";
import "./forget-password-form.scss";

const ForgetPasswordForm = () => {
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
      <Form className="forget-password-form" noValidate>
        <AppInputText
          placeholder="Email"
          value={formik.values.email}
          onChange={(v) => formik.setFieldValue("email", v)}
          error={formik.errors.email}
        />

        <div className="login-form buttons-container">
          <AppButton loading={isPending} type="submit" label="Resetuj hasło" />
        </div>
      </Form>
    </FormikProvider>
  );
};

export default ForgetPasswordForm;
