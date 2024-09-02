import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { initialState } from "./initialState";
import { initialReportState } from "./types";
import { ReportTableData } from "../../types";

/**
 * Получение данных для реестра отчетов
 * @param state
 * @param action
 */
const replaceReportRowData = (
  state: initialReportState,
  action: PayloadAction<ReportTableData[]>
) => {
  state.reportRowData = action.payload;
};

/**
 * Получение данных для реестра отчетов
 * @param state
 * @param action
 */
const replaceIsReportRowDataLoading = (
  state: initialReportState,
  action: PayloadAction<boolean>
) => {
  state.isLoading = action.payload;
};

export const reportSlice = createSlice({
  name: "reportSlice",
  initialState: initialState,
  reducers: {
    replaceReportRowData,
    replaceIsReportRowDataLoading,
  },
});

export const reportReducer = reportSlice.reducer;
