import { useUserCredentials } from "../../../features/identity/getUserCredentials/hooks/useUserConfirmEmail";
import PageLoader from "../../molecules/PageLoader/PageLoader";

const HomeTailsPage = () => {
  const { isPending } = useUserCredentials();
  if (isPending) {
    return <PageLoader visible />;
  }
  return <div>HomeTailsPage</div>;
};

export default HomeTailsPage;
