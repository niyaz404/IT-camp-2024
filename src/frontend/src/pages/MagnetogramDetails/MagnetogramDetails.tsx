import React, { useState } from "react";
import { Modal } from "@consta/uikit/Modal";
import {
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
  const [newElementCoordinate, setNewElementCoordinate] = useState<number>(0);

  const { defects, structuralElements } = useAppSelector(magnetogramSelector);

  const onSave = () => {};

  const onClear = () => {};

  const onOpenModal = () => {
    setIsModalOpen(true);
  };

  const onCloseModal = () => {
    setIsModalOpen(false);
    setNewElementCoordinate(0);
  };

  const onDefectsSwitchChange = () => {};

  const onStructuralElementsSwitchChange = () => {};

  return (
    <div className="container-column w-100 h-100">
      <MagnetogramDetailsToolbar
        name={"Название объекта"}
        isDefectsCheked={false}
        isStructuralElementsCheked={false}
        onDefectsSwitchChange={onDefectsSwitchChange}
        onStructuralElementsSwitchChange={onStructuralElementsSwitchChange}
        onAddNewElement={onOpenModal}
      />

      <MagnetogramWrapper
        onAddNewElement={(coordinate: number) => {
          setNewElementCoordinate(coordinate);
          onOpenModal();
        }}
        elements={[...defects, ...structuralElements]}
      >
        <Magnetogram />
      </MagnetogramWrapper>

      <MagnetogramDetailsFooter onSave={onSave} onClear={onClear} />
      <Modal
        isOpen={isModalOpen}
        hasOverlay
        onClickOutside={onCloseModal}
        onEsc={onCloseModal}
      >
        <AddNewMagnetogramElementForm
          onCloseModal={onCloseModal}
          newElenentCoordinate={newElementCoordinate}
        />
      </Modal>
    </div>
  );
};
