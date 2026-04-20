import type { RouteObject } from "react-router-dom";
import ForgotPasswordPage from "../../components/pages/ForgetPasswordPage/ForgotPasswordPage";
import LoginPage from "../../components/pages/LoginPage/LoginPage";
import ProblemOccurredPage from "../../components/pages/ProblemOccurredPage/ProblemOccurredPage";
import ResetPasswordPage from "../../components/pages/ResetPasswordPage/ResetPasswordPage";
import ConfirmPage from "../../features/identity/confirm-email/components/ConfirmPage/ConfirmPage";
import { RequireGuest } from "./auth-guards/RequireGuest";

export const publicRoutes: RouteObject[] = [
  {
    element: <RequireGuest />,
    children: [
      {
        path: "/login",
        element: <LoginPage />,
      },
      {
        path: "/confirm",
        element: <ConfirmPage />,
      },
      {
        path: "/forgot-password",
        element: <ForgotPasswordPage />,
      },
      {
        path: "/reset-password",
        element: <ResetPasswordPage />,
      },
      {
        path: "/problem",
        element: <ProblemOccurredPage />,
      },
    ],
  },
];
