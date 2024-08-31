import { ElementEdgeProps } from "./types";
import React, { FC, MouseEvent } from "react";
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

  const onRightButtonMouseClick = (event: MouseEvent<HTMLDivElement>) => {
    event.preventDefault();
    return <>asdsadsasa</>;
  };

  const defectTooltipText = `Ближайший левый конструктивный элемент: Сварной шов №1. Ближайший правый конструктивный элемент: Сварной шов №2. Координата: ${coordinate}`;

  const structuralElementTooltipText = `Тип конструктивного элемента: Заплатка. Координата: ${coordinate}`;

  const tooltipText =
    type === "defect" ? defectTooltipText : structuralElementTooltipText;

  return (
    <Draggable
      axis="x"
      onStop={onDragStop}
      defaultPosition={{ x: (coordinate - leftOffset) * 4, y: 0 }}
      position={{ x: coordinate * 4, y: 0 }}
    >
      <div className={css.elementEdge} onContextMenu={onRightButtonMouseClick}>
        <SvgWithTooltip tooltipText={tooltipText} />
      </div>
    </Draggable>
  );
};
