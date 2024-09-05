import { getAllReportRow, removeMagnetogram, addMagnetogram } from "../../api";
import { AppDispatch, RootState } from "../store";
import { reportSlice } from "./slice";
import { reportSelector } from "./selectors";
import { FileParameter } from "../../api/Bff/BffClient";
import FileSaver from "file-saver";

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
      // const { file } = await downloadReportById(reportRowId);

      const fileData =
        "JVBERi0xLjQKJdP0zOEKMSAwIG9iago8PAovQ3JlYXRpb25EYXRlIChEOjIwMjQwOTA0MTkxMTU0KzA1JzAwJykKL0NyZWF0b3IgPEZFRkYwMDUwMDA0NDAwNDYwMDczMDA2ODAwNjEwMDcyMDA3MDAwMjAwMDMxMDAyRTAwMzUwMDMwMDAyRTAwMzQwMDMwMDAzMDAwMzAwMDJEMDA2RTAwNjUwMDc0MDA3MzAwNzQwMDYxCjAwNkUwMDY0MDA2MTAwNzIwMDY0MDAyMDAwMjgwMDY4MDA3NDAwNzQwMDcwMDA3MzAwM0EwMDJGMDAyRjAwNjcwMDY5MDA3NDAwNjgwMDc1MDA2MjAwMkUwMDYzMDA2RgowMDZEMDAyRjAwNzMwMDc0MDA3MzAwNzQwMDY1MDA2OTAwNjcwMDY1MDA3MjAwMkYwMDUwMDA2NDAwNjYwMDUzMDA2ODAwNjEwMDcyMDA3MDAwNDMwMDZGMDA3MjAwNjUKMDAyOT4KL1Byb2R1Y2VyIChQREZzaGFycCAxLjUwLjQwMDAtbmV0c3RhbmRhcmQgXChodHRwczovL2dpdGh1Yi5jb20vc3RzdGVpZ2VyL1BkZlNoYXJwQ29yZVwpKQo+PgplbmRvYmoKMiAwIG9iago8PAovVHlwZSAvQ2F0YWxvZwovUGFnZXMgMyAwIFIKPj4KZW5kb2JqCjMgMCBvYmoKPDwKL1R5cGUgL1BhZ2VzCi9Db3VudCAxCi9LaWRzIFs0IDAgUl0KPj4KZW5kb2JqCjQgMCBvYmoKPDwKL1R5cGUgL1BhZ2UKL01lZGlhQm94IFswIDAgNTk1IDg0Ml0KL1BhcmVudCAzIDAgUgovQ29udGVudHMgNSAwIFIKL1Jlc291cmNlcwo8PAovUHJvY1NldCBbL1BERi9UZXh0L0ltYWdlQi9JbWFnZUMvSW1hZ2VJXQovRXh0R1N0YXRlCjw8Ci9HUzAgNiAwIFIKL0dTMSA3IDAgUgo+PgovRm9udAo8PAovRjAgMTEgMCBSCj4+Cj4+Ci9Hcm91cAo8PAovQ1MgL0RldmljZVJHQgovUyAvVHJhbnNwYXJlbmN5Cj4+Cj4+CmVuZG9iago1IDAgb2JqCjw8Ci9MZW5ndGggMTA1MwovRmlsdGVyIC9GbGF0ZURlY29kZQo+PgpzdHJlYW0KeJztWM2O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";

      if (fileData) {
        const decodedData = atob(fileData);
        const blob = new Blob([decodedData], { type: "text/plain" });
        FileSaver.saveAs(blob, `{file.fileName}.pdf`);
      }
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
