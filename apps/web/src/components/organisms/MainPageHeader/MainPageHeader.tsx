import "./main-page-header.scss";
import type { MainPageHeaderProps } from "./types";
const MainPageHeader = (props: MainPageHeaderProps) => {
  const { rightContent } = props;
  return (
    <div className="main-page-header">
      <div className="main-page-header__right-content">{rightContent}</div>
    </div>
  );
};

export default MainPageHeader;
