import React, { FC, useState } from "react";
import { AddNewReportFormProps } from "./types";
import { Button } from "@consta/uikit/Button";
import { IconClose } from "@consta/uikit/IconClose";
import { IconQuestion } from "@consta/uikit/IconQuestion";
import { IconAttach } from "@consta/uikit/IconAttach";
import { Text } from "@consta/uikit/Text";
import { TextField } from "@consta/uikit/TextField";
import { DragNDropField } from "@consta/uikit/DragNDropField";
import { Attachment } from "@consta/uikit/Attachment";

import css from "./style.css";

export const AddNewReportForm: FC<AddNewReportFormProps> = ({
  onAddNewReport,
  onCloseModal,
}) => {
  const [reportName, setReportName] = useState<string | null>(null);
  const [file, setFile] = React.useState<File | null>(null);

  const onChangeReportName = ({ value }: { value: string | null }) =>
    setReportName(value);

  const onDropFiles = (files: File[]): void => {
    setFile(files[0]);
  };

  const onDeleteFile = () => {
    setFile(null);
  };

  const humanFileSize = (size: number) => {
    const i = size === 0 ? 0 : Math.floor(Math.log(size) / Math.log(1024));
    return `${Number(size / Math.pow(1024, i)).toFixed(2)} ${
      ["Б", "Кб", "Мб", "Кб", "Тб"][i]
    }`;
  };

  const isSaveDisabled = !reportName || !file;

  return (
    <div className={`container-column ${css.addNewReportFormModal} p-8`}>
      <div className="container-row justify-between p-b-8">
        <Text size="xl" weight="bold">
          Новый отчет
        </Text>
        <Button
          size="s"
          onlyIcon={true}
          iconLeft={IconClose}
          view="clear"
          onClick={onCloseModal}
        />
      </div>
      <div className="w-100 container-column">
        <TextField
          className="m-b-6"
          onChange={onChangeReportName}
          value={reportName}
          type="text"
          placeholder="Введите название отчета"
          label="Название отчета"
          labelIcon={IconQuestion}
          required
        />
        <DragNDropField
          className="m-b-6"
          onDropFiles={onDropFiles}
          accept=".pkl"
        >
          {({ openFileDialog }) => (
            <>
              <Text>Перетащите файл сюда или нажмите на кнопку ниже</Text>
              <Text view="ghost" font="mono">
                Поддерживаемые форматы: PKL
              </Text>
              <Button
                className="m-t-6"
                onClick={openFileDialog}
                view="ghost"
                iconLeft={IconAttach}
                label="Выбрать файл"
              />
            </>
          )}
        </DragNDropField>
        <div className={css.fileContainer}>
          {file && (
            <Attachment
              key={file.name}
              fileName={file.name}
              fileExtension={"pkl"}
              fileDescription={`${humanFileSize(
                file.size
              )} ${new Date().toLocaleString()}`}
              withAction
              onButtonClick={onDeleteFile}
              buttonIcon={IconClose}
            />
          )}
        </div>
      </div>

      <Button
        className="m-t-4"
        label="Сохранить"
        size="m"
        onClick={() => {
          onAddNewReport(reportName, file);
        }}
        disabled={isSaveDisabled}
      />
    </div>
  );
};
