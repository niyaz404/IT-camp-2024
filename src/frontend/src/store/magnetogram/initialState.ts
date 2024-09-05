import { InitialMagnetogramState } from "./types";

export const initialState: InitialMagnetogramState = {
  id: "",
  name: "",
  processedImage: "",
  originalImage: "",
  defects: [],
  structuralElements: [],
  isLoading: false,
  isDefectsVisible: true,
  isStructuralElementsVisible: true,
  isShowOriginalImage: false,
};
