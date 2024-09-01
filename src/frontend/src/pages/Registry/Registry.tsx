import React, { useEffect, useState } from "react";
import { ReportTable } from "../../module";
import { AddNewReportForm, RegistryHeader } from "../../components";
import { Modal } from "@consta/uikit/Modal";
import { Layout } from "@consta/uikit/Layout";
import { check, getAllReportRow } from "../../api";

export const Registry = () => {
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  useEffect(() => {
    const loadReportRows = async () => {
      await getAllReportRow();
      await check();
    };

    loadReportRows();
  }, []);

  const onAddNewReport = () => {};

  const onOpenModal = () => {
    setIsModalOpen(true);
  };

  const onCloseModal = () => {
    setIsModalOpen(false);
  };

  return (
    <Layout direction="row" className="container-column w-100 h-100">
      <Layout flex={1}></Layout>
      <Layout flex={4}>
        <div className="w-100">
          <RegistryHeader onAddNewReport={onOpenModal} />
          <ReportTable />
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
