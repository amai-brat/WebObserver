// {
//   "url": "https://gist.githubusercontent.com/amai-brat/9e8306de3f3eb87da52c30d7197a1cc8/raw/gistfile1.txt",
//   "id": 2,
//   "startedAt": "2025-05-18T11:40:37.899177Z",
//   "endedAt": null,
//   "lastEntryAt": "2025-05-18T11:42:08.795136Z",
//   "lastChangeAt": "2025-05-18T11:40:37.899177Z",
//   "template": {
//     "id": 2,
//     "name": "Текстовый файл",
//     "description": "Отслеживает за текстовым файлом (не бинари)"
//   },
//   "cronExpression": "* * * * *"
// }

// export const ObservingTypes = {
//   "NONE": "NONE",
//   "YouTubePlaylist": "YouTubePlaylist",
//   "Text": "Text",
// } as const;

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
