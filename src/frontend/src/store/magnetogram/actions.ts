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
  (
    description: string,
    type: MagnetogramElementType,
    leftCoordinateX: number,
    rightCoordinateX: number
  ) =>
  (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();

    const newMagnetogramElement: MagnetogramElement = {
      id: "",
      description: description,
      type: type,
      leftCoordinateX: leftCoordinateX,
      rightCoordinateX: rightCoordinateX,
    };

    if (type === "defect") {
      dispatch(magnetogramSlice.actions.replaceDefect(newMagnetogramElement));
    } else {
      dispatch(
        magnetogramSlice.actions.replaceStructuralElements(
          newMagnetogramElement
        )
      );
    }
  };
