import { CommitDto } from "../../../api/Bff/BffClient";
import { ReportTableData } from "../../../types";

/**
 * Преобразует отчет к локальному типу
 * @param res отчет тиав CommitDto
 * @returns отчет локального типа
 */
export const castReportToLocal = (res: CommitDto): ReportTableData => {
  const result: ReportTableData = {
    id: res.id ?? "",
    name: res.name ?? "",
    createdAt: getRuDate(res.createdAt),
    createdBy: res.createdBy ?? "",
    isDefective: res.isDefective,
    magnetogramId: res.magnetogramId,
  };
  return result;
};

/**
 * Преобразует дату к Русскому формату
 * @param date дата
 * @param config конфиг
 * @returns дата Русского формата
 */
export const getRuDate = (
  date: any,
  config?: Intl.DateTimeFormatOptions
): string => {
  const formatter = new Intl.DateTimeFormat("ru-RU", config);
  date = typeof date === "string" ? new Date(date) : date;
  return formatter.format(date);
};
