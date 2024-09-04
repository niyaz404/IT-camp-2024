import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { MagnetogramDetails, Registry } from "../../pages";
import React from "react";
import { RoutePaths } from "../../types";
import { Auth, Navbar, PrivateRoute } from "..";
import { isUserAuthenticatedSelector, useAppSelector } from "../../store";

export const Routers: React.FC = () => {
  const isUserAuthenticated = useAppSelector(isUserAuthenticatedSelector);

  return (
    <Router>
      {isUserAuthenticated === true && <Navbar />}

      <Routes>
        <Route
          path={`${RoutePaths.Magnetogram}/:id`}
          element={
            <PrivateRoute>
              <MagnetogramDetails />
            </PrivateRoute>
          }
        />
        <Route
          path={RoutePaths.Registry}
          element={
            <PrivateRoute>
              <Registry />
            </PrivateRoute>
          }
        />
        {/* <Route path={"/*"} element={<>404</>} /> */}
        <Route
          path={RoutePaths.All}
          element={
            <PrivateRoute>
              <Registry />
            </PrivateRoute>
          }
        />
        <Route path={RoutePaths.Auth} element={<Auth />} />
      </Routes>
    </Router>
  );
};
