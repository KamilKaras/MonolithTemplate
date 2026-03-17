import { useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import PageLoader from "../../../../../components/pages/PageLoader/PageLoader";
import { useUserConfirmEmail } from "../../hooks/useUserConfirmEmail";

const ConfirmPage = () => {
  const [searchParams] = useSearchParams();
  const userId = searchParams.get("userId");
  const token = searchParams.get("token");

  const { isPending, mutateAsync } = useUserConfirmEmail();

  useEffect(() => {
    if (!token || !userId) return;
    mutateAsync({ token, userId });
  }, [mutateAsync, token, userId]);

  return (
    <div>
      <PageLoader visible={isPending} />
    </div>
  );
};

export default ConfirmPage;
