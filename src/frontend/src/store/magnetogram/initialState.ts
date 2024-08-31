import { InitialMagnetogramState } from "./types";

export const initialState: InitialMagnetogramState = {
  id: "",
  originalImage:
    "https://sun9-61.userapi.com/impg/7si0UBDyDbTJIAcO8f8bz8edYbNnZeGxjycXcg/QKWthMNk0-A.jpg?size=2560x80&quality=96&sign=f9284bec24830e39d04225c5e3ae852c&type=album",
  // magnetogramURL: "https://sun9-47.userapi.com/impg/4AN_a0F4kqtqxohdMJ7Z1WEo0TZ1DxW_LH_Chw/zV-iHNZRaBk.jpg?size=2560x80&quality=96&sign=7a4f8012ecb8a39d95e4388ce2c09f05&type=album",
  // magnetogramURL: "https://sun9-79.userapi.com/impg/tFunkzv7P5XBBeCjFiV_ZYtSxxXF7nP1IyFwkg/5vml6Z4IQeU.jpg?size=2560x80&quality=96&sign=3126b0ad7938b825a5d68a24c73675d6&type=album",
  processImage: null,
  defects: [
    {
      description: "Это дефект",
      id: "1",
      coordinateX: 50,
      type: "defect",
      markerColor: "rgb(255,255,0)",
    },
  ],
  structuralElements: [
    {
      description: "Это структурный элемент",
      id: "1",
      coordinateX: 100,
      type: "structuralElement",
      markerColor: "rgb(255,255,0)",
    },
  ],
  isLoading: false,
};
