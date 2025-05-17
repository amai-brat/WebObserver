import axios from "axios"
import { toast } from "react-toastify";
import type { PayloadAction, SerializedError } from "@reduxjs/toolkit";

// TODO: change properties in backend
export interface ApiError {
  message: string;
  title?: string;
  code?: number;
}

export const getErrorMessage = (action: PayloadAction<ApiError | undefined, string, object, SerializedError>): string => {
  if (!action || !action.payload) return "";
  
  return action.payload.message 
    ?? action.payload.title 
    ?? action.error.message 
    ?? "";
}

export const alertError = (error: Error | string) => {
  if (typeof error === "string") {
    toast(error, {type: "error"});
    return;
  }
  
  if (!axios.isAxiosError(error)) return;
  const isApiError = (data: unknown): data is ApiError => {
    return typeof data === "object" && data !== null && "message" in data;
  };

  const responseData: unknown = error.response?.data;
  if (isApiError(responseData)) {
    toast(responseData.message, { type: "error" });
  } else {
    const fallbackMessage = error.message || "An unexpected error occurred";
    toast(fallbackMessage, { type: "error" });
  }
}