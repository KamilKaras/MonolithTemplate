import { TabPanel, TabView } from "primereact/tabview";
import type { AppTabViewerProps } from "./types";

const AppTabViewer = (props: AppTabViewerProps) => {
  const { tabs } = props;
  return (
    <TabView>
      {tabs.map((el, index) => {
        return (
          <TabPanel key={index} header={el.header}>
            {el.content}
          </TabPanel>
        );
      })}
    </TabView>
  );
};

export default AppTabViewer;
