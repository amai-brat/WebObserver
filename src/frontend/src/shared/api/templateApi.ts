import { ENDPOINTS } from "../../app/consts/endpoints";
import type { ObservingTemplate } from "../../app/models/observing";
import apiClient from "../utils/apiClient";


interface GetAllTemplatesResponse {
  templates: ObservingTemplate[]
}

const getAll = async (abortSignal?: AbortSignal): Promise<ObservingTemplate[]> => {
  const { data } = await apiClient.get<GetAllTemplatesResponse>(ENDPOINTS.TEMPLATE.GET_ALL, {
    signal: abortSignal
  });
  return data.templates;
}

export const templateApi = {
  getAll
}
