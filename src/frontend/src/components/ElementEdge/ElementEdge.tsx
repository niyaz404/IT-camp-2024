import { ElementEdgeProps } from "./types";
import React, { FC, useEffect, useState } from "react";
import css from "./style.css";
import Draggable from "react-draggable";
import { withTooltip } from "../../hocs";

export const ElementEdge: FC<ElementEdgeProps> = ({
  color,
  coordinate,
  onDragStop,
  type,
  leftOffset,
}) => {
  const defectPoints = "0,0 16,0 9,10 9,522 16,532 0,532 7,522 7,10";
  const structutalElementPoints =
    "0,8 8,0 16,8 9,16 9,526 16,534 8,542 0,534 7,526 7,16";

  const points = type === "defect" ? defectPoints : structutalElementPoints;

  const SvgWithTooltip = withTooltip(() => {
    return (
      <div>
        <svg width="18" height="564">
          <polygon
            points={points}
            fill={color}
            stroke="black"
            strokeWidth="1"
          />
        </svg>
      </div>
    );
  });

  return (
    <Draggable
      axis="x"
      onStop={onDragStop}
      defaultPosition={{ x: (coordinate - leftOffset) * 4, y: 0 }}
      position={{ x: coordinate * 4, y: 0 }}
    >
      <div className={css.elementEdge}>
        <SvgWithTooltip tooltipText={String(coordinate)} />
      </div>
    </Draggable>
  );
};
