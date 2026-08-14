import type { RouteObject } from "react-router-dom";
import HomePage from "../../components/pages/HomePage/HomePage";
import { RequireAuth } from "./auth-guards/RequireAuth";

export const privateRoutes: RouteObject[] = [
  {
    element: <RequireAuth />,
    children: [
      {
        path: "/home",
        element: <HomePage />,
      },
    ],
  },
];
