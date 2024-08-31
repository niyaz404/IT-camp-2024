import { DraggableEvent } from "react-draggable";
import { MagnetogramElementType } from "../../types";

export type ElementEdgeProps = {
  color: string;
  coordinate: number;
  onDragStop: (e: DraggableEvent) => void;
  type: MagnetogramElementType;
  leftOffset: number;
};
