import { InitialMagnetogramState } from "./types";

export const initialState: InitialMagnetogramState = {
  id: "",
  name: "",
  processImage: "",
  defects: [],
  structuralElements: [],
  isLoading: false,
  isDefectsVisible: true,
  isStructuralElementsVisible: true,
};
