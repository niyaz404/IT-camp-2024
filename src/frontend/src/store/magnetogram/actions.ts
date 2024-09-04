import { getRandomColor } from "../../utils";
import { getMagnetogramInfo, saveNewMagnetogram } from "../../api";
import {
  Defect,
  MagnetogramElementType,
  MarkerSide,
  StructuralElement,
  StructuralElementType,
  StructuralElementTypes,
} from "../../types";
import { authSelector } from "../auth";
import { AppDispatch, RootState } from "../store";
import { magnetogramSelector } from "./selectors";
import { magnetogramSlice } from "./slice";
import { castDefect, castStructuralElement } from "./utils";
import { LeftRightSplit } from "./types";

/**
 * Подгружает данные для реестра отчетов
 */
export const loadMagnetogramById =
  (magnetogramId: string | undefined) => async (dispatch: AppDispatch) => {
    dispatch(magnetogramSlice.actions.replaceIsMagnetogramLoading(true));
    try {
      const data = await getMagnetogramInfo(magnetogramId);

      dispatch(magnetogramSlice.actions.replaceMagnetogramId(data.id));
      dispatch(magnetogramSlice.actions.replaceName(data.name));
      dispatch(
        magnetogramSlice.actions.replaceMagnetogramImage(
          `data:image/jpeg;base64,${data.processedImage}`
        )
      );
      dispatch(magnetogramSlice.actions.replaceDefects(data.defects));

      dispatch(сalculateDefectInfo(data.defects));

      dispatch(
        magnetogramSlice.actions.replaceStructuralElements(
          data.structuralElements
        )
      );

      dispatch(magnetogramSlice.actions.replaceIsMagnetogramLoading(false));
    } catch (error) {
      alert("Ошибка загрузки данных");
      console.error(error);
    }
  };

/**
 * Добавляет новый элемент магнитограмы
 * @param type тип элемента
 * @param leftCoordinateX левая кооррдината по оси Х
 * @param rightCoordinateX правая кооррдината по оси Х
 * @param discrition описание
 * @param structuralElementType тип конструктивного элемента
 */
export const addMagnetogramElement =
  (
    type: MagnetogramElementType,
    leftCoordinateX: number,
    rightCoordinateX: number,
    description?: string,
    structuralElementType?: StructuralElementType
  ) =>
  (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();
    const { defects, structuralElements } = magnetogramSelector(state);

    if (type === "defect") {
      const newDefect: Defect = {
        id: "",
        description: description ?? "",
        type: type,
        leftCoordinateX: leftCoordinateX,
        rightCoordinateX: rightCoordinateX,
        markerColor: getRandomColor(),
        isEditable: false,
      };
      dispatch(
        magnetogramSlice.actions.replaceDefects([...defects, newDefect])
      );
    } else {
      const newStructuralElement: StructuralElement = {
        id: "",
        type: type,
        leftCoordinateX: leftCoordinateX,
        rightCoordinateX: rightCoordinateX,
        markerColor: getRandomColor(),
        isEditable: false,
        structuralElementType:
          structuralElementType ?? StructuralElementTypes[0],
      };

      dispatch(
        magnetogramSlice.actions.replaceStructuralElements([
          ...structuralElements,
          newStructuralElement,
        ])
      );
      dispatch(сalculateDefectInfo(defects));
    }
  };

/**
 * Обновляет координаты элемента магнитограммы
 * @param magnetogramElement элемент магнитограммы
 * @param coordinateX координата
 */
export const updateElementCoordinates =
  (
    magnetogramElementId: string,
    type: MagnetogramElementType,
    coordinateX: number,
    side: MarkerSide
  ) =>
  (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();
    const { defects, structuralElements } = magnetogramSelector(state);

    // Передвижение дефекта
    if (type === "defect") {
      const currentDefect = defects.find(
        (defect) => defect.id === magnetogramElementId
      );

      if (currentDefect) {
        // В зависимости от side менять либо левую коорнидату, либо правую
        const newCurrentDefect: Defect =
          side === "left"
            ? {
                ...currentDefect,
                leftCoordinateX: coordinateX,
              }
            : {
                ...currentDefect,
                rightCoordinateX: coordinateX,
              };
        dispatch(magnetogramSlice.actions.replaceDefect(newCurrentDefect));
      }
    }

    // Передвижение конструктивного элемента
    if (type === "structuralElement") {
      const currentStructuralElement = structuralElements.find(
        (structuralElement) => structuralElement.id === magnetogramElementId
      );

      if (currentStructuralElement) {
        // В зависимости от side менять либо левую коорнидату, либо правую
        const newCurrentStructuralElement: StructuralElement =
          side === "left"
            ? {
                ...currentStructuralElement,
                leftCoordinateX: coordinateX,
              }
            : {
                ...currentStructuralElement,
                rightCoordinateX: coordinateX,
              };
        dispatch(
          magnetogramSlice.actions.replaceStructuralElement(
            newCurrentStructuralElement
          )
        );
      }
    }
    dispatch(сalculateDefectInfo(defects));
  };

