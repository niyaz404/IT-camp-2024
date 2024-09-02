import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { MagnetogramDetails, Registry } from "../../pages";
import React from "react";
import { RoutePaths } from "../../types";
import { Auth, Navbar, PrivateRoute } from "..";

export const Routers: React.FC = () => {
  return (
    <Router>
      {/* Navbar отображается только, если пользователь авторизован */}
      <Navbar />
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
        <Route
          path={RoutePaths.Auth}
          element={
            <PrivateRoute>
              <Auth />
            </PrivateRoute>
          }
        />
      </Routes>
    </Router>
  );
};
