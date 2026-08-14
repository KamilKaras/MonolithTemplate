import { Link } from "react-router-dom";
import { APP_NAME } from "../../../shared/branding/appIdentity";
import HeaderActions from "../../organisms/HeaderActions/HeaderActions";
import MainPageHeader from "../../organisms/MainPageHeader/MainPageHeader";
import "./main-page.scss";
import type { MainPageProps } from "./types";

const MainPage = (props: MainPageProps) => {
  const { children } = props;
  return (
    <div className="main-page">
      <MainPageHeader
        brandLabel={APP_NAME}
        brandDescription="Authenticated workspace"
        navigation={
          <>
            <Link
              to="/home"
              className="main-page-header__nav-link main-page-header__nav-link--active"
            >
              Home
            </Link>
            <Link to="/" className="main-page-header__nav-link">
              Landing
            </Link>
          </>
        }
        rightContent={<HeaderActions />}
      />
      <main className="main-page-content">{children}</main>
    </div>
  );
};

export default MainPage;
