import React, { FC, useRef, useState } from "react";
import { ActionCellRendererProps } from "./types";
import { Button } from "@consta/uikit/Button";
import { IconKebab } from "@consta/uikit/IconKebab";
import { IconEdit } from "@consta/uikit/IconEdit";
import { IconTrash } from "@consta/uikit/IconTrash";
import { IconDocExport } from "@consta/uikit/IconDocExport";
import { Popover } from "@consta/uikit/Popover";
import css from "./style.css";

export const ActionCellRenderer: FC<ActionCellRendererProps> = ({
  onDownload,
  onEdit,
  onRemove,
}) => {
  const buttonRef = useRef<HTMLButtonElement | null>(null);
  const [isPopoverVisible, setIsPopoverVisible] = useState<boolean>(false);

  const onOpenKebab = () => {
    setIsPopoverVisible(true);
  };

  const onCloseKebab = () => {
    setIsPopoverVisible(false);
  };

  return (
    <>
      <Button
        label="Скачать"
        size="xs"
        className="m-r-1"
        iconRight={IconDocExport}
        onClick={onDownload}
      />

      <Button
        ref={buttonRef}
        size="xs"
        iconSize="s"
        view="ghost"
        onlyIcon
        iconLeft={IconKebab}
        onClick={onOpenKebab}
      />

      {isPopoverVisible && (
        <Popover
          direction="downStartLeft"
          spareDirection="downStartLeft"
          offset="2xs"
          arrowOffset={0}
          onClickOutside={onCloseKebab}
          isInteractive={true}
          anchorRef={buttonRef}
          equalAnchorWidth={false}
          placeholder={undefined}
          onPointerEnterCapture={undefined}
          onPointerLeaveCapture={undefined}
        >
          <div className={css.popover}>
            <Button
              className="m-b-2"
              label="Редактировать"
              size="xs"
              view="clear"
              iconLeft={IconEdit}
              onClick={onEdit}
            />
            <Button
              label="Удалить"
              size="xs"
              view="clear"
              iconLeft={IconTrash}
              onClick={onRemove}
            />
          </div>
        </Popover>
      )}
    </>
  );
};
