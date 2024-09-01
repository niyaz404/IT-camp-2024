import axios from "axios";

// const baseURL = "";
const baseURL = "http://webapi:8000";

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
