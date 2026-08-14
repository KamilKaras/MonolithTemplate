import { Card } from "primereact/card";
import AppButton from "../../atoms/AppButton/AppButton";
import type { AppStateCardProps } from "./types";

const AppStateCard = ({
  title,
  message,
  actionLabel,
  onAction,
}: AppStateCardProps) => {
  return (
    <Card title={title} subTitle={message}>
      {actionLabel && onAction ? (
        <AppButton label={actionLabel} onClick={onAction} />
      ) : null}
    </Card>
  );
};

export default AppStateCard;
