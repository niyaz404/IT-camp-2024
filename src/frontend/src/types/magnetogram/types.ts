export type MagnetogramElementType = "defect" | "structuralElement";

export type MagnetogramElement = {
  id: string; // идентификатор
  description: string; // наименование магнитограммы
  type: MagnetogramElementType; // тип элемента
  coordinateX: number; // координата по оси Х
  leftNeighbour?: MagnetogramElement | null; // Ближайший левый элементы
  rightNeighbour?: MagnetogramElement | null; // Ближайший правый элементы
  markerColor: string;
  isEditable: false;
};

export type Magnetogram = {
  id: string; // идентификатор
  name: string; // наименование магнитограммы
  objectName: string; //наименование участка объекта (трубы)
  data: any; // данные
  createdAt: Date; // дата создания
};
