import { StructuralElementTypes, StructuralElementType } from "../types";
import { StructuralElementType as StructuralElementTypeClient } from "../api/Bff/BffClient";

/**
 * Преоразует тип структурного элемента
 * @param type локальный тип структурного элемента
 * @returns тип структурного элемента
 */
export const getStructuralElementType = (
  type: StructuralElementType
): StructuralElementTypeClient => {
  switch (type) {
    case StructuralElementTypes[1]:
      return 1;
    case StructuralElementTypes[2]:
      return 2;
    case StructuralElementTypes[3]:
      return 3;
    case StructuralElementTypes[4]:
      return 4;

    default:
      return 0;
  }
};
