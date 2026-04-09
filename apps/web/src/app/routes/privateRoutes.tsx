import type { RouteObject } from "react-router-dom";
import HomeTailsPage from "../../components/pages/HomeTailsPage/HomeTailsPage";
import { RequireAuth } from "./authGuards/RequireAuth";

export const privateRoutes: RouteObject[] = [
  {
    element: <RequireAuth />,
    children: [
      {
        path: "/home",
        element: <HomeTailsPage />,
      },
    ],
  },
];
