import {
  MagnetogramElementType,
  MarkerSide,
  StructuralElementCount,
  StructuralElementType,
} from "../../types";

export type MarkerProps = {
  id: string;
  color: string;
  coordinate: number;
  type: MagnetogramElementType;
  leftOffset: number;
  scrollOffset: number;
  side: MarkerSide;
  isEditable: boolean;
  setCoordinate: (side: MarkerSide, coordinate: number) => void;
};
