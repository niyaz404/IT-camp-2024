export type MagnetogramDetailsToolbarProps = {
  name: string;
  isDefectsCheked: boolean;
  isStructuralElementsCheked: boolean;
  isShowOriginalImageCheked: boolean;
  scale: number;
  onChangeScale: ({ value }: { value: number | null }) => void;
  onShowOriginalImageSwitchChange: (checked: boolean) => void;
  onDefectsSwitchChange: (checked: boolean) => void;
  onStructuralElementsSwitchChange: (checked: boolean) => void;
  onAddNewElement: () => void;
  onSave: () => void;
};
