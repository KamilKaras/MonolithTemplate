import type { RouteObject } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";

export const privateRoutes: RouteObject[] = [
  {
    element: (
      <ProtectedRoute>
        <div></div>
      </ProtectedRoute>
    ),
    children: [
      {
        path: "/dashboard",
        element: <div></div>,
      },
    ],
  },
];
