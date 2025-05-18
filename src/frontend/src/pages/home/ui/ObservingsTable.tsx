import { useEffect, useState } from "react";
import { ObservingTypes, type ObservingBase, type TextObserving, type YouTubePlaylistObserving } from "../../../app/models/observing";
import { observingApi } from "../../../shared/api/observingApi";
import { alertError } from "../../../shared/utils/alert";
import cronstrue from 'cronstrue/i18n';
import { toast } from "react-toastify";

type ObservingGridEntryData = ObservingBase & { resourceId: string }

export const ObservingsTable = () => {
  const [observings, setObservings] = useState<ObservingGridEntryData[]>();

  const toGridEntryData = (observing: ObservingBase): ObservingGridEntryData => {
    const isValidTemplateId = (id: number): id is keyof typeof ObservingTypes => {
      return id in ObservingTypes;
    }

    const id = observing.template.id;
    if (!isValidTemplateId(id)) {
      throw new Error('Not valid template id given');
    }

    switch (ObservingTypes[id]) {
      case "YouTubePlaylist":
        const ytPl = observing as YouTubePlaylistObserving;
        return { ...observing, resourceId: ytPl.playlistId }
      case "Text":
        const tex = observing as TextObserving;
        return { ...observing, resourceId: tex.url };
      default:
        return { ...observing, resourceId: '' };
    }
  }

  useEffect(() => {
    (async () => {
      try {
        const resp = await observingApi.getAll();
        setObservings(resp.map(toGridEntryData));
      } catch (e) {
        alertError(e as Error);
      }
    })()
  }, []);


  const copyToClipBoard = async (toCopy: string): Promise<void> => {
    try {
      await navigator.clipboard.writeText(toCopy);
      toast("Скопировано", { type: "success" });
    } catch (err) {
      console.error('Failed to copy text: ', err);
    }
  }

  return <>
    <div className="bg-secondary-lighter max-w-5xl mx-auto shadow rounded">
      <div className="bg-primary grid grid-cols-5 gap-1 px-4 text-center text-white border-b-2 border-secondary rounded">
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Шаблон</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Ресурс</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Последняя проверка</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Последнее изменение</div>
        <div className="p-4 ">Частотность проверки</div>
      </div>
      {!observings && <p>У вас пока нет отслеживаемых ресурсов</p>}
      {observings && observings.map(o => (
        <>
          <div key={o.id} className="grid grid-cols-5 gap-1 px-4">
            <div
              className="py-4 px-1 text-center border-r-1 border-secondary line-clamp-2 overflow-hidden"
              title={o.template.name}
            >
              {o.template.name}
            </div>

            <div
              className="py-4 px-1 text-center border-r-1 border-secondary overflow-hidden overflow-ellipsis text-nowrap cursor-pointer"
              title={o.resourceId}
              onClick={() => copyToClipBoard(o.resourceId)}
            >
              {o.resourceId}
            </div>

            <div
              className="py-4 px-1 text-center border-r-1 border-secondary line-clamp-2 overflow-hidden"
              title={new Date(o.lastEntryAt).toLocaleString()}
            >
              {new Date(o.lastEntryAt).toLocaleString()}
            </div>

            <div
              className="py-4 px-1 text-center border-r-1 border-secondary line-clamp-2 overflow-hidden"
              title={new Date(o.lastChangeAt).toLocaleString()}
            >
              {new Date(o.lastChangeAt).toLocaleString()}
            </div>

            <div
              className="p-4 text-center line-clamp-2 overflow-hidden"
              title={cronstrue.toString(o.cronExpression, { locale: navigator.language })}
            >
              {cronstrue.toString(o.cronExpression, { locale: navigator.language })}
            </div>
          </div>
          <hr className="text-secondary" />
        </>
      ))}
    </div>
  </>;
}