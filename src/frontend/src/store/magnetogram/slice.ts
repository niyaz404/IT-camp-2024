import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { initialState } from "./initialState";
import { InitialMagnetogramState } from "./types";
import { MagnetogramElement } from "../../types";

const replaceDefect = (
  state: InitialMagnetogramState,
  action: PayloadAction<MagnetogramElement>
) => {
  state.defects = [...state.defects, action.payload];
};

const replaceStructuralElements = (
  state: InitialMagnetogramState,
  action: PayloadAction<MagnetogramElement>
) => {
  state.structuralElements = [...state.structuralElements, action.payload];
};

export const magnetogramSlice = createSlice({
  name: "magnetogramSlice",
  initialState: initialState,
  reducers: {
    replaceDefect,
    replaceStructuralElements,
  },
});

export const magnetogramReducer = magnetogramSlice.reducer;
