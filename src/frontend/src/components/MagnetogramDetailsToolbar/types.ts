export type MagnetogramDetailsToolbarProps = {
  name: string;
  isDefectsCheked: boolean;
  isStructuralElementsCheked: boolean;
  onDefectsSwitchChange: (checked: boolean) => void;
  onStructuralElementsSwitchChange: (checked: boolean) => void;
  onAddNewElement: () => void;
  onSave: () => void;
};
