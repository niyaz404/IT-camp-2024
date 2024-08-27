import css from "./style.css";
import React from "react";
import { Table, TableColumn } from "@consta/uikit/Table";
import { Text } from "@consta/uikit/Text";

import { Badge } from "@consta/uikit/Badge";

import { ReportTableData, RoutePaths } from "../../types";
import { mockReportTableData } from "./mockMetaData";
import { ActionCellRenderer } from "../../components";
import { useNavigate } from "react-router-dom";

export const ReportTable = () => {
  const navigate = useNavigate();

  console.log("render");
  console.log(mockReportTableData);

  const columns: TableColumn<ReportTableData>[] = [
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
      accessor: "userId",
      align: "left",
      sortable: true,
      width: 300,
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
      align: "center",
      sortable: true,
      renderCell: (row) => {
        const onDownload = () => {
          alert(`onDownload ${row.name}`);
        };
        const onEdit = () => {
          alert(`onEdit ${row.name}`);
        };
        const onRemove = () => {
          alert(`onRemove ${row.name}`);
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
        rows={mockReportTableData}
        columns={columns}
        borderBetweenColumns
        borderBetweenRows
        zebraStriped="odd"
        size="l"
      />
    </div>
  );
};
