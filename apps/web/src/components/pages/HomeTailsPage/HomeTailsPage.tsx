import { useMe } from "../../../features/identity/me/hooks/useMe";
import PageLoader from "../../molecules/PageLoader/PageLoader";

const HomeTailsPage = () => {
  const { isPending } = useMe();
  if (isPending) {
    return <PageLoader visible />;
  }
  return <div>HomeTailsPage</div>;
};

export default HomeTailsPage;
