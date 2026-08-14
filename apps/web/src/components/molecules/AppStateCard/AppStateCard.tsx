import { Card } from "primereact/card";
import AppButton from "../../atoms/AppButton/AppButton";
import "./app-state-card.scss";
import type { AppStateCardProps } from "./types";

const AppStateCard = ({
  eyebrow = "MonolithTemplate",
  title,
  message,
  actionLabel,
  onAction,
  secondaryActionLabel,
  onSecondaryAction,
}: AppStateCardProps) => {
  return (
    <Card className="app-state-card">
      <p className="app-state-card__eyebrow">{eyebrow}</p>
      <h3 className="app-state-card__title">{title}</h3>
      <p className="app-state-card__message">{message}</p>

      {actionLabel || secondaryActionLabel ? (
        <div className="app-state-card__actions">
          {actionLabel && onAction ? (
            <AppButton label={actionLabel} onClick={onAction} />
          ) : null}
          {secondaryActionLabel && onSecondaryAction ? (
            <button
              type="button"
              className="app-state-card__secondary-action"
              onClick={onSecondaryAction}
            >
              {secondaryActionLabel}
            </button>
          ) : null}
        </div>
      ) : null}
    </Card>
  );
};

export default AppStateCard;
