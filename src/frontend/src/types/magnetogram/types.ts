export type MagnetogramElementType = "defect" | "structuralElement";

export type MarkerSide = "left" | "right";

export type MagnetogramElement = {
  id: string; // идентификатор
  type: MagnetogramElementType; // тип элемента
  leftCoordinateX: number; // левая координата по оси Х
  rightCoordinateX: number; // правая координата по оси Х
  markerColor: string;
  isEditable: boolean;
};

export type Defect = MagnetogramElement & {
  description: string; // наименование магнитограммы
  leftStructuralElementId: string | null; // Ближайший левый структурный элемент
  rightStructuralElementId: string | null; // Ближайший правый структурный элемент
};

export type StructuralElement = MagnetogramElement & {
  structuralElementType: number;
};

export type Magnetogram = {
  id: string;
  name: string;
  createdAt: Date;
  createdBy: string | undefined;
  isDefective: boolean;
  defects: Defect[];
  structuralElements: StructuralElement[];
  processedImage: string | undefined;
};
