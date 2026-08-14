import "./main-page-header.scss";
import type { MainPageHeaderProps } from "./types";
const MainPageHeader = (props: MainPageHeaderProps) => {
  const { brandLabel, brandDescription, navigation, rightContent } = props;
  return (
    <div className="main-page-header">
      <div className="main-page-header__brand">
        <span className="main-page-header__brand-mark">MT</span>
        <div className="main-page-header__brand-copy">
          <span className="main-page-header__brand-label">{brandLabel}</span>
          <span className="main-page-header__brand-description">
            {brandDescription}
          </span>
        </div>
      </div>

      <nav className="main-page-header__nav" aria-label="Primary">
        {navigation}
      </nav>

      <div className="main-page-header__right-content">{rightContent}</div>
    </div>
  );
};

export default MainPageHeader;
