import { StructuralElementTypes, StructuralElementType } from "../types";

/**
 * Преоразует тип структурного элемента к локальному
 * @param type тип структурного элемента
 * @returns  локальный тип структурного элемента
 */
export const getStructuralElementLocalType = (
  type: number
): StructuralElementType => {
  switch (type) {
    case 1:
      return StructuralElementTypes[1];
    case 2:
      return StructuralElementTypes[2];
    case 3:
      return StructuralElementTypes[3];
    case 4:
      return StructuralElementTypes[4];

    default:
      return StructuralElementTypes[0];
  }
};
