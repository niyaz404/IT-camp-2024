import React, { FC, MouseEventHandler } from "react";
import { MagnetogramProps } from "./types";
import { Button } from "@consta/uikit/Button";
import { IconAllDone } from "@consta/uikit/IconAllDone";
import { IconTrash } from "@consta/uikit/IconTrash";
import css from "./style.css";

export const Magnetogram: FC<MagnetogramProps> = () => {
  const onOpenModal = (event: React.MouseEvent) => {
    console.log(Math.ceil(event.clientX / 4));
  };
  return (
    <div
      className={`${css.magnetogram} justify-between align-center p-h-8 p-v-4`}
    >
      <img
        onClick={onOpenModal}
        height="512"
        src="https://sun9-61.userapi.com/impg/7si0UBDyDbTJIAcO8f8bz8edYbNnZeGxjycXcg/QKWthMNk0-A.jpg?size=2560x80&quality=96&sign=f9284bec24830e39d04225c5e3ae852c&type=album"
        // src="https://sun9-47.userapi.com/impg/4AN_a0F4kqtqxohdMJ7Z1WEo0TZ1DxW_LH_Chw/zV-iHNZRaBk.jpg?size=2560x80&quality=96&sign=7a4f8012ecb8a39d95e4388ce2c09f05&type=album"
        // src="https://sun9-79.userapi.com/impg/tFunkzv7P5XBBeCjFiV_ZYtSxxXF7nP1IyFwkg/5vml6Z4IQeU.jpg?size=2560x80&quality=96&sign=3126b0ad7938b825a5d68a24c73675d6&type=album"
        className={css.edge}
      />
    </div>
  );
};
