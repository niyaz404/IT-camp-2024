import { formatCoordinate, getRandomColor } from "../../utils";
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
import { v4 as uuidv4 } from "uuid";

/**
 * Подгружает данные для реестра отчетов
 */
export const loadMagnetogramById =
  (magnetogramId: string | undefined) => async (dispatch: AppDispatch) => {
    dispatch(magnetogramSlice.actions.replaceIsMagnetogramLoading(true));
    try {
      const data = await getMagnetogramInfo(magnetogramId);

      dispatch(magnetogramSlice.actions.replaceCommitId(data.commitId));
      dispatch(
        magnetogramSlice.actions.replaceMagnetogramId(data.magnetogramId)
      );

      dispatch(magnetogramSlice.actions.replaceName(data.name));
      dispatch(
        magnetogramSlice.actions.replaceOriginalMagnetogramImage(
          // `data:image/jpeg;base64,${data.processedImage}`
          // "https://sun9-23.userapi.com/impg/kJ22PWyYRcZ0V-Lkww_tFPZnLLgt8gI5RBJISw/YRQLsjAcg9Y.jpg?size=1280x40&quality=96&sign=eab5cf28aac95eed962d744252f68ffa&type=album"
          "https://sun9-29.userapi.com/impg/NTJDGnhj-1aBEUJ9z0jT8zc5i4Uis8bk5TRoeQ/wAlso5jESrY.jpg?size=1280x40&quality=96&sign=e27f53dd0274d91594e1650708a0a80a&type=album"
        )
      );
      dispatch(
        magnetogramSlice.actions.replaceProcessedMagnetogramImage(
          // `data:image/jpeg;base64,${data.processedImage}`
          // "https://sun9-56.userapi.com/impg/1tRq08Du23PXRx6UsctCe_brx_bo5QU86fT1IA/CvVXmCbNBz0.jpg?size=1280x40&quality=96&sign=76263c9cbaa6e5b1edc0b6bf049de726&type=album"
          "https://psv4.userapi.com/c909628/u181754921/docs/d2/8e788a45ca20/magnetogram_output.png?extra=xHS1L4ScAeKZTx3iUM_Opaq1Y8szqK5ceTOuXC3L2PSIHkUySG50jdprdMsDq4XqGhJhl22Dt293LZAaz5uIBxuJfo38uKMWSJfxQ6QrVekhi_lo1CdVb_DrBj7NtXGGHKA9ycE6sKizbpR1zDEGig6C4Ek"
        )
      );

      dispatch(
        magnetogramSlice.actions.replaceStructuralElements(
          data.structuralElements
        )
      );

      dispatch(magnetogramSlice.actions.replaceDefects(data.defects));

      dispatch(сalculateDefectInfo());

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
        id: uuidv4(),
        description: description ?? "",
        type: type,
        leftCoordinateX: formatCoordinate(leftCoordinateX),
        rightCoordinateX: formatCoordinate(rightCoordinateX),
        markerColor: getRandomColor(),
        isEditable: false,
      };

      dispatch(
        magnetogramSlice.actions.replaceDefects([...defects, newDefect])
      );
      dispatch(coordinatesCorrection(newDefect));
      dispatch(сalculateDefectInfo());
    } else {
      const newStructuralElement: StructuralElement = {
        id: uuidv4(),
        type: type,
        leftCoordinateX: formatCoordinate(leftCoordinateX),
        rightCoordinateX: formatCoordinate(rightCoordinateX),
        markerColor: getRandomColor(),
        isEditable: false,
        structuralElementType:
          structuralElementType ?? StructuralElementTypes[0],
      };
      dispatch(coordinatesCorrection(newStructuralElement));
      dispatch(
        magnetogramSlice.actions.replaceStructuralElements([
          ...structuralElements,
          newStructuralElement,
        ])
      );

      dispatch(сalculateDefectInfo());
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
                leftCoordinateX: formatCoordinate(coordinateX),
              }
            : {
                ...currentDefect,
                rightCoordinateX: formatCoordinate(coordinateX),
              };
        dispatch(coordinatesCorrection(newCurrentDefect));
        dispatch(magnetogramSlice.actions.replaceDefect(newCurrentDefect));
        dispatch(сalculateDefectInfo());
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
                leftCoordinateX: formatCoordinate(coordinateX),
              }
            : {
                ...currentStructuralElement,
                rightCoordinateX: formatCoordinate(coordinateX),
              };

        dispatch(coordinatesCorrection(newCurrentStructuralElement));
        dispatch(
          magnetogramSlice.actions.replaceStructuralElement(
            newCurrentStructuralElement
          )
        );
        dispatch(сalculateDefectInfo());
      }
    }
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
 * Устанавливает признак видимости структурных элементов
 * @param value признак видимости структурных элементов
 */
