import {
  getAllReportRow,
  removeMagnetogram,
  downloadReportById,
  addMagnetogram,
} from "../../api";
import { AppDispatch, RootState } from "../store";
import { reportSlice } from "./slice";
import { reportSelector } from "./selectors";
import { FileParameter } from "src/api/Bff/BffClient";

/**
 * Подгружает данные для реестра отчетов
 */
export const loadReportRowData = () => async (dispatch: AppDispatch) => {
  try {
    dispatch(reportSlice.actions.replaceIsReportRowDataLoading(true));
    const data = await getAllReportRow();
    dispatch(reportSlice.actions.replaceReportRowData(data));
    dispatch(reportSlice.actions.replaceIsReportRowDataLoading(false));
  } catch (error) {
    alert("Ошибка загрузки отчетов");
    console.error(error);
  }
};

/**
 * Удаляет конктреный отчет
 * @param reportRowId идентификатор отчета
 */
export const removeReportRow =
  (reportRowId: string) =>
  async (dispatch: AppDispatch, getState: () => RootState) => {
    const state = getState();
    const { reportRowData } = reportSelector(state);
    try {
      await removeMagnetogram(reportRowId);

      const filtredData = reportRowData.filter(
        (item) => item.id !== reportRowId
      );

      dispatch(reportSlice.actions.replaceReportRowData(filtredData));
    } catch (error) {
      console.error(error);
      alert("Ошибка удаления отчета");
    }
  };

/**
 * Загружает отчет по конктреному идентификатору
 * @param reportRowId идентификатор отчета
 */
export const downloadReport =
  (reportRowId: string) => async (dispatch: AppDispatch) => {
    try {
      const reportFile = await downloadReportById(reportRowId);
    } catch (error) {
      console.error(error);
      alert("Ошибка выгрузки отчета");
    }
  };

/**
 * Создает новый отчет
 * @param name наименование отчета
 * @param createdAt дата создания
 * @param file файл магнитограммы
 */
export const addNewMagnetogramReport =
  (
    name: string | null,
    createdBy: string,
    createdAt: Date,
    file: File | null
  ) =>
  async (dispatch: AppDispatch) => {
    try {
      if (name && file) {
        const parsedFile: FileParameter = {
          data: file,
          fileName: file.name,
        };
        await addMagnetogram(name, createdBy, createdAt, parsedFile);
      } else {
        throw new Error("Ошибка создания отчета");
      }
    } catch (error) {
      console.error(error);
      alert("Ошибка создания отчета");
    }
  };
