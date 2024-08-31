import { MagnetogramElement, MagnetogramElementType } from "../../types";
import { AppDispatch, RootState } from "../store";
import { magnetogramSlice } from "./slice";

/**
 * Добавляет новый элемент магнитограмы
 * @param discrition описание
 * @param leftCoordinateX левая кооррдинато по оси Х
 * @param rightCoordinateX правая кооррдинато по оси Х
 */
export const addMagnetogramElement =
  (description: string, type: MagnetogramElementType, coordinateX: number) =>
  (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();

    const getRandomNum = () => {
      const min = 0;
      const max = 255;
      return Math.floor(Math.random() * (max - min + 1)) + min;
    };

    const newMagnetogramElement: MagnetogramElement = {
      id: "new",
      description: description,
      type: type,
      coordinateX: coordinateX,
      markerColor: `rgb(${getRandomNum()},${getRandomNum()},${getRandomNum()})`,
      isEditable: false,
    };

    if (type === "defect") {
      dispatch(magnetogramSlice.actions.replaceDefects(newMagnetogramElement));
    } else {
      dispatch(
        magnetogramSlice.actions.replaceStructuralElements(
          newMagnetogramElement
        )
      );
    }
  };

export const updateElementCoordinate =
  (magnetogramElement: MagnetogramElement, coordinateX: number) =>
  (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();

    const newMagnetogramElement: MagnetogramElement = {
      ...magnetogramElement,
      coordinateX,
    };

    if (magnetogramElement.type === "defect") {
      dispatch(magnetogramSlice.actions.replaceDefect(newMagnetogramElement));
    }

    if (magnetogramElement.type === "structuralElement") {
      dispatch(
        magnetogramSlice.actions.replaceStructuralElement(newMagnetogramElement)
      );
    }
  };

export const setIsStructuralElementsVisible =
  (isVisible: boolean) => (dispatch: AppDispatch) => {
    dispatch(
      magnetogramSlice.actions.replaceIsStructuralElementsVisible(isVisible)
    );
  };

export const setIsDefectsVisible =
  (isVisible: boolean) => (dispatch: AppDispatch) => {
    dispatch(magnetogramSlice.actions.replaceIsDefectsVisible(isVisible));
  };
