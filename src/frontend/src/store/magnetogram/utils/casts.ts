import {
  Defect,
  MagnetogramElementType,
  StructuralElement,
} from "../../../types";
import { DefectDto, StructuralElementDto } from "../../../api/Bff/BffClient";

/**
 * Преобразовывает дефект к типуDefectDto
 * @param localDefect дефект, локального типа
 * @returns дефект типа DefectDto
 */
export const castDefect = (localDefect: Defect): DefectDto => {
  const mappedDefects = new DefectDto({
    id: localDefect.id,
    type: getType(localDefect.type),
    leftStructuralElementId: localDefect.leftStructuralElementId ?? undefined,
    rightStructuralElementId: localDefect.leftStructuralElementId ?? undefined,
    color: localDefect.markerColor,
    startXCoordinate: localDefect.leftCoordinateX,
    endXCoordinate: localDefect.rightCoordinateX,
    description: localDefect.description,
  });

  return mappedDefects;
};

/**
 * Преобразовывает структурный элемент к типу StructuralElementDto
 * @param structuralElement структурный элемент, локального типа
 * @returns дефект типа StructuralElementDto
 */
export const castStructuralElement = (
  localStructuralElement: StructuralElement
): StructuralElementDto => {
  const mappedStructuralElement = new StructuralElementDto({
    id: localStructuralElement.id,
    type: getType(localStructuralElement.type),
    startXCoordinate: localStructuralElement.leftCoordinateX,
    endXCoordinate: localStructuralElement.rightCoordinateX,
    color: localStructuralElement.markerColor,
    structuralElementType: localStructuralElement.structuralElementType,
  });

  return mappedStructuralElement;
};

const getType = (type: MagnetogramElementType) => {
  if (type === "defect") return 1;
  else {
    return 2;
  }
};
