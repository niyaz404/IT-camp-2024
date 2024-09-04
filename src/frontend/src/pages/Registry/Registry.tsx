import React, { useEffect, useState } from "react";
import { ReportTable } from "../../module";
import { AddNewReportForm, RegistryHeader } from "../../components";
import { Modal } from "@consta/uikit/Modal";
import { Layout } from "@consta/uikit/Layout";
import { check } from "../../api";
import {
  addNewMagnetogramReport,
  authSelector,
  loadReportRowData,
  reportSelector,
  useAppDispatch,
  useAppSelector,
} from "../../store";
import { Loader } from "@consta/uikit/Loader";

export const Registry = () => {
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const { reportRowData, isLoading } = useAppSelector(reportSelector);
  const { currentUser } = useAppSelector(authSelector);

  const dispatch = useAppDispatch();

  useEffect(() => {
    const loadRowData = () => {
      dispatch(loadReportRowData());
      // check();
    };

    loadRowData();
  }, []);

  const onAddNewReport = (reportName: string | null, file: File | null) => {
    const createdAt = new Date();
    const createdBy = currentUser?.userName ?? "";
    dispatch(addNewMagnetogramReport(reportName, createdBy, createdAt, file));
  };

  const onOpenModal = () => {
    setIsModalOpen(true);
  };

  const onCloseModal = () => {
    setIsModalOpen(false);
  };

  if (isLoading === true) {
    return <Loader />;
  }

  return (
    <Layout direction="row" className="container-column w-100 h-100">
      <Layout flex={1}></Layout>
      <Layout flex={4}>
        <div className="w-100">
          <RegistryHeader onAddNewReport={onOpenModal} />
          <ReportTable rowData={reportRowData} />
          <Modal
            isOpen={isModalOpen}
            hasOverlay
            onClickOutside={onCloseModal}
            onEsc={onCloseModal}
          >
            <AddNewReportForm
              onAddNewReport={onAddNewReport}
              onCloseModal={onCloseModal}
            />
          </Modal>
        </div>
      </Layout>
      <Layout flex={1}></Layout>
    </Layout>
  );
};