export const setIsShowOriginalImage =
  (value: boolean) => (dispatch: AppDispatch) => {
    dispatch(magnetogramSlice.actions.replaceIsShowOriginalImage(value));
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
    const {
      defects,
      processedImage,
      structuralElements,
      name,
      commitId,
      magnetogrammId,
    } = magnetogramSelector(state);
    const { currentUser } = authSelector(state);
    try {
      defects.forEach((defect) => {
        dispatch(coordinatesCorrection(defect));
      });

      structuralElements.forEach((structuralElement) => {
        dispatch(coordinatesCorrection(structuralElement));
      });

      dispatch(сalculateDefectInfo());

      saveNewMagnetogram(
        // "00000000-0000-0000-0000-000000000000",
        // "00000000-0000-0000-0000-000000000000",
        commitId,
        magnetogrammIds,
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
      const filtredDefects = [...defects].filter(
        (defect) => defect.id !== magnetogramElementId
      );
      dispatch(magnetogramSlice.actions.replaceDefects(filtredDefects));
      filtredDefects.forEach((defect) => {
        dispatch(coordinatesCorrection(defect));
      });
      dispatch(сalculateDefectInfo());
    }
    if (type === "structuralElement") {
      const filtredStructuralElements = [...structuralElements].filter(
        (structuralElement) => structuralElement.id !== magnetogramElementId
      );
      dispatch(
        magnetogramSlice.actions.replaceStructuralElements(
          filtredStructuralElements
        )
      );
      structuralElements.forEach((structuralElement) => {
        dispatch(coordinatesCorrection(structuralElement));
      });
    }
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
  () => (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();
    const { defects, structuralElements } = magnetogramSelector(state);

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

/**
 * Проверяет координаты элемента магнитограммы и при необходимости меняет их местами
 * @param magnetogramElement элемент магнитограммы
 */
export const coordinatesCorrection =
  (magnetogramElement: StructuralElement | Defect) =>
  (dispatch: AppDispatch) => {
    if (
      magnetogramElement.leftCoordinateX > magnetogramElement.rightCoordinateX
    ) {
      if (magnetogramElement.type === "defect") {
        const newCurrentDefect: Defect = {
          ...(magnetogramElement as Defect),
          leftCoordinateX: formatCoordinate(
            magnetogramElement.rightCoordinateX
          ),
          rightCoordinateX: formatCoordinate(
            magnetogramElement.leftCoordinateX
          ),
        };
        dispatch(magnetogramSlice.actions.replaceDefect(newCurrentDefect));
      }
      if (magnetogramElement.type === "structuralElement") {
        const newCurrentStructuralElement: StructuralElement = {
          ...(magnetogramElement as StructuralElement),
          leftCoordinateX: formatCoordinate(
            magnetogramElement.rightCoordinateX
          ),
          rightCoordinateX: formatCoordinate(
            magnetogramElement.leftCoordinateX
          ),
        };
        dispatch(
          magnetogramSlice.actions.replaceStructuralElement(
            newCurrentStructuralElement
          )
        );
      }
    }
  };

/**
 *  Устанавливает значение масштаба
 * @param value значение масштаба
 */
export const setScale = (value: number) => (dispatch: AppDispatch) => {
  dispatch(magnetogramSlice.actions.replaceScale(value));
};
