import "primereact/resources/themes/lara-light-cyan/theme.css";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./app/App.tsx";
import "./app/theme/themes/mytheme/theme.scss";
import "./index.scss";
import AppProviders from "./shared/providers/AppProviders.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AppProviders>
      <App />
    </AppProviders>
  </StrictMode>,
);
