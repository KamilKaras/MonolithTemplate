import { useLogout } from "../../../features/identity/logout/hooks/useLogout";
import { useAuth } from "../../../shared/hooks/useAuth";
import { AppIconButton } from "../../atoms/AppIconButton/AppIconButton";
import PageLoader from "../../molecules/PageLoader/PageLoader";

const HeaderActions = () => {
  const { isPending, mutateAsync } = useLogout();
  const { isAuthenticated, isLoading } = useAuth();

  const loading = isPending || isLoading;

  if (loading) return <PageLoader visible />;

  return (
    <div>
      <AppIconButton
        disabled={!isAuthenticated}
        tooltip="Wyloguj"
        icon="POWER_OFF"
        onClick={async () => mutateAsync()}
      />
    </div>
  );
};

export default HeaderActions;
