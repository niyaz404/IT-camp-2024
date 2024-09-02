import { axiosInstance } from "../axiosInstanse";
import {
  AuthClient,
  CommitClient,
  CommitDto,
  DefectDto,
  FileParameter,
  HealthClient,
  MagnetogramClient,
  ReportClient,
  StructuralElementDto,
  UserCredentialsDto,
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
  name: string,
  createdBy: string,
  createdAt: Date,
  file: FileParameter
) => {
  const client = getClient(BffClients.magnetogramClient);
  return client.save2(name, createdBy, createdAt, file);
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
  createdAt: Date,
  name: string | undefined,
  createdBy: string | undefined,
  isDefective: boolean,
  defects: DefectDto[] | undefined,
  structuralElements: StructuralElementDto[] | undefined,
  processedImage: string | undefined
) => {
  const client = getClient(BffClients.commitClient);

  const newMagnetogram = new CommitDto({
    magnetogramId,
    createdAt,
    name,
    createdBy,
    isDefective,
    defects,
    structuralElements,
    processedImage,
  });

  return client.save(newMagnetogram);
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
export const downloadReportById = (reportRowId: string) => {
  const client = getClient(BffClients.reportClient);
  return client.get2(reportRowId);
};

/**
 * Вход пользователя в систему
 * @returns
 */
export const loginInSystem = (
  login: string | undefined,
  password: string | undefined
) => {
  const client = getClient(BffClients.authClient);
  const userCredentials = new UserCredentialsDto({
    login,
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
