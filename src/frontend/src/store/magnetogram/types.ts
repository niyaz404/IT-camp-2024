import { Defect, StructuralElement } from "../../types";

export type InitialMagnetogramState = {
  id: string;
  name: string;
  defects: Defect[];
  structuralElements: StructuralElement[];
  processedImage: string | undefined;
  isLoading: boolean;
  isDefectsVisible: boolean;
  isStructuralElementsVisible: boolean;
};

export type LeftRightSplit = {
  left: StructuralElement[];
  right: StructuralElement[];
};
