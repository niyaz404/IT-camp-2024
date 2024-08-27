import React, { FC, useState } from "react";
import { AddNewMagnetogramElementProps } from "./types";
import { Button } from "@consta/uikit/Button";
import { IconClose } from "@consta/uikit/IconClose";
import { IconQuestion } from "@consta/uikit/IconQuestion";
import { Text } from "@consta/uikit/Text";
import { TextField } from "@consta/uikit/TextField";
import { Switch } from "@consta/uikit/Switch";

import css from "./style.css";
import { addMagnetogramElement, useAppDispatch } from "../../store";

const MAX_MAGNETOGRAM_WIDTH = 4096;

export const AddNewMagnetogramElementForm: FC<
  AddNewMagnetogramElementProps
> = ({ onCloseModal }) => {
  const [firstPoint, setFirstPoint] = useState<number>(0);
  const [secondPoint, setSecondPoint] = useState<number>(0);
  const [isFirstPointHasError, setIsFirstPointHasError] =
    useState<boolean>(false);
  const [isSecondPointHasError, setIsSecondPointHasError] =
    useState<boolean>(false);
  const [isDefectCheked, setIsDefectCheked] = useState<boolean>(false);
  const [isStructuralElementCheked, setIsStructuralElementCheked] =
    useState<boolean>(false);
  const [description, setDescription] = useState<string | null>(null);

  const dispatch = useAppDispatch();

  const onStructuralElementSwitchChange = () => {
    setIsStructuralElementCheked((prev) => !prev);
    setIsDefectCheked(false);
  };
  const onIsDefectSwitchChange = () => {
    setIsDefectCheked((prev) => !prev);
    setIsStructuralElementCheked(false);
  };

  const onChangeFirstPoint = ({ value }: { value: string | null }) => {
    setIsFirstPointHasError(Number(value) > MAX_MAGNETOGRAM_WIDTH);
    setFirstPoint(Number(value));
  };

  const onChangeSecondPoint = ({ value }: { value: string | null }) => {
    setIsSecondPointHasError(Number(value) > MAX_MAGNETOGRAM_WIDTH);
    setSecondPoint(Number(value));
  };

  const onChangeDescription = ({ value }: { value: string | null }) => {
    setDescription(value);
  };

  const onAddNewMagnetogramElement = () => {
    const type = isDefectCheked ? "defect" : "structuralElement";
    dispatch(
      addMagnetogramElement("discrition", type, firstPoint, secondPoint)
    );
    onCloseModal();
  };

  const isSaveDisabled =
    !firstPoint ||
    !secondPoint ||
    isFirstPointHasError ||
    isSecondPointHasError ||
    !description ||
    (!isDefectCheked && !isStructuralElementCheked);

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

      <TextField
        className="m-r-3 m-b-4"
        onChange={onChangeDescription}
        value={description}
        type="text"
        label="Описание"
        labelIcon={IconQuestion}
        required
        width="full"
      />

      <div className="w-100 container-row m-b-6 justify-between">
        <TextField
          className="m-r-3"
          onChange={onChangeFirstPoint}
          value={String(firstPoint)}
          type="number"
          label="Координата левая (Ось Х)"
          labelIcon={IconQuestion}
          required
          min={0}
          max={MAX_MAGNETOGRAM_WIDTH}
          status={isFirstPointHasError ? "alert" : undefined}
          caption={
            isFirstPointHasError ? "Указано некорректное значение" : undefined
          }
          width="full"
        />
        <TextField
          className="m-l-3"
          onChange={onChangeSecondPoint}
          value={String(secondPoint)}
          type="number"
          label="Координата правая (Ось Х)"
          labelIcon={IconQuestion}
          required
          min={0}
          max={MAX_MAGNETOGRAM_WIDTH}
          status={isSecondPointHasError ? "alert" : undefined}
          caption={
            isSecondPointHasError ? "Указано некорректное значение" : undefined
          }
          width="full"
        />
      </div>

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
