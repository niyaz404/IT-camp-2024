import React, { FC, useRef, useState } from "react";
import { NavbarProps } from "./types";
import { Header } from "@consta/uikit/Header";
import { HeaderLogin, HeaderModule } from "@consta/uikit/Header";
import { Popover } from "@consta/uikit/Popover";
import { Button } from "@consta/uikit/Button";
import { IconExit } from "@consta/uikit/IconExit";

import css from "./style.css";
import { useNavigate } from "react-router-dom";
import { RoutePaths } from "../../types";

export const Navbar: FC<NavbarProps> = () => {
  const ref = useRef(null);
  const [isMenuOpen, setIsMenuOpen] = useState<boolean>(false);

  const navigate = useNavigate();

  const onLogout = () => {
    // logout
    navigate(RoutePaths.Auth);
  };

  const onHeaderLoginClick = () => {
    if (isLogged) {
      setIsMenuOpen(true);
    } else {
      navigate(RoutePaths.Auth);
    }
  };

  const onCloseMenu = () => {
    setIsMenuOpen(false);
  };

  const isLogged = true;

  return (
    <div className="container-row justify-between align-center w-100">
      <Header
        leftSide={<></>}
        rightSide={
          <>
            <HeaderModule>
              <HeaderLogin
                ref={ref}
                isLogged={isLogged}
                personName={"Дамир"}
                personInfo="Доп. информация"
                onClick={onHeaderLoginClick}
                placeholder={undefined}
                onPointerEnterCapture={undefined}
                onPointerLeaveCapture={undefined}
              />

              {isMenuOpen && (
                <Popover
                  direction="downCenter"
                  offset="2xs"
                  arrowOffset={0}
                  onClickOutside={onCloseMenu}
                  isInteractive={true}
                  anchorRef={ref}
                  equalAnchorWidth={true}
                  placeholder={undefined}
                  onPointerEnterCapture={undefined}
                  onPointerLeaveCapture={undefined}
                >
                  <div className={css.popover}>
                    <Button
                      label="Выйти"
                      size="xs"
                      view="clear"
                      onClick={onLogout}
                      iconLeft={IconExit}
                    />
                  </div>
                </Popover>
              )}
            </HeaderModule>
          </>
        }
      />
    </div>
  );
};
