import axios from "axios";
import { setupCache } from 'axios-cache-interceptor';
import { getToken } from "./token";

const apiClient = axios.create({
  withCredentials: true,
});

const cachedApiClient = setupCache(apiClient);

apiClient.interceptors.request.use(config => {
  config.headers.Authorization = `Bearer ${getToken()}`;
  return config;
}, error => {
  return Promise.reject(error as Error);
});

export default cachedApiClient;