export interface ObservingPayload {
  observingEntryId: number, 
}

export interface TextObservingPayload extends ObservingPayload {
  text: string,
}

export interface YouTubePlaylistObservingPayload extends ObservingPayload {
  items: YouTubePlaylistItem[],
}

export interface YouTubePlaylistItem {
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

export interface UnavailableYouTubePlaylistItem {
  id: number,
  savedItemId?: number,
  savedItem?: YouTubePlaylistItem,
  currentItemId: number,
  currentItem: YouTubePlaylistItem,
}

export type ObservingPayloadSummary = TextPayloadSummary | YouTubePlaylistPayloadSummary;
export interface TextPayloadSummary {
  $type: "Text",
  length: number,
  linesCount: number,
}
export interface YouTubePlaylistPayloadSummary {
  $type: "YouTubePlaylist"
  itemsCount: number,
  unavailableItemsCount: number,
}

export type DiffSummary = TextDiffSummary | YouTubePlaylistDiffSummary;

export interface TextDiffSummary {
  $type: "Text",
  added: number,
  removed: number
}

export interface YouTubePlaylistDiffSummary {
  $type: "YouTubePlaylist"
  added: number,
  removed: number,
  changed: number,
  unavailable: number,
}

export interface ObservingEntry {
  id: number,
  observingId: number,
  occuredAt: string,  
  payloadSummary: ObservingPayloadSummary,
  lastDiff: DiffSummary
}

