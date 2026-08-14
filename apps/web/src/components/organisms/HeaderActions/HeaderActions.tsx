import { useLogout } from "../../../features/identity/logout/hooks/useLogout";
import { useAuth } from "../../../shared/hooks/useAuth";
import { AppIconButton } from "../../atoms/AppIconButton/AppIconButton";
import "./header-actions.scss";

const HeaderActions = () => {
  const { isPending, mutateAsync } = useLogout();
  const { isAuthenticated, isLoading, user } = useAuth();

  if (isLoading) return null;

  return (
    <div className="header-actions">
      <div className="header-actions__identity">
        <span className="header-actions__label">Signed in as</span>
        <strong className="header-actions__name">
          {isAuthenticated
            ? (user?.user.name ?? "Authenticated user")
            : "Guest"}
        </strong>
      </div>
      <AppIconButton
        disabled={!isAuthenticated || isPending}
        tooltip="Wyloguj"
        ariaLabel="Log out"
        icon="POWER_OFF"
        onClick={async () => mutateAsync()}
      />
    </div>
  );
};

export default HeaderActions;
