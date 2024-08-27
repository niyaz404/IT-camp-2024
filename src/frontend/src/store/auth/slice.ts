import { createSlice } from "@reduxjs/toolkit";
import { initialState } from "./initialState";

export const authSlice = createSlice({
  name: "authSlice",
  initialState: initialState,
  reducers: {},
});

export const authReducer = authSlice.reducer;
