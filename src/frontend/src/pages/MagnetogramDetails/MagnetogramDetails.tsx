import React, { useState } from "react";
import { Modal } from "@consta/uikit/Modal";
import {
  ElementEdge,
  MagnetogramDetailsFooter,
  MagnetogramDetailsToolbar,
} from "../../components";
import {
  Magnetogram,
  AddNewMagnetogramElementForm,
  MagnetogramWrapper,
} from "../../module";
import { magnetogramSelector, useAppSelector } from "../../store";

export const MagnetogramDetails = () => {
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

  const { defects, structuralElements } = useAppSelector(magnetogramSelector);

  const onSave = () => {};

  const onClear = () => {};

  const onOpenModal = () => {
    setIsModalOpen(true);
  };

  const onCloseModal = () => {
    setIsModalOpen(false);
  };

  const onDefectsSwitchChange = () => {};

  const onStructuralElementsSwitchChange = () => {};

  const onStructuralElementSwitchChange = () => {};

  const onIsDefectSwitchChange = () => {};

  const elements = [0, 10, 15, 50, 100];

  return (
    <div className="container-column w-100 h-100">
      <MagnetogramDetailsToolbar
        name={"Название объекта"}
        isDefectsCheked={false}
        isStructuralElementsCheked={false}
        onDefectsSwitchChange={onDefectsSwitchChange}
        onStructuralElementsSwitchChange={onStructuralElementsSwitchChange}
        onAddNewDefect={onOpenModal}
      />

      <MagnetogramWrapper elements={[...defects, ...structuralElements]}>
        <Magnetogram />
      </MagnetogramWrapper>

      <MagnetogramDetailsFooter onSave={onSave} onClear={onClear} />
      <Modal
        isOpen={isModalOpen}
        hasOverlay
        onClickOutside={onCloseModal}
        onEsc={onCloseModal}
      >
        <AddNewMagnetogramElementForm onCloseModal={onCloseModal} />
      </Modal>
    </div>
  );
};
