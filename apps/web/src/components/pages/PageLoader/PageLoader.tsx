import "./page-loader.scss";
import type { PageLoaderProps } from "./types";

const PageLoader = (props: PageLoaderProps) => {
  const { loadingText = "Trwa ładowanie...", visible = false } = props;

  if (!visible) return null;

  return (
    <div className="page-loader">
      <div className="page-loader__spinner" />
      <p className="page-loader__text">{loadingText}</p>
    </div>
  );
};

export default PageLoader;
