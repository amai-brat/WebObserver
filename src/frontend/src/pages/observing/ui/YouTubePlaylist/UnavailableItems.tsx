import { Link } from "react-router-dom";
import type { UnavailableYouTubePlaylistItem, YouTubePlaylistItem } from "../../../../app/models/observingEntry"
import type React from "react";

interface UnavailableItemsProps {
  items: UnavailableYouTubePlaylistItem[];
}

export const UnavailableItems: React.FC<UnavailableItemsProps> = ({ items }) => {
  return (
    <div>
      <div className="bg-primary-darker text-white w-fit p-2 rounded-t-lg">Список недоступных роликов: {items.length}</div>
      <div className="bg-secondary-lighter rounded-b rounded-r overflow-scroll flex flex-col gap-0.5 shadow">
        {items.map(item => (
          <div key={item.id} className="bg-primary m-1 text-white shadow rounded">
            <p className="mb-2 p-2 bg-primary-darker w-fit rounded">Позиция: {item.currentItem.position}</p>
            <p className="p-2 bg-primary-darker w-fit rounded">Текущая версия: </p>
            <PlaylistItemComponent item={item.currentItem} />
            <p className="p-2 bg-primary-darker w-fit rounded">Сохранённая версия: </p>
            <PlaylistItemComponent item={item.savedItem} />
          </div>
        ))}
      </div>
    </div>
  );
}


interface PlaylistItemComponentProps {
  item?: YouTubePlaylistItem;
}
const PlaylistItemComponent: React.FC<PlaylistItemComponentProps> = ({ item }) => {
  const fallbackThumbnail = "https://files.catbox.moe/nvekkp.png"

  return (
    <>
      {!item && <div className="mt-1 bg-primary-darker max-w-full w-fit p-2 rounded">
        <p className="">К сожалению, сохранённой версии нет :( </p>
        <Link 
          to={`https://web.archive.org/web/*/https://youtu.be/EpRRRoXcU6s`}
          className="hover:underline">
          Поискать в WebArchive
        </Link>
      </div>}
      {item && (<div className="grid grid-cols-[1fr_3fr] text-white">
        <div className="flex flex-col p-2 w-full">
          <Link to={`https://youtu.be/${item.videoId}`}>
            <img
              src={item.thumbnailUrl ?? fallbackThumbnail}
              className="w-full"></img>
          </Link>
        </div>
        <div className="flex flex-col p-4 pl-0.5 w-full min-w-0">
          <div className="bg-primary-darker max-w-full w-fit p-2 rounded">
            <p className="truncate" title={item.title}>Название: {item.title}</p>
            <p className="truncate" title={item.description}>Описание: {item.description}</p>
            <p className="truncate" title={item.publishedAt}>Опубликовано: {new Date(item.publishedAt ?? new Date(0)).toLocaleString()}</p>
            <p className="truncate" title={item.videoOwnerChannelTitle}>Канал: {item.videoOwnerChannelTitle ?? 'null'}</p>
          </div>
        </div>
      </div>)}
    </>
  );
}