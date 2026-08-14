import HeaderActions from "../../organisms/HeaderActions/HeaderActions";
import MainPageHeader from "../../organisms/MainPageHeader/MainPageHeader";
import "./main-page.scss";
import type { MainPageProps } from "./types";

const MainPage = (props: MainPageProps) => {
  const { children } = props;
  return (
    <div className="main-page">
      <MainPageHeader rightContent={<HeaderActions />} />
      <div className="main-page-content">{children}</div>
    </div>
  );
};

export default MainPage;
