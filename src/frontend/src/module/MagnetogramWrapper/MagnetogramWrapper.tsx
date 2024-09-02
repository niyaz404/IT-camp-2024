import React, { FC, useEffect, useRef, useState, MouseEvent } from "react";
import { Marker } from "../../module";
import { useAppDispatch } from "../../store";
import css from "./style.css";
import { MagnetogramWrapperProps } from "./types";

export const MagnetogramWrapper: FC<MagnetogramWrapperProps> = ({
  children,
  elements,
  isDefectsCheked,
  isStructuralElementsCheked,
  onAddNewElement,
}) => {
  const magnetogramRef = useRef<HTMLDivElement | null>(null);
  const [leftOffset, setLeftOffset] = useState<number>(0);
  const [scrollOffset, setScrollOffset] = useState<number>(0);

  const dispatch = useAppDispatch();

  useEffect(() => {
    if (magnetogramRef.current) {
      // Значение левого отпступа картинки, которое было задано стилем магнитограммы
      const paddingLeftValue = parseInt(
        window.getComputedStyle(magnetogramRef.current).paddingLeft
      );

      setLeftOffset(paddingLeftValue);
    }
  }, []);

  useEffect(() => {
    magnetogramRef.current?.addEventListener("scroll", function () {
      magnetogramRef.current &&
        setScrollOffset(magnetogramRef.current?.scrollLeft);
    });

    return () => {
      magnetogramRef.current?.removeEventListener("scroll", function () {
        magnetogramRef.current &&
          setScrollOffset(magnetogramRef.current?.scrollLeft);
      });
    };
  }, [magnetogramRef.current]);

  const onRightButtonMouseClick = (event: MouseEvent<HTMLDivElement>) => {
    event.preventDefault();
    const coordinate = (event.clientX - leftOffset + scrollOffset) / 4;
    onAddNewElement(coordinate);
  };

  return (
    <div
      ref={magnetogramRef}
      className={css.magnetogramWrapper}
      onContextMenu={onRightButtonMouseClick}
    >
      {children}

      <div>
        {elements.map((element) => {
          return (
            <div
              className={`${
                element.type === "defect" ? css.edgesD : css.edgesS
              } ${
                (isDefectsCheked && element.type === "defect") ||
                (isStructuralElementsCheked &&
                  element.type === "structuralElement")
                  ? ""
                  : css.none
              }`}
            >
              <Marker
                id={element.id}
                color={element.markerColor}
                coordinate={element.leftCoordinateX}
                type={element.type}
                leftOffset={leftOffset}
                scrollOffset={scrollOffset}
                side="left"
              />
              <Marker
                id={element.id}
                color={element.markerColor}
                coordinate={element.rightCoordinateX}
                type={element.type}
                leftOffset={leftOffset}
                scrollOffset={scrollOffset}
                side="right"
              />
            </div>
          );
        })}
      </div>
    </div>
  );
};
