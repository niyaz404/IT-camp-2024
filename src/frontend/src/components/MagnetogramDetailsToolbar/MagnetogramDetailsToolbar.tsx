import React, { FC } from "react";
import { MagnetogramDetailsToolbarProps } from "./types";
import { Text } from "@consta/uikit/Text";
import { Switch } from "@consta/uikit/Switch";
import { Button } from "@consta/uikit/Button";
import { IconShape } from "@consta/uikit/IconShape";
import css from "./style.css";
import { Delimiter } from "../../components";

export const MagnetogramDetailsToolbar: FC<MagnetogramDetailsToolbarProps> = ({
  name,
  isDefectsCheked,
  isStructuralElementsCheked,
  onDefectsSwitchChange,
  onStructuralElementsSwitchChange,
  onAddNewDefect,
}) => {
  return (
    <div
      className={`container-row  align-center w-100 p-h-8 p-v-4 ${css.toolbar}`}
    >
      <Text size="2xl" weight="bold">
        {name}
      </Text>
      <Delimiter />

      <Button
        label="Новый элемент"
        size="xs"
        iconLeft={IconShape}
        onClick={onAddNewDefect}
        view="secondary"
      />

      <Delimiter />

      <Switch
        className="m-r-3"
        size="s"
        label="Дефекты"
        checked={isDefectsCheked}
        onClick={onDefectsSwitchChange}
      />
      <Switch
        size="s"
        label="Конструктивные элементы"
        checked={isStructuralElementsCheked}
        onClick={onStructuralElementsSwitchChange}
      />
    </div>
  );
};
