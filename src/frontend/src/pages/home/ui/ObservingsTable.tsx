import { useEffect, useState } from "react";
import { ObservingTypes, type ObservingBase, type TextObserving, type YouTubePlaylistObserving } from "../../../app/models/observing";
import { observingApi } from "../../../shared/api/observingApi";
import { alertError, alertSuccess } from "../../../shared/utils/alert";
import cronstrue from 'cronstrue/i18n';
import { toast } from "react-toastify";
import { IoMdRefresh } from 'react-icons/io';
import { MdDelete } from 'react-icons/md';
import axios from "axios";
import { Link } from "react-router-dom";
import { ROUTES } from "../../../app/consts/routes";

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
    const ac = new AbortController();

    (async () => {
      try {
        const resp = await observingApi.getAll(ac.signal);
        setObservings(resp.map(toGridEntryData));
      } catch (e) {
        if (!axios.isCancel(e)) {
          alertError(e as Error);
        }
      }
    })()

    return () => {
      ac.abort();
    };
  }, []);


  const copyToClipBoard = async (toCopy: string): Promise<void> => {
    try {
      await navigator.clipboard.writeText(toCopy);
      toast("Скопировано", { type: "success" });
    } catch (err) {
      console.error('Failed to copy text: ', err);
    }
  }

  const onDeleteButtonClicked = async (id: number): Promise<void> => {
    try {
      await observingApi.remove(id);
      setObservings(prev => prev?.filter(o => o.id != id));
      alertSuccess("Успешно удалено");
    } catch (e) {
      if (!axios.isCancel(e)) {
        alertError(e as Error);
      }
    }
  }

  const onRefreshButtonClicked = async (): Promise<void> => {
    try {
      const resp = await observingApi.getAll();
      setObservings(resp.map(toGridEntryData));
    } catch (e) {
      if (!axios.isCancel(e)) {
        alertError(e as Error);
      }
    }
  }

  return <>
    <div className="bg-secondary-lighter max-w-5xl mx-auto shadow rounded">
      <div className="bg-primary grid grid-cols-[2fr_2fr_2fr_2fr_2fr_0.5fr] gap-1 px-4 text-center text-white border-b-2 border-secondary rounded">
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Шаблон</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Ресурс</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Последняя проверка</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Последнее изменение</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Частотность проверки</div>
        <div className="p-4 self-center m-auto cursor-pointer"><IoMdRefresh size={36} onClick={() => onRefreshButtonClicked()} /></div>
      </div>
      {(!observings || observings?.length == 0) && <p>У вас пока нет отслеживаемых ресурсов</p>}
      {observings && observings.map(o => (
        <div key={o.id}>
          <div className="grid grid-cols-[2fr_2fr_2fr_2fr_2fr_0.5fr] gap-1 px-4 hover:bg-secondary">
            <Link to={ROUTES.OBSERVING.replace(":id", `${o.id}`)} className="contents">
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
                className="py-4 px-1 border-r-1 border-secondary text-center line-clamp-2 overflow-hidden"
                title={cronstrue.toString(o.cronExpression, { locale: navigator.language })}
              >
                {cronstrue.toString(o.cronExpression, { locale: navigator.language })}
              </div>
            </Link>

            <div
              className="p-4 text-center self-center m-auto"
            >
              <MdDelete size={36} color="red" onClick={(e) => {
                e.preventDefault();
                onDeleteButtonClicked(o.id)
              }} />
            </div>
          </div>
          <hr className="text-secondary" />
        </div>
      ))}
    </div>
  </>;
}