import React, { FC, useEffect, useRef, useState, MouseEvent } from "react";
import { Marker } from "../../module";
import css from "./style.css";
import { MarkerSegmentProps } from "./types";
import { MarkerBackground } from "../../components";
import { MarkerSide } from "../../types";

export const MarkerSegment: FC<MarkerSegmentProps> = ({
  element,
  isDefectsCheked,
  isStructuralElementsCheked,
  leftOffset,
  scrollOffset,
}) => {
  const [width, setWidth] = useState<number>(0);
  const [leftCoordinate, setLeftCoordinate] = useState<number>(0);
  const [rightCoordinate, setRightCoordinate] = useState<number>(0);

  useEffect(() => {
    setWidth(Math.abs(rightCoordinate - leftCoordinate));
  }, [leftCoordinate, rightCoordinate]);

  const setCoordinate = (side: MarkerSide, coordinate: number) => {
    if (side === "left") {
      setLeftCoordinate(coordinate);
    }
    if (side === "right") {
      setRightCoordinate(coordinate);
    }
  };

  const elementStyle =
    element.type === "defect" ? css.defect : css.structuralElement;

  const isElementVisibleStyle =
    (isDefectsCheked && element.type === "defect") ||
    (isStructuralElementsCheked && element.type === "structuralElement")
      ? ""
      : css.displayNone;

  return (
    <div className={`${elementStyle} ${isElementVisibleStyle}`}>
      <Marker
        id={element.id}
        color={element.markerColor}
        coordinate={element.leftCoordinateX}
        type={element.type}
        leftOffset={leftOffset}
        scrollOffset={scrollOffset}
        side="left"
        isEditable={element.isEditable}
        setCoordinate={setCoordinate}
      />

      <MarkerBackground
        color={element.markerColor}
        leftOffset={leftCoordinate}
        width={width}
        type={element.type}
      />

      <Marker
        id={element.id}
        color={element.markerColor}
        coordinate={element.rightCoordinateX}
        type={element.type}
        leftOffset={leftOffset}
        scrollOffset={scrollOffset}
        side="right"
        isEditable={element.isEditable}
        setCoordinate={setCoordinate}
      />
    </div>
  );
};
