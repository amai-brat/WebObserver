export interface ObservingPayload {
  observingEntryId: number, 
}

export interface TextObservingPayload extends ObservingPayload {
  text: string,
}

export interface YouTubePlaylistObservingPayload extends ObservingPayload {
  items: YouTubePlaylistItem[],
}

interface YouTubePlaylistItem {
  id: number,
  videoId: string,
  title: string,
  description: string,
  position: number,
  thumbnailUrl: string,
  publishedAt: string,
  videoOwnerChannelTitle: string,
  status: string
}

export type DiffPayload = TextDiffPayload | YouTubePlaylistDiffPayload;

export interface TextDiffPayload {
  added: string[],
  removed: string[],
  isEmpty: boolean,
}

export interface YouTubePlaylistDiffPayload {
  added: YouTubePlaylistItem[],
  removed: YouTubePlaylistItem[],
  changed: YouTubePlaylistItem[],
  unavailable: YouTubePlaylistItem[],
}

export interface ObservingEntry {
  id: number,
  observingId: number,
  occuredAt: string,  
  payload: ObservingPayload,
  lastDiff: DiffPayload
}

