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

export const castMagnetogram = (res: CommitDto): Magnetogram => {
  const result: Magnetogram = {
    id: res.magnetogramId ?? "",
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
    markerColor: defect.color ?? "",
    isEditable: false,
    description: defect.description ?? "",
    leftStructuralElementId: defect.leftStructuralElementId ?? "",
    rightStructuralElementId: defect.rightStructuralElementId ?? "",
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
    id: localStructuralElement.id,
    type: getType(localStructuralElement.type),
    startXCoordinate: localStructuralElement.leftCoordinateX,
    endXCoordinate: localStructuralElement.rightCoordinateX,
    color: localStructuralElement.markerColor,
    structuralElementType: localStructuralElement.structuralElementType,
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
    markerColor: structuralElement.color ?? "",
    isEditable: false,
    structuralElementType: structuralElement.structuralElementType,
  };

  return mappedStructuralElement;
};

const getType = (type: MagnetogramElementType) => {
  if (type === "defect") return 1;
  else {
    return 2;
  }
};
