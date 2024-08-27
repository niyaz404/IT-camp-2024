export type MagnetogramElementType = "defect" | "structuralElement";

export type MagnetogramElement = {
  id: string; // идентификатор
  description: string; // наименование магнитограммы
  type: MagnetogramElementType; // тип элемента
  leftCoordinateX: number; // Левая координа по оси Х
  rightCoordinateX: number; // Правая координа по оси Х
  leftNeighbour?: MagnetogramElement | null; // Ближайший левый элементы
  rightNeighbour?: MagnetogramElement | null; // Ближайший правый элементы
};

export type Magnetogram = {
  id: string; // идентификатор
  name: string; // наименование магнитограммы
  objectName: string; //наименование участка объекта (трубы)
  data: any; // данные
  createdAt: Date; // дата создания
};

export type MetaData = {
  id: string; // идентификатор
  magnetogramId: string; // идентификатор магнитограммы
  createdAt: Date; // дата создания
  userId: string; // идентификатор пользователя
  isDefective: boolean; // наличие дефектов
  defects: MagnetogramElement[];
  structuralElements: MagnetogramElement[];
  originalImage: any; // исходное изображение
  processImage: any; // исходное изображение
};

export type Report = {
  id: string; // идентификатор
  metaDataId: string; // идентификатор метадаты
  data: any; // данные
  createdAt: Date; // дата создания
};

// Запросы Magnetogram

// POST
// add(Magnetogram){
//     return boolean
// }

// Запросы MetaData

// POST
// addNewMagnetogramVersion(MetaData){
//     return boolean
// }

// DELETE
// remove(MetaData.magnetogramId){
//     return boolean
// }

// GET
// get(MetaData.magnetogramId){
//     return MetaData
// }

// GET
// getAll(){
//     return MetaData[]
// }

// Запросы Report

// GET
// download(MetaData.id){
//     return Report
// }
