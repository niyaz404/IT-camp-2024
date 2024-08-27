import React, { FC } from "react";
import { MagnetogramDetailsFooterProps } from "./types";
import { Button } from "@consta/uikit/Button";
import { IconAllDone } from "@consta/uikit/IconAllDone";
import { IconTrash } from "@consta/uikit/IconTrash";
import css from "./style.css";

export const MagnetogramDetailsFooter: FC<MagnetogramDetailsFooterProps> = ({
  onSave,
  onClear,
}) => {
  return (
    <div
      className={`${css.footer} justify-between align-center w-100 p-h-8 p-v-4`}
    >
      <Button
        label="Очистить"
        size="s"
        view="ghost"
        iconLeft={IconTrash}
        onClick={onClear}
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
