import { Defect, StructuralElement } from "../../types";

export type MagnetogramWrapperProps = {
  children: React.ReactNode;
  isDefectsCheked: boolean;
  isStructuralElementsCheked: boolean;
  onAddNewElement: (coordinate: number) => void;
};
