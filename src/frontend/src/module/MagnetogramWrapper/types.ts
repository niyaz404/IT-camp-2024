import { Defect, StructuralElement } from "../../types";

export type MagnetogramWrapperProps = {
  children: React.ReactNode;
  elements: (Defect | StructuralElement)[];
  isDefectsCheked: boolean;
  isStructuralElementsCheked: boolean;
  onAddNewElement: (coordinate: number) => void;
};
