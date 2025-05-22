import type { ObservingBase, TextObserving, YouTubePlaylistObserving } from "./observing";

export type ObservingWithResourceId = ObservingBase & { resourceId: string }

const convertFrom = (observing: ObservingBase): ObservingWithResourceId => {
  switch (observing.$type) {
    case "YouTubePlaylist":
      return {
        ...observing,
        resourceId: (observing as YouTubePlaylistObserving).playlistId
      };
    case "Text":
      return {
        ...observing,
        resourceId: (observing as TextObserving).url
      };
    default:
      return { ...observing, resourceId: '' };
  }
}

const getResourceId = (observing: ObservingBase): string => {
  switch (observing.$type) {
    case "YouTubePlaylist":
      return (observing as YouTubePlaylistObserving).playlistId
    case "Text":
      return (observing as TextObserving).url
    default:
      return ''
  }
}

export const ObservingWithResourceId = {
  convertFrom,
  getResourceId
};
