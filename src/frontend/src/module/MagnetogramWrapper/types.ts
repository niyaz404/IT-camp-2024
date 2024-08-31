import { MagnetogramElement } from "../../types";

export type MagnetogramWrapperProps = {
  children: React.ReactNode;
  elements: MagnetogramElement[];
  onAddNewElement: (coordinate: number) => void;
};
