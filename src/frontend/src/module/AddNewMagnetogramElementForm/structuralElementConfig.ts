import { StructuralElementNames, StructuralElementName } from "../../types";

export type StructuralElementConfigItem = {
  id: number;
  label: StructuralElementName;
};

export const structuralElementConfig: StructuralElementConfigItem[] = [
  {
    id: 0,
    label: StructuralElementNames.None,
  },
  {
    id: 1,
    label: StructuralElementNames.WeldSeam,
  },
  {
    id: 2,
    label: StructuralElementNames.Bend,
  },
  {
    id: 3,
    label: StructuralElementNames.Branching,
  },
  {
    id: 4,
    label: StructuralElementNames.Patch,
  },
];
