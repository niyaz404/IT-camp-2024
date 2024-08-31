export type MagnetogramDetailsToolbarProps = {
  name: string;
  isDefectsCheked: boolean;
  isStructuralElementsCheked: boolean;
  onDefectsSwitchChange: () => void;
  onStructuralElementsSwitchChange: () => void;
  onAddNewElement: () => void;
};
