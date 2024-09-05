import React, { FC } from "react";
import css from "./style.css";
import { magnetogramSelector, useAppSelector } from "../../store";

export const Magnetogram: FC = () => {
  const { isShowOriginalImage, originalImage, processedImage } =
    useAppSelector(magnetogramSelector);

  const image = isShowOriginalImage ? originalImage : processedImage;

  return (
    <div className={css.magnetogram}>
      <img height="512" src={image} className={css.edge} />
    </div>
  );
};
