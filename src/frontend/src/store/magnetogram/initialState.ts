import { InitialMagnetogramState } from "./types";

export const initialState: InitialMagnetogramState = {
  commitId: "",
  magnetogrammId: "",
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
