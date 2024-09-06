import { MagnetogramElementType } from "../types";

export const getMagnetogramElementType = (type: MagnetogramElementType) => {
  if (type === "defect") return 1;
  else {
    return 2;
  }
};
