import { useMe } from "../../features/identity/me/hooks/useMe";

export const AuthGate = ({ children }: { children: React.ReactNode }) => {
  const { isLoading } = useMe();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return <>{children}</>;
};
