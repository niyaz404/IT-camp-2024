import { MarkerProps } from "./types";
import React, { FC, MouseEvent, useEffect, useRef, useState } from "react";
import css from "./style.css";
import Draggable, { DraggableData, DraggableEvent } from "react-draggable";
import { withTooltip } from "../../hocs";
import { Popover } from "@consta/uikit/Popover";
import { Button } from "@consta/uikit/Button";
import { IconTrash } from "@consta/uikit/IconTrash";
import { IconLock } from "@consta/uikit/IconLock";
import { IconUnlock } from "@consta/uikit/IconUnlock";
import {
  defectSelector,
  structuralElementSelector,
  removeMagnetogramElement,
  reverseMagnetogramElementEnable,
  updateElementCoordinates,
  useAppDispatch,
  useAppSelector,
} from "../../store";
import { StructuralElementNames } from "../../types";

export const Marker: FC<MarkerProps> = ({
  id,
  color,
  coordinate,
  type,
  leftOffset,
  scrollOffset,
  side,
  isEditable,
  setCoordinate,
}) => {
  const markerRef = useRef<HTMLDivElement | null>(null);
  const draggableRef = useRef<Draggable>(null);
  const [isContextMenuVisible, setIsContextMenuVisible] =
    useState<boolean>(false);

  const currentDefect = useAppSelector((state) => defectSelector(state, id));

  const currentStructuralElement = useAppSelector((state) =>
    structuralElementSelector(state, id)
  );

  const dispatch = useAppDispatch();

  useEffect(() => {
    if (draggableRef.current) {
      // Получаем координату X при первом рендере
      const state = draggableRef.current.state as DraggableData;
      const coordinate = state.x;
      setCoordinate(side, coordinate);
    }
  }, [coordinate]);

  const defectPoints = "0,0 16,0 9,10 9,522 16,532 0,532 7,522 7,10";
  const structutalElementPoints =
    "0,8 8,0 16,8 9,16 9,526 16,534 8,542 0,534 7,526 7,16";

  const points = type === "defect" ? defectPoints : structutalElementPoints;

  const rightStructuralElementCount =
    currentDefect && currentDefect.rightStructuralElementCount;

  const leftStructuralElementCount =
    currentDefect && currentDefect.leftStructuralElementCount;

  const structuralElementType =
    currentStructuralElement && currentStructuralElement.structuralElementType;

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
    event.stopPropagation();
    setIsContextMenuVisible(true);
  };

  const onCloseContextMenu = () => {
    setIsContextMenuVisible(false);
  };

  const onLock = () => {
    dispatch(reverseMagnetogramElementEnable(id, type));
    onCloseContextMenu();
  };

  const onRemove = () => {
    dispatch(removeMagnetogramElement(id, type));
    onCloseContextMenu();
  };

  const onDragStop = (e: DraggableEvent) => {
    const newCoordinate =
      ((e as MouseEvent).clientX + scrollOffset - leftOffset) / 4;
    dispatch(updateElementCoordinates(id, type, newCoordinate, side));
  };

  const leftCounts = `Кол-во. структурных элементов слева: ${StructuralElementNames.WeldSeam} ${leftStructuralElementCount?.WeldSeam}, ${StructuralElementNames.Bend} ${leftStructuralElementCount?.Bend}, ${StructuralElementNames.Branching} ${leftStructuralElementCount?.Branching}, ${StructuralElementNames.Patch} ${leftStructuralElementCount?.Patch}`;

  const rightCounts = `Кол-во. структурных элементов справа: ${StructuralElementNames.WeldSeam} ${rightStructuralElementCount?.WeldSeam}, ${StructuralElementNames.Bend} ${rightStructuralElementCount?.Bend}, ${StructuralElementNames.Branching} ${rightStructuralElementCount?.Branching}, ${StructuralElementNames.Patch} ${rightStructuralElementCount?.Patch}`;

  const defectTooltipText = `${leftCounts}. ${rightCounts}. Координата: ${coordinate}`;

  const structuralElementTooltipText = `Тип конструктивного элемента: ${
    structuralElementType && StructuralElementNames[structuralElementType]
  }. Координата: ${coordinate}`;

  const tooltipText =
    type === "defect" ? defectTooltipText : structuralElementTooltipText;

  return (
    <>
      <Draggable
        axis="x"
        onStop={onDragStop}
        defaultPosition={{ x: (coordinate - leftOffset) * 4, y: 0 }}
        position={{ x: coordinate * 4, y: 0 }}
        cancel=".isDisable"
        ref={draggableRef}
      >
        <div
          className={`${css.marker} ${isEditable === false && "isDisable"}`}
          onContextMenu={onRightButtonMouseClick}
          ref={markerRef}
        >
          <SvgWithTooltip tooltipText={tooltipText} />
        </div>
      </Draggable>
      {isContextMenuVisible && (
        <Popover
          direction="rightStartUp"
          spareDirection="rightStartUp"
          offset="s"
          arrowOffset={0}
          onClickOutside={onCloseContextMenu}
          isInteractive={true}
          anchorRef={markerRef}
          equalAnchorWidth={false}
          placeholder={undefined}
          onPointerEnterCapture={undefined}
          onPointerLeaveCapture={undefined}
        >
          <div className={css.popover}>
            <Button
              className="m-b-2"
              label={isEditable === false ? "Разблокировать" : "Заблокировать"}
              size="xs"
              view="clear"
              iconLeft={isEditable === false ? IconUnlock : IconLock}
              onClick={onLock}
            />
            <Button
              label="Удалить"
              size="xs"
              view="clear"
              iconLeft={IconTrash}
              onClick={onRemove}
            />
          </div>
        </Popover>
      )}
    </>
  );
};
