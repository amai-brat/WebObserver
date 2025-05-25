import type { ObservingBase, TextObserving, YouTubePlaylistObserving } from "./observing";

export type ObservingWithResourceId = ObservingBase & { resourceId: string }

const convertFrom = (observing: ObservingBase): ObservingWithResourceId => {
  switch (observing.$type) {
    case "YouTubePlaylist":
      return {
        ...observing,
        resourceId: getResourceId(observing)
      };
    case "Text":
      return {
        ...observing,
        resourceId: getResourceId(observing)
      };
    default:
      return { ...observing, resourceId: '' };
  }
}

const getResourceId = (observing: ObservingBase): string => {
  switch (observing.$type) {
    case "YouTubePlaylist":
      {
        const o = observing as YouTubePlaylistObserving;
        return `${o.playlistName ?? ''} : ${o.playlistId}`;
      }
    case "Text":
      {
        const o = observing as TextObserving;
        return `${o.url}`;
      }
    default:
      return ''
  }
}

export const ObservingWithResourceId = {
  convertFrom,
  getResourceId
};
