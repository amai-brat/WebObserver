import type { ObservingEntry, UnavailableYouTubePlaylistItem } from "./observingEntry";

// templateId -> templateType
export type ObservingTypeKey = keyof typeof ObservingTypes; // 0 | 1 | 2
export type ObservingTypeValue = typeof ObservingTypes[ObservingTypeKey] // "NONE" | "YouTubePlaylist" | "Text";
export const ObservingTypes = {
  0: "NONE",
  1: "YouTubePlaylist",
  2: "Text",
} as const;

export interface ObservingTemplate {
  id: number,
  name: string,
  description: string,
  type: string
}

export interface ObservingBase {
  $type: ObservingTypeValue,
  id: number,
  startedAt: string,
  endedAt: string,
  lastEntryAt: string,
  lastChangeAt: string,
  cronExpression: string,
  template: ObservingTemplate,
  entries: ObservingEntry[];
}

export interface TextObserving extends ObservingBase{
  url: string
}

export interface YouTubePlaylistObserving extends ObservingBase {
  playlistId: string,
  playlistName?: string;
  unavailableItems: UnavailableYouTubePlaylistItem[]
}

