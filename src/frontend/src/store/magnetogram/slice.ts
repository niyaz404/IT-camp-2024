import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { initialState } from "./initialState";
import { InitialMagnetogramState } from "./types";
import { MagnetogramElement } from "../../types";

/**
 * Обновление всех дефектов
 * @param state текущее состояние
 * @param action массив дефектов
 */
const replaceDefects = (
  state: InitialMagnetogramState,
  action: PayloadAction<MagnetogramElement>
) => {
  state.defects = [...state.defects, action.payload];
};

/**
 * Обновление всех структурных элементов
 * @param state текущее состояние
 * @param action массив структурных элементов
 */
const replaceStructuralElements = (
  state: InitialMagnetogramState,
  action: PayloadAction<MagnetogramElement>
) => {
  state.structuralElements = [...state.structuralElements, action.payload];
};

/**
 * Обновление определенных дефектов
 * @param state текущее состояние
 * @param action обновляемый дефект
 */
const replaceDefect = (
  state: InitialMagnetogramState,
  action: PayloadAction<MagnetogramElement>
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
  action: PayloadAction<MagnetogramElement>
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
  },
});

export const magnetogramReducer = magnetogramSlice.reducer;
