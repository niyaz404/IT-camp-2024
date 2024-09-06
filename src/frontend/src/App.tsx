import { Theme, presetGpnDefault } from "@consta/uikit/Theme";
import { Routers } from "./module";
import React from "react";
import css from "./App.css";

const App: React.FC = () => (
  <Theme preset={presetGpnDefault}>
    <div className={css.app}>
      <Routers />
    </div>
  </Theme>
);

export default App;
