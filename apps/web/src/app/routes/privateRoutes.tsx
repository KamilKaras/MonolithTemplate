import { Outlet, type RouteObject } from "react-router-dom";
import HomePage from "../../components/pages/HomePage/HomePage";
import MainPage from "../../components/templates/MainPage/MainPage";
import { RequireAuth } from "./auth-guards/RequireAuth";

export const privateRoutes: RouteObject[] = [
  {
    element: <RequireAuth />,
    children: [
      {
        element: (
          <MainPage>
            <Outlet />
          </MainPage>
        ),
        children: [
          {
            path: "/home",
            element: <HomePage />,
          },
        ],
      },
    ],
  },
];
