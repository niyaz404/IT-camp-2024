import { RootState } from "../store";

export const magnetogramSelector = (state: RootState) => state.magnetogram;

export const defectSelector = (state: RootState, defectId: string) => {
  return state.magnetogram.defects.find((defect) => defect.id === defectId);
};

export const structuralElementSelector = (
  state: RootState,
  structuralElementId: string
) => {
  return state.magnetogram.structuralElements.find(
    (structuralElement) => structuralElement.id === structuralElementId
  );
};
