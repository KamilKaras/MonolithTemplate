import { Navigate, Outlet, useLocation } from "react-router-dom";
import PageLoader from "../../../components/molecules/PageLoader/PageLoader";
import { buildReturnUrl } from "../../../shared/auth/safeReturnUrl";
import { useAuth } from "../../../shared/hooks/useAuth";

export const RequireAuth = () => {
  const { isLoading, isAuthenticated, hasAuthCheckError } = useAuth();
  const location = useLocation();

  if (isLoading) {
    return <PageLoader visible />;
  }

  if (hasAuthCheckError) {
    return <Navigate to="/problem" replace />;
  }

  if (!isAuthenticated) {
    const returnUrl = buildReturnUrl(
      location.pathname,
      location.search,
      location.hash,
    );

    return (
      <Navigate
        to={`/login?returnUrl=${returnUrl}`}
        replace
        state={{ from: location }}
      />
    );
  }

  return <Outlet />;
};
