import { saveNewMagnetogram } from "../../api";
import {
  Defect,
  MagnetogramElement,
  MagnetogramElementType,
  MarkerSide,
  StructuralElement,
} from "../../types";
import { authSelector } from "../auth";
import { AppDispatch, RootState } from "../store";
import { magnetogramSelector } from "./selectors";
import { magnetogramSlice } from "./slice";
import { castDefect, castStructuralElement } from "./utils";

/**
 * Подгружает данные для реестра отчетов
 */
export const loadMagnetogramById =
  (magnetogramId: string | undefined) => (dispatch: AppDispatch) => {
    dispatch(magnetogramSlice.actions.replaceIsMagnetogramLoading(true));
    //   const data = await getMagnetogramInfo(magnetogramId);
    //   dispatch(reportSlice.actions.replaceReportRowData(data));

    setTimeout(() => {
      const data = {
        id: "1",
        name: "Отчет 1",
        processImage:
          "https://psv4.userapi.com/c909618/u181754921/docs/d59/70ca2237550b/magnetogram_output.png?extra=0VhFDVr55GXspkc73NxiSmi_1VOoBlwv0TU3D5xwHGTGcp4jrT5iyRcdQ8fYYvMl-WozpBYwE11rFho3a75uiFkaLm5SXsG9mVmwlltV2in8LEFn4Pn3IcdhidnjDR1AKPEdBJ5JR0_V2hQE9g86ky2mRQ",
        defects: [
          {
            description: "Это дефект",
            id: "1",
            leftCoordinateX: 50,
            rightCoordinateX: 150,
            type: "defect",
            markerColor: "rgb(255,0,255)",
            isEditable: false,
          },
        ] as Defect[],
        structuralElements: [
          {
            id: "1",
            leftCoordinateX: 100,
            rightCoordinateX: 200,
            type: "structuralElement",
            markerColor: "rgb(255,255,0)",
            isEditable: false,
          },
        ] as StructuralElement[],
      };

      dispatch(magnetogramSlice.actions.replaceMagnetogramId(data.id));
      dispatch(magnetogramSlice.actions.replaceName(data.name));
      dispatch(
        magnetogramSlice.actions.replaceMagnetogramImage(data.processImage)
      );
      dispatch(magnetogramSlice.actions.replaceDefects(data.defects));
      dispatch(
        magnetogramSlice.actions.replaceStructuralElements(
          data.structuralElements
        )
      );
      dispatch(magnetogramSlice.actions.replaceIsMagnetogramLoading(false));
    }, 0);
  };

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

    if (type === "defect") {
      const newDefect: Defect = {
        id: "new",
        description: description,
        type: type,
        leftCoordinateX: coordinateX,
        rightCoordinateX: coordinateX,
        markerColor: `rgb(${getRandomNum()},${getRandomNum()},${getRandomNum()})`,
        isEditable: false,
        leftStructuralElementId: "",
        rightStructuralElementId: "",
      };
      dispatch(magnetogramSlice.actions.replaceDefects([newDefect]));
    } else {
      const newStructuralElement: StructuralElement = {
        id: "new",
        type: type,
        leftCoordinateX: coordinateX,
        rightCoordinateX: coordinateX,
        markerColor: `rgb(${getRandomNum()},${getRandomNum()},${getRandomNum()})`,
        isEditable: false,
        structuralElementType: 1,
      };

      dispatch(
        magnetogramSlice.actions.replaceStructuralElements([
          newStructuralElement,
        ])
      );
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
    const { defects, id, processImage, structuralElements, name } =
      magnetogramSelector(state);
    const { userName } = authSelector(state);
    try {
      saveNewMagnetogram(
        id,
        new Date(),
        name,
        userName,
        !!defects.length,
        defects.map(castDefect),
        structuralElements.map(castStructuralElement),
        processImage
      );
    } catch (error) {
      alert("Ошибка сохранения изменений");
      console.error(error);
    }
  };
