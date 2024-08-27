import { ElementEdgeProps } from "./types";
import React, { FC } from "react";
import css from "./style.css";

export const ElementEdge: FC<ElementEdgeProps> = ({ color, coordinate }) => {
  return (
    <div
      className={`${css.elementEdge}`}
      style={{
        left: coordinate,
      }}
    >
      <div className={css.arrowDown}>
        <svg width={16} height={10}>
          <polygon
            points="0, 0 ,8, 10,  16, 0"
            fill={color}
            stroke="black"
            strokeWidth="1"
          />
        </svg>
      </div>

      <div
        style={{
          position: "absolute",
          height: "100%",
          width: "0.4rem",
          backgroundColor: color,
          border: `0.1rem solid #000`,
        }}
      />

      <div className={css.arrowUp}>
        <svg width={16} height={10}>
          <polygon
            points="0, 10 ,8, 0,  16, 10"
            fill={color}
            stroke="black"
            strokeWidth="1"
          />
        </svg>
      </div>
    </div>
  );
};
