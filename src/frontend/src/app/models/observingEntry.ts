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

export type DiffSummary = TextDiffSummary | YouTubePlaylistDiffSummary;

export interface TextDiffSummary {
  added: number,
  removed: number
}

export interface YouTubePlaylistDiffSummary {
  added: number,
  removed: number,
  changed: number,
  unavailable: number,
}

export interface ObservingEntry {
  id: number,
  observingId: number,
  occuredAt: string,  
  payload: ObservingPayload,
  lastDiff: DiffSummary
}

