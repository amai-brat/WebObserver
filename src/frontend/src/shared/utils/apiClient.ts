import axios from "axios";
import { getToken } from "./token";

const apiClient = axios.create({
  withCredentials: true
});

apiClient.interceptors.request.use(config => {
  config.headers.Authorization = `Bearer ${getToken()}`;
  return config;
}, error => {
  return Promise.reject(error as Error);
});

export default apiClient;