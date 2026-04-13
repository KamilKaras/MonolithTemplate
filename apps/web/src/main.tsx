import "primeicons/primeicons.css";
import "primereact/resources/themes/lara-light-cyan/theme.css";
import { createRoot } from "react-dom/client";
import App from "./app/App.tsx";
import "./app/theme/themes/mytheme/theme.scss";
import "./index.scss";
import AppProviders from "./shared/providers/AppProviders.tsx";

createRoot(document.getElementById("root")!).render(
  <AppProviders>
    <App />
  </AppProviders>,
);
