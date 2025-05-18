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


const getAll = async (): Promise<ObservingBase[]> => {
  const { data } = await apiClient.get<GetAllObservingsResponse>(ENDPOINTS.OBSERVING.GET_ALL);
  return data.observings;
}

const create = async (dto: BaseCreateObservingDto): Promise<number> => {
  const { data } = await apiClient.post<CreateObservingResponse>(ENDPOINTS.OBSERVING.CREATE, dto);
  return data.observingId;
}

const remove = async (observingId: number): Promise<void> => {
  const { data } = await apiClient.delete<void>(`${ENDPOINTS.OBSERVING.DELETE}${observingId}`);
  return data;
}

const edit = async (observingId: number, dto: EditObservingDto): Promise<void> => {
  const { data } = await apiClient.patch<void>(`${ENDPOINTS.OBSERVING.EDIT}${observingId}`, dto);
  return data;
}


export const observingApi = {
  getAll,
  create,
  remove,
  edit
}