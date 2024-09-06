import React, { FC } from "react";
import { MagnetogramDetailsToolbarProps } from "./types";
import { Text } from "@consta/uikit/Text";
import { Switch } from "@consta/uikit/Switch";
import { Button } from "@consta/uikit/Button";
import { IconShape } from "@consta/uikit/IconShape";
import { IconSelect } from "@consta/uikit/IconSelect";
import { IconStop } from "@consta/uikit/IconStop";
import { IconAllDone } from "@consta/uikit/IconAllDone";
import { Slider } from "@consta/uikit/Slider";

import css from "./style.css";
import { Delimiter } from "../../components";

export const MagnetogramDetailsToolbar: FC<MagnetogramDetailsToolbarProps> = ({
  name,
  isDefectsCheked,
  isStructuralElementsCheked,
  isShowOriginalImageCheked,
  onShowOriginalImageSwitchChange,
  onDefectsSwitchChange,
  onStructuralElementsSwitchChange,
  onAddNewElement,
  onSave,
  onChangeScale,
  scale,
}) => {
  return (
    <div
      className={`container-row justify-between align-center w-100 p-h-8 p-v-4 ${css.toolbar}`}
    >
      <div className="container-row align-center w-100">
        <Text size="2xl" weight="bold">
          {name}
        </Text>
        <Delimiter />

        <Button
          label="Новый элемент"
          size="xs"
          iconLeft={IconShape}
          onClick={onAddNewElement}
          view="secondary"
        />

        <Delimiter />

        <Switch
          size="s"
          label="Дефекты"
          checked={isDefectsCheked}
          onChange={({ checked }) => onDefectsSwitchChange(checked)}
        />
        <IconSelect size="l" view="secondary" />

        <Switch
          className="m-l-2"
          size="s"
          label="Конструктивные элементы"
          checked={isStructuralElementsCheked}
          onChange={({ checked }) => onStructuralElementsSwitchChange(checked)}
        />
        <IconStop size="s" className={css.rotate} view="secondary" />

        <Delimiter />

        <Switch
          size="s"
          label="Исходная магнитограмма"
          checked={isShowOriginalImageCheked}
          onChange={({ checked }) => onShowOriginalImageSwitchChange(checked)}
        />
      </div>
      <Delimiter />

      <Slider
        className="p-v-2 m-h-2"
        label={`Масштаб ${scale}%`}
        onChange={onChangeScale}
        value={scale}
      />

      <Button
        label="Сохранить"
        size="s"
        iconLeft={IconAllDone}
        onClick={onSave}
        view="secondary"
      />
    </div>
  );
};
