import { MagnetogramElement } from "../../types";

export type InitialMagnetogramState = {
  id: string;
  defects: MagnetogramElement[];
  structuralElements: MagnetogramElement[];
  processImage: any;
  isLoading: boolean;
  isDefectsVisible: boolean;
  isStructuralElementsVisible: boolean;
};
