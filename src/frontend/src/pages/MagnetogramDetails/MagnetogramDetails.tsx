import React, { MouseEventHandler, useEffect, useState } from "react";
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
import {
  magnetogramSelector,
  setIsDefectsVisible,
  setIsStructuralElementsVisible,
  useAppDispatch,
  useAppSelector,
} from "../../store";
import { Loader } from "@consta/uikit/Loader";

export const MagnetogramDetails = () => {
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [newElementCoordinate, setNewElementCoordinate] = useState<number>(0);

  const {
    defects,
    structuralElements,
    isDefectsVisible,
    isStructuralElementsVisible,
  } = useAppSelector(magnetogramSelector);

  const dispatch = useAppDispatch();

  const onSave = () => {};

  const onClear = () => {};

  const onOpenModal = () => {
    setIsModalOpen(true);
  };

  const onCloseModal = () => {
    setIsModalOpen(false);
    setNewElementCoordinate(0);
  };

  const onDefectsSwitchChange = (checked: boolean) => {
    dispatch(setIsDefectsVisible(checked));
  };

  const onStructuralElementsSwitchChange = (checked: boolean) => {
    dispatch(setIsStructuralElementsVisible(checked));
  };

  return (
    <div className="container-column w-100 h-100">
      <MagnetogramDetailsToolbar
        name={"Название объекта"}
        isDefectsCheked={isDefectsVisible}
        isStructuralElementsCheked={isStructuralElementsVisible}
        onDefectsSwitchChange={onDefectsSwitchChange}
        onStructuralElementsSwitchChange={onStructuralElementsSwitchChange}
        onAddNewElement={onOpenModal}
        onSave={onSave}
      />

      <MagnetogramWrapper
        onAddNewElement={(coordinate: number) => {
          setNewElementCoordinate(coordinate);
          onOpenModal();
        }}
        elements={[...defects, ...structuralElements]}
        isDefectsCheked={isDefectsVisible}
        isStructuralElementsCheked={isStructuralElementsVisible}
      >
        <Magnetogram />
      </MagnetogramWrapper>

      {/* <MagnetogramDetailsFooter onSave={onSave} onClear={onClear} /> */}
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
