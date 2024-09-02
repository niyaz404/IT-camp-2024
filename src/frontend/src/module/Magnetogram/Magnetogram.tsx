import React, { FC } from "react";
import css from "./style.css";
import { magnetogramSelector, useAppSelector } from "../../store";

export const Magnetogram: FC = () => {
  const { processImage } = useAppSelector(magnetogramSelector);

  return (
    <div className={css.magnetogram}>
      <img height="512" src={processImage} className={css.edge} />
    </div>
  );
};
