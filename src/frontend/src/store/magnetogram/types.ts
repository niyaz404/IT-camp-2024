import { Defect, StructuralElement } from "../../types";

export type InitialMagnetogramState = {
  id: string;
  name: string,
  defects: Defect[];
  structuralElements: StructuralElement[];
  processImage: string;
  isLoading: boolean;
  isDefectsVisible: boolean;
  isStructuralElementsVisible: boolean;
};
