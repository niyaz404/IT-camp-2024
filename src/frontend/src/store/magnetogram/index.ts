export { magnetogramReducer, magnetogramSlice } from "./slice";
export { magnetogramSelector } from "./selectors";
export {
  addMagnetogramElement,
  updateElementCoordinates,
  setIsDefectsVisible,
  setIsStructuralElementsVisible,
  loadMagnetogramById,
  saveMagnetogram,
  removeMagnetogramElement,
  reverseMagnetogramElementEnable,
} from "./actions";

export {
  castDefect,
  castDefectToLocal,
  castMagnetogram,
  castStructuralElement,
  castStructuralElementToLocal,
} from "./utils";
