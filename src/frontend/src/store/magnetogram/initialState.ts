import { InitialMagnetogramState } from "./types";

export const initialState: InitialMagnetogramState = {
  id: "",
  name: "",
  processedImage: "",
  defects: [],
  structuralElements: [],
  isLoading: false,
  isDefectsVisible: true,
  isStructuralElementsVisible: true,
};