/**
 * Устанавливает признак видимости структурных элементов
 * @param isVisible признак видимости структурных элементов
 */
export const setIsStructuralElementsVisible =
  (isVisible: boolean) => (dispatch: AppDispatch) => {
    dispatch(
      magnetogramSlice.actions.replaceIsStructuralElementsVisible(isVisible)
    );
  };

/**
 *  Устанавливает признак видимости дефектов
 * @param isVisible признак видимости дефектов
 */
export const setIsDefectsVisible =
  (isVisible: boolean) => (dispatch: AppDispatch) => {
    dispatch(magnetogramSlice.actions.replaceIsDefectsVisible(isVisible));
  };

/**
 * Сохраняет измененеия пользователя для создания новой версии магнитограммы
 */
export const saveMagnetogram =
  () => (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();
    const { defects, id, processedImage, structuralElements, name } =
      magnetogramSelector(state);
    const { currentUser } = authSelector(state);
    try {
      saveNewMagnetogram(
        id,
        new Date(),
        name,
        currentUser?.userName ?? "",
        !!defects.length,
        defects.map(castDefect),
        structuralElements.map(castStructuralElement),
        processedImage
      );
    } catch (error) {
      alert("Ошибка сохранения изменений");
      console.error(error);
    }
  };

/**
 * Удаляет элемент магнитограммы
 * @param magnetogramElementId идентификатор элемента магнитограммы
 * @param type тип элемента магнитограммы
 */
export const removeMagnetogramElement =
  (magnetogramElementId: string, type: MagnetogramElementType) =>
  (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();
    const { defects, structuralElements } = magnetogramSelector(state);

    if (type === "defect") {
      const filtredDefects = defects.filter(
        (defect) => defect.id !== magnetogramElementId
      );
      dispatch(magnetogramSlice.actions.replaceDefects(filtredDefects));
    }
    if (type === "structuralElement") {
      const filtredStructuralElements = structuralElements.filter(
        (structuralElement) => structuralElement.id !== magnetogramElementId
      );
      dispatch(
        magnetogramSlice.actions.replaceStructuralElements(
          filtredStructuralElements
        )
      );
    }
    dispatch(сalculateDefectInfo(defects));
  };

/**
 * Заменяет признак блокировки элемента магнитограммы на противоположный
 * @param magnetogramElementId идентификатор элемента магнитограммы
 * @param type тип элемента магнитограммы
 */
export const reverseMagnetogramElementEnable =
  (magnetogramElementId: string, type: MagnetogramElementType) =>
  (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();
    const { defects, structuralElements } = magnetogramSelector(state);

    if (type === "defect") {
      const currentDefect = defects.find(
        (defect) => defect.id === magnetogramElementId
      );
      if (currentDefect) {
        dispatch(
          magnetogramSlice.actions.replaceDefect({
            ...currentDefect,
            isEditable: !currentDefect.isEditable,
          })
        );
      }
    }
    if (type === "structuralElement") {
      const currentStructuralElement = structuralElements.find(
        (structuralElement) => structuralElement.id === magnetogramElementId
      );
      if (currentStructuralElement) {
        dispatch(
          magnetogramSlice.actions.replaceStructuralElement({
            ...currentStructuralElement,
            isEditable: !currentStructuralElement.isEditable,
          })
        );
      }
    }
  };

/**
 * Для каждого дефекта расчитывает и устанавивает количество структурных элементов справа и слева
 */
export const сalculateDefectInfo =
  (defects: Defect[]) => (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();
    const { structuralElements } = magnetogramSelector(state);

    const newDefects = defects.map((defect) => {
      const { left, right } = structuralElements.reduce(
        (acc: LeftRightSplit, structuralElement) => {
          if (structuralElement.leftCoordinateX <= defect.leftCoordinateX) {
            acc.left.push(structuralElement);
          } else {
            acc.right.push(structuralElement);
          }
          return acc;
        },
        {
          left: [],
          right: [],
        }
      );

      const result: Defect = {
        ...defect,
        leftStructuralElementCount: {
          None: 0,
          WeldSeam: calculateCount(left, "WeldSeam"),
          Bend: calculateCount(left, "Bend"),
          Branching: calculateCount(left, "Branching"),
          Patch: calculateCount(left, "Patch"),
        },
        rightStructuralElementCount: {
          None: 0,
          WeldSeam: calculateCount(right, "WeldSeam"),
          Bend: calculateCount(right, "Bend"),
          Branching: calculateCount(right, "Branching"),
          Patch: calculateCount(right, "Patch"),
        },
      };

      return result;
    });

    dispatch(magnetogramSlice.actions.replaceDefects(newDefects));
    newDefects;
  };

/**
 * Вычисляет количество структурных элементов определенного типа
 * @param arr массив структурных элементов
 * @param structuralElementType тип структурного элемента
 * @returns количество структурных элементов
 */
const calculateCount = (
  arr: StructuralElement[],
  structuralElementType: StructuralElementType
) => {
  return arr.reduce((acc, item) => {
    return item.structuralElementType === structuralElementType ? acc + 1 : acc;
  }, 0);
};
