import { Values } from "../common";

export type MagnetogramElementType = "defect" | "structuralElement";

export type MarkerSide = "left" | "right";

export const StructuralElementNames = {
  None: "Чистая труба",
  WeldSeam: "Сварочный шов",
  Bend: "Изгиб",
  Branching: "Ветвление",
  Patch: "Заплатка",
} as const;

export type StructuralElementName = Values<typeof StructuralElementNames>;
export type StructuralElementType = keyof typeof StructuralElementNames;

export const StructuralElementTypes = {
  0: "None",
  1: "WeldSeam",
  2: "Bend",
  3: "Branching",
  4: "Patch",
} as const;

export type MagnetogramElement = {
  id: string; // идентификатор
  type: MagnetogramElementType; // тип элемента
  leftCoordinateX: number; // левая координата по оси Х
  rightCoordinateX: number; // правая координата по оси Х
  markerColor: string;
  isEditable: boolean;
  structuralElementType?: StructuralElementType;
};

export type StructuralElementCount = {
  None: number;
  WeldSeam: number;
  Bend: number;
  Branching: number;
  Patch: number;
};

export type Defect = MagnetogramElement & {
  description: string;
  leftStructuralElementCount?: StructuralElementCount;
  rightStructuralElementCount?: StructuralElementCount;
};

export type StructuralElement = MagnetogramElement & {
  structuralElementType: StructuralElementType;
};

export type Magnetogram = {
  magnetogramId: string;
  commitId: string;
  name: string;
  createdAt: Date;
  createdBy: string | undefined;
  isDefective: boolean;
  defects: Defect[];
  structuralElements: StructuralElement[];
  processedImage: string | undefined;
};
