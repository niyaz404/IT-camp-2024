import { createSlice } from "@reduxjs/toolkit";
import { initialState } from "./initialState";

export const reportSlice = createSlice({
  name: "reportSlice",
  initialState: initialState,
  reducers: {},
});

export const reportReducer = reportSlice.reducer;
