import React, { FC } from "react";
import { MagnetogramWrapperProps } from "./types";
import css from "./style.css";
import { ElementEdge } from "../../components";

export const MagnetogramWrapper: FC<MagnetogramWrapperProps> = ({
  children,
  elements,
}) => {
  return (
    <div className={css.magnetogramWrapper}>
      {children}
      <div className={css.edges}>
        <div className={css.edgesContent}>
          {elements.map((element) => (
            <>
              <ElementEdge
                color={"yellow"}
                coordinate={element.rightCoordinateX}
              />
              <ElementEdge
                color={"yellow"}
                coordinate={element.leftCoordinateX}
              />
            </>
          ))}
        </div>
      </div>
    </div>
  );
};
