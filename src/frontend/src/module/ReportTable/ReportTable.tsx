import css from "./style.css";
import React, { FC, useEffect } from "react";
import { Table, TableColumn } from "@consta/uikit/Table";
import { Text } from "@consta/uikit/Text";
import { Badge } from "@consta/uikit/Badge";
import { ReportTableData, RoutePaths } from "../../types";
import { ActionCellRenderer } from "../../components";
import { useNavigate } from "react-router-dom";
import { ReportTableProps } from "./types";
import { removeReportRow, downloadReport, useAppDispatch } from "../../store";

export const ReportTable: FC<ReportTableProps> = ({ rowData }) => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const colDefs: TableColumn<ReportTableData>[] = [
    {
      title: "Наименование",
      accessor: "name",
      align: "left",
      sortable: true,
      width: 400,
      renderCell: (row) => {
        const magnetogramPath = `${RoutePaths.Magnetogram}/${row.id}`;

        return (
          <Text
            size="s"
            view="link"
            className={css.link}
            onClick={() => navigate(magnetogramPath)}
          >
            {row.name}
          </Text>
        );
      },
    },
    {
      title: "Дата создания",
      accessor: "createdAt",
      align: "left",
      sortable: true,
      width: 250,
    },
    {
      title: "Автор",
      accessor: "createdBy",
      align: "left",
      sortable: true,
      width: 200,
    },
    {
      title: "Наличие дефектов",
      accessor: "isDefective",
      align: "left",
      sortable: true,
      renderCell: (row) => {
        const text = row.isDefective ? "Есть" : "Нет";
        const status = row.isDefective ? "warning" : "success";
        return <Badge size="s" label={text} status={status} view="stroked" />;
      },
    },
    {
      title: "Действия",
      accessor: "id",
      sortable: true,
      width: 150,
      renderCell: (row) => {
        const onDownload = () => {
          dispatch(downloadReport(row.id));
        };
        const onEdit = () => {
          const magnetogramPath = `${RoutePaths.Magnetogram}/${row.id}`;
          navigate(magnetogramPath);
        };
        const onRemove = () => {
          dispatch(removeReportRow(row.id));
        };

        return (
          <ActionCellRenderer
            onDownload={onDownload}
            onEdit={onEdit}
            onRemove={onRemove}
          />
        );
      },
    },
  ];

  return (
    <div className={css.table}>
      <Table
        rows={rowData}
        columns={colDefs}
        borderBetweenColumns
        borderBetweenRows
        zebraStriped="odd"
        size="l"
      />
    </div>
  );
};
