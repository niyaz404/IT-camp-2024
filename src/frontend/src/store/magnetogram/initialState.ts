import { InitialMagnetogramState } from "./types";

export const initialState: InitialMagnetogramState = {
  id: "",
  originalImage:
    "https://psv4.userapi.com/c909618/u181754921/docs/d59/ebb01bf44d66/magnetogram_output.png?extra=W3-9hTDSB75RNGgGiLe5ilkmaU20d-ANvoxO8QkHS0UYkDmWRz5guNoedo2W4qOaAZBYMROpeK0MilBcV9hA_lUdrUSme-GTkoeTUrzBaKZ44Hakb7GH6CU0wB3yiGdBmgfd3pqjMJMwd1lvJllU3MX6j1o",

  processImage:
    "https://psv4.userapi.com/c909618/u181754921/docs/d59/ebb01bf44d66/magnetogram_output.png?extra=W3-9hTDSB75RNGgGiLe5ilkmaU20d-ANvoxO8QkHS0UYkDmWRz5guNoedo2W4qOaAZBYMROpeK0MilBcV9hA_lUdrUSme-GTkoeTUrzBaKZ44Hakb7GH6CU0wB3yiGdBmgfd3pqjMJMwd1lvJllU3MX6j1o",
  defects: [
    {
      description: "Это дефект",
      id: "1",
      coordinateX: 50,
      type: "defect",
      markerColor: "rgb(255,255,0)",
      isEditable: false,
    },
  ],
  structuralElements: [
    {
      description: "Это структурный элемент",
      id: "1",
      coordinateX: 100,
      type: "structuralElement",
      markerColor: "rgb(255,255,0)",
      isEditable: false,
    },
  ],
  isLoading: false,
  isDefectsVisible: true,
  isStructuralElementsVisible: true,
};
