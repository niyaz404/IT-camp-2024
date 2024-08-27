import type { FC, ReactElement } from "react";
import React from "react";
import { Navigate } from "react-router-dom";

// import { useAppSelector } from "../store";
// import { isUserAuthenticatedSelector } from "../store/auth/selector";

export const PrivateRoute: FC<{ children: ReactElement }> = ({ children }) => {
  //   const isUserAuthenticated = useAppSelector(isUserAuthenticatedSelector);

  //   return isUserAuthenticated ? children : <Navigate to="/" />;
  return true ? children : <Navigate to="/" />;
};
