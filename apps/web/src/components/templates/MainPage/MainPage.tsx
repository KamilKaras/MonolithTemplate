import MainPageFooter from "../../organisms/MainPageFooter/MainPageFooter";
import MainPageHeader from "../../organisms/MainPageHeader/MainPageHeader";
import "./main-page.scss";
import type { MainPageProps } from "./types";

const MainPage = (props: MainPageProps) => {
  const { children } = props;
  return (
    <div className="main-page">
      <MainPageHeader />
      <div className="main-page-content">{children}</div>
      <MainPageFooter />
    </div>
  );
};

export default MainPage;
