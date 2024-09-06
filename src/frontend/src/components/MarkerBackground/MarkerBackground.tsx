import React, { FC } from "react";
import { MarkerBackgroundProps } from "./types";

export const MarkerBackground: FC<MarkerBackgroundProps> = ({
  color,
  width,
  leftOffset,
  type,
}) => {
  return (
    <div
      style={{
        position: "absolute",
        left: leftOffset ?? 0,
        width: width ?? 0,
        backgroundColor: color,
        opacity: "0.25",
        height: type === "defect" ? "532px" : "543px",
        marginLeft: "0.4rem",
      }}
    />
  );
};
