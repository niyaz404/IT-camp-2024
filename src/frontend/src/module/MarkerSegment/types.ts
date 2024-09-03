import { MagnetogramElement } from "../../types";

export type MarkerSegmentProps = {
  element: MagnetogramElement;
  isDefectsCheked: boolean;
  isStructuralElementsCheked: boolean;
  leftOffset: number;
  scrollOffset: number;
};
