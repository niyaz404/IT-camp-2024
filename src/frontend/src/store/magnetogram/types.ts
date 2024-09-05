import { Defect, StructuralElement } from "../../types";

export type InitialMagnetogramState = {
  id: string;
  name: string;
  defects: Defect[];
  structuralElements: StructuralElement[];
  processedImage: string | undefined;
  originalImage: string | undefined;
  isLoading: boolean;
  isDefectsVisible: boolean;
  isStructuralElementsVisible: boolean;
  isShowOriginalImage: boolean;
  scale: number;
};

export type LeftRightSplit = {
  left: StructuralElement[];
  right: StructuralElement[];
};
