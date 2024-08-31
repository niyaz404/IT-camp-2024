import React, { FC, useState } from "react";
import { AddNewMagnetogramElementProps } from "./types";
import { Button } from "@consta/uikit/Button";
import { IconClose } from "@consta/uikit/IconClose";
import { IconQuestion } from "@consta/uikit/IconQuestion";
import { Text } from "@consta/uikit/Text";
import { TextField } from "@consta/uikit/TextField";
import { Switch } from "@consta/uikit/Switch";
import { Select } from "@consta/uikit/Select";
import { structuralElementConfig } from "./structuralElementConfig";
import css from "./style.css";
import { addMagnetogramElement, useAppDispatch } from "../../store";

const MAX_MAGNETOGRAM_WIDTH = 4096;

type Item = {
  label: string;
  id: number;
};

export const AddNewMagnetogramElementForm: FC<
  AddNewMagnetogramElementProps
> = ({ onCloseModal, newElenentCoordinate }) => {
  const [coordinate, setCoordinate] = useState<number>(newElenentCoordinate);
  const [isCoordinateInvalid, setIsCoordinateInvalid] =
    useState<boolean>(false);
  const [isDefectCheked, setIsDefectCheked] = useState<boolean>(true);
  const [isStructuralElementCheked, setIsStructuralElementCheked] =
    useState<boolean>(false);
  const [description, setDescription] = useState<string | null>(null);
  const [structuralElementType, setStructuralElementType] =
    useState<Item | null>(null);

  const dispatch = useAppDispatch();

  const onStructuralElementSwitchChange = () => {
    setIsStructuralElementCheked((prev) => !prev);
    setIsDefectCheked(false);
  };
  const onIsDefectSwitchChange = () => {
    setIsDefectCheked((prev) => !prev);
    setIsStructuralElementCheked(false);
  };

  const onChangeCoordinate = ({ value }: { value: string | null }) => {
    setIsCoordinateInvalid(Number(value) > MAX_MAGNETOGRAM_WIDTH);
    setCoordinate(Number(value));
  };

  const onChangeDescription = ({ value }: { value: string | null }) => {
    setDescription(value);
  };

  const onChangeStructuralElementType = ({ value }: { value: Item | null }) => {
    setStructuralElementType(value);
  };

  const onAddNewMagnetogramElement = () => {
    const type = isDefectCheked ? "defect" : "structuralElement";
    dispatch(addMagnetogramElement("discrition", type, coordinate));
    onCloseModal();
  };

  const isSaveDisabled =
    !coordinate ||
    isCoordinateInvalid ||
    (!isDefectCheked && !isStructuralElementCheked) ||
    (isStructuralElementCheked && !structuralElementType) ||
    (isDefectCheked && !description);

  return (
    <div className={`container-column ${css.addNewReportFormModal} p-8`}>
      <div className="container-row justify-between p-b-8">
        <Text size="xl" weight="bold">
          Новый элемент
        </Text>
        <Button
          size="s"
          onlyIcon={true}
          iconLeft={IconClose}
          view="clear"
          onClick={onCloseModal}
        />
      </div>

      {isDefectCheked && (
        <TextField
          className="m-r-3 m-b-4"
          onChange={onChangeDescription}
          value={description}
          type="text"
          label="Описание"
          required
          width="full"
        />
      )}

      {isStructuralElementCheked && (
        <Select
          className="m-r-3 m-b-4"
          placeholder="Выберите тип структурного элемента"
          items={structuralElementConfig}
          value={structuralElementType}
          onChange={onChangeStructuralElementType}
          label="Тип структурного элемента"
          required
        />
      )}

      <TextField
        className="m-r-3 m-b-4"
        onChange={onChangeCoordinate}
        value={String(coordinate)}
        type="number"
        label="Координата (Ось Х)"
        labelIcon={IconQuestion}
        required
        min={0}
        max={MAX_MAGNETOGRAM_WIDTH}
        status={isCoordinateInvalid ? "alert" : undefined}
        caption={
          isCoordinateInvalid ? "Указано некорректное значение" : undefined
        }
        width="full"
      />

      <Switch
        className="m-r-3 m-b-6"
        size="s"
        label="Дефект"
        checked={isDefectCheked}
        onClick={onIsDefectSwitchChange}
      />
      <Switch
        className="m-r-3 m-b-6"
        size="s"
        label="Конструктивный элемент"
        checked={isStructuralElementCheked}
        onClick={onStructuralElementSwitchChange}
      />

      <Button
        label="Добавить"
        size="m"
        onClick={onAddNewMagnetogramElement}
        disabled={isSaveDisabled}
      />
    </div>
  );
};
