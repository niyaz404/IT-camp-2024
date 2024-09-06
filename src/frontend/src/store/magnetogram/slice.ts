import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { initialState } from "./initialState";
import { InitialMagnetogramState } from "./types";
import { Defect, MagnetogramElement, StructuralElement } from "../../types";

/**
 * Обновление всех дефектов
 * @param state текущее состояние
 * @param action массив дефектов
 */
const replaceDefects = (
  state: InitialMagnetogramState,
  action: PayloadAction<Defect[]>
) => {
  console.log("action.payload", action.payload);

  state.defects = action.payload;
};

/**
 * Обновление всех структурных элементов
 * @param state текущее состояние
 * @param action массив структурных элементов
 */
const replaceStructuralElements = (
  state: InitialMagnetogramState,
  action: PayloadAction<StructuralElement[]>
) => {
  state.structuralElements = action.payload;
};

/**
 * Обновление определенных дефектов
 * @param state текущее состояние
 * @param action обновляемый дефект
 */
const replaceDefect = (
  state: InitialMagnetogramState,
  action: PayloadAction<Defect>
) => {
  state.defects = [
    ...state.defects.filter((defetct) => defetct.id !== action.payload.id),
    action.payload,
  ];
};

/**
 * Обновление определенных конструктивных элементов
 * @param state текущее состояние
 * @param action обновляемый конструктивный элемент
 */
const replaceStructuralElement = (
  state: InitialMagnetogramState,
  action: PayloadAction<StructuralElement>
) => {
  state.structuralElements = [
    ...state.structuralElements.filter(
      (structuralElement) => structuralElement.id !== action.payload.id
    ),
    action.payload,
  ];
};

/**
 * Заменяет признак видимости маркеров дефектов
 * @param state текущее состояние
 * @param action признак видимости маркеров дефектов
 */
const replaceIsDefectsVisible = (
  state: InitialMagnetogramState,
  action: PayloadAction<boolean>
) => {
  state.isDefectsVisible = action.payload;
};

/**
 * Заменяет признак видимости маркеров структурных элементов
 * @param state текущее состояние
 * @param action признак видимости маркеров структурных элементов
 */
const replaceIsStructuralElementsVisible = (
  state: InitialMagnetogramState,
  action: PayloadAction<boolean>
) => {
  state.isStructuralElementsVisible = action.payload;
};

/**
 * Заменяет признак загрузки магнитограммы
 * @param state текущее состояние
 * @param action признак загрузки магнитограммы
 */
const replaceIsMagnetogramLoading = (
  state: InitialMagnetogramState,
  action: PayloadAction<boolean>
) => {
  state.isLoading = action.payload;
};

/**
 * Заменяет признак отображения магнитограммы без обработки
 * @param state текущее состояние
 * @param action признак отображения магнитограммы без обработки
 */
const replaceIsShowOriginalImage = (
  state: InitialMagnetogramState,
  action: PayloadAction<boolean>
) => {
  state.isShowOriginalImage = action.payload;
};

/**
 * Заменяет идентификатор текущей обработки магнитограммы (commitId)
 * @param state текущее состояние
 * @param action идентификатор текущей обработки магнитограммы
 */
const replaceCommitId = (
  state: InitialMagnetogramState,
  action: PayloadAction<string>
) => {
  state.commitId = action.payload;
};

/**
 * Заменяет изображение текущей обработанной магнитограммы
 * @param state текущее состояние
 * @param action изображение текущей обработанной магнитограммы
 */
const replaceProcessedMagnetogramImage = (
  state: InitialMagnetogramState,
  action: PayloadAction<string | undefined>
) => {
  state.processedImage = action.payload;
};

/**
 * Заменяет изображение текущей магнитограммы без обработки
 * @param state текущее состояние
 * @param action изображение текущей магнитограммы без обработки
 */
const replaceOriginalMagnetogramImage = (
  state: InitialMagnetogramState,
  action: PayloadAction<string | undefined>
) => {
  state.originalImage = action.payload;
};

/**
 * Заменяет наименование
 * @param state текущее состояние
 * @param action наименование
 */
const replaceName = (
  state: InitialMagnetogramState,
  action: PayloadAction<string>
) => {
  state.name = action.payload;
};

/**
 * Заменяет масштаб
 * @param state текущее состояние
 * @param action значение масштаба
 */
const replaceScale = (
  state: InitialMagnetogramState,
  action: PayloadAction<number>
) => {
  state.scale = action.payload;
};

/**
 * Заменяет масштаб
 * @param state текущее состояние
 * @param action значение масштаба
 */
const replaceMagnetogramId = (
  state: InitialMagnetogramState,
  action: PayloadAction<string>
) => {
  state.magnetogrammId = action.payload;
};

export const magnetogramSlice = createSlice({
  name: "magnetogramSlice",
  initialState: initialState,
  reducers: {
    replaceDefects,
    replaceStructuralElements,
    replaceDefect,
    replaceStructuralElement,
    replaceIsDefectsVisible,
    replaceIsStructuralElementsVisible,
    replaceIsMagnetogramLoading,
    replaceProcessedMagnetogramImage,
    replaceOriginalMagnetogramImage,
    replaceName,
    replaceIsShowOriginalImage,
    replaceScale,
    replaceCommitId,
    replaceMagnetogramId,
  },
});

export const magnetogramReducer = magnetogramSlice.reducer;
