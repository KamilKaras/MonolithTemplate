import type { RouteObject } from "react-router-dom";
import ForgetPasswordPage from "../../components/pages/ForgetPasswordPage/ForgetPasswordPage";
import LoginPage from "../../components/pages/LoginPage/LoginPage";
import ConfirmPage from "../../features/identity/userConfirmEmail/components/ConfirmPage/ConfirmPage";
import { RequireGuest } from "./authGuards/RequireGuest";

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
        element: <ForgetPasswordPage />,
      },
    ],
  },
];
