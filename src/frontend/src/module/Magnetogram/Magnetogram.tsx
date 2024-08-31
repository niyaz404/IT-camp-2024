import React, { FC, MouseEventHandler } from "react";
import { MagnetogramProps } from "./types";
import css from "./style.css";

export const Magnetogram: FC<MagnetogramProps> = () => {
  const onOpenModal = (event: React.MouseEvent) => {};
  return (
    <div className={css.magnetogram}>
      <img
        onClick={onOpenModal}
        height="512"
        // width="4096"
        // height="128"
        src="https://psv4.userapi.com/c909618/u181754921/docs/d59/ebb01bf44d66/magnetogram_output.png?extra=W3-9hTDSB75RNGgGiLe5ilkmaU20d-ANvoxO8QkHS0UYkDmWRz5guNoedo2W4qOaAZBYMROpeK0MilBcV9hA_lUdrUSme-GTkoeTUrzBaKZ44Hakb7GH6CU0wB3yiGdBmgfd3pqjMJMwd1lvJllU3MX6j1o"
        // src="https://sun9-29.userapi.com/impg/NTJDGnhj-1aBEUJ9z0jT8zc5i4Uis8bk5TRoeQ/wAlso5jESrY.jpg?size=1280x40&quality=96&sign=e27f53dd0274d91594e1650708a0a80a&type=album"
        // src="https://sun9-61.userapi.com/impg/7si0UBDyDbTJIAcO8f8bz8edYbNnZeGxjycXcg/QKWthMNk0-A.jpg?size=2560x80&quality=96&sign=f9284bec24830e39d04225c5e3ae852c&type=album"
        // src="https://sun9-47.userapi.com/impg/4AN_a0F4kqtqxohdMJ7Z1WEo0TZ1DxW_LH_Chw/zV-iHNZRaBk.jpg?size=2560x80&quality=96&sign=7a4f8012ecb8a39d95e4388ce2c09f05&type=album"
        // src="https://sun9-79.userapi.com/impg/tFunkzv7P5XBBeCjFiV_ZYtSxxXF7nP1IyFwkg/5vml6Z4IQeU.jpg?size=2560x80&quality=96&sign=3126b0ad7938b825a5d68a24c73675d6&type=album"
        className={css.edge}
      />
    </div>
  );
};
