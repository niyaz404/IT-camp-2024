import React, { useLayoutEffect, useState } from "react";
import { Modal } from "@consta/uikit/Modal";
import { MagnetogramDetailsToolbar } from "../../components";
import {
  Magnetogram,
  AddNewMagnetogramElementForm,
  MagnetogramWrapper,
} from "../../module";
import {
  loadMagnetogramById,
  magnetogramSelector,
  saveMagnetogram,
  setIsDefectsVisible,
  setIsShowOriginalImage,
  setIsStructuralElementsVisible,
  useAppDispatch,
  useAppSelector,
} from "../../store";
import { Loader } from "@consta/uikit/Loader";
import { useParams } from "react-router-dom";

export const MagnetogramDetails = () => {
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [newElementCoordinate, setNewElementCoordinate] = useState<number>(0);

  const {
    name,
    defects,
    structuralElements,
    isDefectsVisible,
    isStructuralElementsVisible,
    isLoading,
    isShowOriginalImage,
  } = useAppSelector(magnetogramSelector);

  const dispatch = useAppDispatch();

  const params = useParams();
  const magnetogramId = params.id;

  useLayoutEffect(() => {
    const loadMagnetogram = () => {
      dispatch(loadMagnetogramById(magnetogramId));
    };
    loadMagnetogram();
  }, []);

  const onSave = () => {
    dispatch(saveMagnetogram());
  };

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

  const onShowOriginalImageSwitchChange = (checked: boolean) => {
    dispatch(setIsShowOriginalImage(checked));
  };

  if (isLoading) {
    return <Loader />;
  }

  return (
    <div className="container-column w-100 h-100">
      <MagnetogramDetailsToolbar
        name={name}
        isDefectsCheked={isDefectsVisible}
        isStructuralElementsCheked={isStructuralElementsVisible}
        onDefectsSwitchChange={onDefectsSwitchChange}
        onStructuralElementsSwitchChange={onStructuralElementsSwitchChange}
        onAddNewElement={onOpenModal}
        onSave={onSave}
        isShowOriginalImageCheked={isShowOriginalImage}
        onShowOriginalImageSwitchChange={onShowOriginalImageSwitchChange}
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
