import { axiosInstance } from "../axiosInstanse";
import {
  AuthClient,
  CommitClient,
  DefectDto,
  FileParameter,
  HealthClient,
  MagnetogramClient,
  ReportClient,
  StructuralElementDto,
  UserCredentials,
} from "./BffClient";

enum BffClients {
  authClient,
  commitClient,
  magnetogramClient,
  reportClient,
  healthClient,
}

function getClient(client: BffClients.authClient): AuthClient;
function getClient(client: BffClients.commitClient): CommitClient;
function getClient(client: BffClients.magnetogramClient): MagnetogramClient;
function getClient(client: BffClients.reportClient): ReportClient;
function getClient(client: BffClients.healthClient): HealthClient;

function getClient(client: BffClients): any {
  switch (client) {
    case BffClients.authClient:
      return new AuthClient("", axiosInstance);

    case BffClients.commitClient:
      return new CommitClient("", axiosInstance);

    case BffClients.magnetogramClient:
      return new MagnetogramClient("", axiosInstance);

    case BffClients.reportClient:
      return new ReportClient("", axiosInstance);

    case BffClients.healthClient:
      return new HealthClient("", axiosInstance);
  }
}

/**
 * Добавление новой магнитограммы
 * @param name
 * @param createdAt
 * @param file
 * @returns
 */
export const addMagnetogram = (
  name: string | undefined,
  createdAt: Date | undefined,
  file: FileParameter | undefined
) => {
  const client = getClient(BffClients.magnetogramClient);
  return client.save2(name, undefined, createdAt, file);
};

/**
 * Получаем все строки реестра
 * @returns
 */
export const getAllReportRow = () => {
  const client = getClient(BffClients.commitClient);
  return client.getAll();
};

/**
 * Получаем информацию о конктреной магнитограмме
 * @returns
 */
export const getMagnetogramInfo = (reportRowId: string) => {
  const client = getClient(BffClients.commitClient);
  return client.get(reportRowId);
};

/**
 * Сохранение магнитограммы
 * @returns
 */
export const saveNewMagnetogram = (
  magnetogramId: string | undefined,
  createdAt: Date | undefined,
  name: string | undefined,
  createdBy: string | undefined,
  isDefective: boolean | undefined,
  defects: DefectDto[] | undefined,
  structuralElements: StructuralElementDto[] | undefined,
  processedMagnetogram: string | undefined,
  originalMagnetogram: FileParameter | undefined
) => {
  const client = getClient(BffClients.commitClient);
  return client.save(
    magnetogramId,
    createdAt,
    name,
    createdBy,
    isDefective,
    defects,
    structuralElements,
    processedMagnetogram,
    originalMagnetogram
  );
};

/**
 * Удаление конктреной магнитограммы
 * @returns
 */
export const removeMagnetogram = (reportRowId: string) => {
  const client = getClient(BffClients.commitClient);
  return client.delete(reportRowId);
};

/**
 * Получаем отчет по конкретной строке
 * @returns
 */
export const getReportById = (reportRowId: string) => {
  const client = getClient(BffClients.reportClient);
  return client.get2(reportRowId);
};

/**
 * Вход пользователя в систему
 * @returns
 */
export const loginInSystem = (
  username: string | undefined,
  password: string | undefined
) => {
  const client = getClient(BffClients.authClient);
  const userCredentials = new UserCredentials({
    username,
    password,
  });
  return client.login(userCredentials);
};

/**
 * Проверка бэка
 * @returns
 */
export const check = () => {
  const client = getClient(BffClients.healthClient);
  return client.check();
};
