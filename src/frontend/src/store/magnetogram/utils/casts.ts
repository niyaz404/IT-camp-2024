import {
  Defect,
  Magnetogram,
  MagnetogramElement,
  MagnetogramElementType,
  StructuralElement,
} from "../../../types";
import {
  CommitDto,
  DefectDto,
  StructuralElementDto,
} from "../../../api/Bff/BffClient";
import {
  getMagnetogramElementType,
  getRandomColor,
  getStructuralElementLocalType,
  getStructuralElementType,
} from "../../../utils";

export const castMagnetogram = (res: CommitDto): Magnetogram => {
  const result: Magnetogram = {
    commitId: res.id ?? "",
    name: res.name ?? "",
    createdAt: res.createdAt,
    createdBy: res.createdBy,
    isDefective: res.isDefective,
    defects: res.defects ? res.defects.map(castDefectToLocal) : [],
    structuralElements: res.structuralElements
      ? res.structuralElements.map(castStructuralElementToLocal)
      : [],
    processedImage: res.processedImage,
  };
  return result;
};

/**
 * Преобразовывает дефект к типу DefectDto
 * @param localDefect дефект, локального типа
 * @returns дефект типа DefectDto
 */
export const castDefect = (localDefect: Defect): DefectDto => {
  const mappedDefects = new DefectDto({
    id: localDefect.id ?? "",
    type: getMagnetogramElementType(localDefect.type),
    startXCoordinate: localDefect.leftCoordinateX,
    endXCoordinate: localDefect.rightCoordinateX,
    description: localDefect.description,
  });

  return mappedDefects;
};

/**
 * Преобразовывает дефект к локальному типу
 * @param localDefect дефект типа DefectDto
 * @returns дефект локального типа
 */
export const castDefectToLocal = (defect: DefectDto): Defect => {
  const mappedDefect: Defect = {
    id: defect.id ?? "",
    type: "defect",
    leftCoordinateX: defect.startXCoordinate,
    rightCoordinateX: defect.endXCoordinate,
    markerColor: getRandomColor(),
    isEditable: false,
    description: defect.description ?? "",
  };

  return mappedDefect;
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
    id: localStructuralElement.id ?? "",
    type: getMagnetogramElementType(localStructuralElement.type),
    startXCoordinate: localStructuralElement.leftCoordinateX,
    endXCoordinate: localStructuralElement.rightCoordinateX,
    structuralElementType: getStructuralElementType(
      localStructuralElement.structuralElementType
    ),
  });

  return mappedStructuralElement;
};

/**
 * Преобразовывает структурный элемент к типу StructuralElementDto
 * @param structuralElement структурный элемент, локального типа
 * @returns дефект типа StructuralElementDto
 */
export const castStructuralElementToLocal = (
  structuralElement: StructuralElementDto
): StructuralElement => {
  const mappedStructuralElement: StructuralElement = {
    id: structuralElement.id ?? "",
    type: "structuralElement",
    leftCoordinateX: structuralElement.startXCoordinate,
    rightCoordinateX: structuralElement.endXCoordinate,
    markerColor: getRandomColor(),
    isEditable: false,
    structuralElementType: getStructuralElementLocalType(
      structuralElement.structuralElementType
    ),
  };

  return mappedStructuralElement;
};
