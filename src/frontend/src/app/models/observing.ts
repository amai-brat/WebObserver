// templateId -> templateType
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
  id: number,
  startedAt: string,
  endedAt: string,
  lastEntryAt: string,
  lastChangeAt: string,
  cronExpression: string,
  template: ObservingTemplate,
}

export interface TextObserving extends ObservingBase{
  url: string
}

export interface YouTubePlaylistObserving extends ObservingBase {
  playlistId: string
}
