import AppIconButton from "../../atoms/AppIconButton/AppIconButton";
import "./main-page-header.scss";
const MainPageHeader = () => {
  return (
    <div className="main-page-header">
      <div className="main-page-header__right">
        <AppIconButton
          icon="POWER_OFF"
          onClick={() => undefined}
          label="test"
        />
      </div>
    </div>
  );
};

export default MainPageHeader;
