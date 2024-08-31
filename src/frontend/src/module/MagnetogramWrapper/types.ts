import { MagnetogramElement } from "../../types";

export type MagnetogramWrapperProps = {
  children: React.ReactNode;
  elements: MagnetogramElement[];
  isDefectsCheked: boolean;
  isStructuralElementsCheked: boolean;
  onAddNewElement: (coordinate: number) => void;
};
