import axios from "axios";

const baseURL = "";

export const axiosInstance = axios.create({
  baseURL,
  responseType: "json",
});

axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.authorization = "Bearer " + token;
  }
  return config;
});
