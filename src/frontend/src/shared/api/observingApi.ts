import { ENDPOINTS } from "../../app/consts/endpoints";
import type { ObservingBase } from "../../app/models/observing";
import apiClient from "../utils/apiClient";


interface BaseCreateObservingDto {
  $type: string,
  cronExpression: string,
  templateId: number,
}

export interface TextCreateObservingDto extends BaseCreateObservingDto {
  url: string;
}

export interface YouTubePlaylistCreateObservingDto extends BaseCreateObservingDto {
  playlistUrl: string | undefined;
  playlistId: string | undefined;
}

export interface EditObservingDto {
  cronExpression: string
}


interface CreateObservingResponse {
  observingId: number;
}

interface GetAllObservingsResponse {
  observings: ObservingBase[]
}


const getAll = async (abortSignal?: AbortSignal): Promise<ObservingBase[]> => {
  const { data } = await apiClient.get<GetAllObservingsResponse>(ENDPOINTS.OBSERVING.GET_ALL, {
    signal: abortSignal
  });
  return data.observings;
}

const create = async (dto: BaseCreateObservingDto, abortSignal?: AbortSignal): Promise<number> => {
  const { data } = await apiClient.post<CreateObservingResponse>(ENDPOINTS.OBSERVING.CREATE, dto, {
    signal: abortSignal
  });
  return data.observingId;
}

const remove = async (observingId: number, abortSignal?: AbortSignal): Promise<void> => {
  const { data } = await apiClient.delete<void>(`${ENDPOINTS.OBSERVING.DELETE}${observingId}`, {
    signal: abortSignal
  });
  return data;
}

const edit = async (observingId: number, dto: EditObservingDto, abortSignal?: AbortSignal): Promise<void> => {
  const { data } = await apiClient.patch<void>(`${ENDPOINTS.OBSERVING.EDIT}${observingId}`, dto, {
    signal: abortSignal
  });
  return data;
}


export const observingApi = {
  getAll,
  create,
  remove,
  edit
}