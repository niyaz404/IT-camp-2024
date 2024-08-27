import { MagnetogramElement } from "../../types";

export type InitialMagnetogramState = {
  id: string;
  defects: MagnetogramElement[];
  structuralElements: MagnetogramElement[];
  originalImage: any;
  processImage: any;
  isLoading: boolean;
};
