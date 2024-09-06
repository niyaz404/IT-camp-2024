import { InitialMagnetogramState } from "./types";

export const initialState: InitialMagnetogramState = {
  commitId: "",
  name: "",
  processedImage: "",
  originalImage: "",
  defects: [],
  structuralElements: [],
  isLoading: false,
  isDefectsVisible: true,
  isStructuralElementsVisible: true,
  isShowOriginalImage: false,
  scale: 0,
};
