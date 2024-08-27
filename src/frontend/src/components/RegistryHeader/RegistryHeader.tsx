import React, { FC } from "react";
import { RegistryHeaderProps } from "./types";
import { Button } from "@consta/uikit/Button";
import { IconAdd } from "@consta/uikit/IconAdd";
import { Text } from "@consta/uikit/Text";

export const RegistryHeader: FC<RegistryHeaderProps> = ({ onAddNewReport }) => {
  return (
    <div className="container-row justify-between align-center w-100 p-8">
      <Text size="2xl" weight="bold">
        Отчеты
      </Text>
      <Button
        label="Новый отчет"
        size="m"
        iconRight={IconAdd}
        onClick={onAddNewReport}
      />
    </div>
  );
};
