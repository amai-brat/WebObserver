import { useEffect, useState } from "react";
import { observingApi } from "../../../shared/api/observingApi";
import { alertError, alertSuccess } from "../../../shared/utils/alert";
import cronstrue from 'cronstrue/i18n';
import { IoMdRefresh } from 'react-icons/io';
import { MdDelete } from 'react-icons/md';
import axios from "axios";
import { Link } from "react-router-dom";
import { ROUTES } from "../../../app/consts/routes";
import { FaClipboard } from "react-icons/fa";
import { ObservingWithResourceId } from "../../../app/models/observingWithResourceId";

export const ObservingsTable = () => {
  const [observings, setObservings] = useState<ObservingWithResourceId[]>();
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const fetchData = async (ac?: AbortSignal, isMounted?: boolean) => {
    try {
      setIsLoading(true)
      const resp = await observingApi.getAll(ac);
      setObservings(resp.map(ObservingWithResourceId.convertFrom));
    } catch (e) {
      if (!axios.isCancel(e)) {
        alertError(e as Error);
      }
    }
    finally {
      if (isMounted || isMounted === undefined)
        setIsLoading(false);
    }
  }
  useEffect(() => {
    const ac = new AbortController();
    let isMounted = true;

    void fetchData(ac.signal, isMounted);
    return () => {
      isMounted = false;
      ac.abort();
    };
  }, []);


  const copyToClipBoard = async (toCopy: string): Promise<void> => {
    try {
      await navigator.clipboard.writeText(toCopy);
      alertSuccess(`Скопировано: ${toCopy}`);
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
    await fetchData();
  }

  return <>
    <div className="bg-secondary-lighter max-w-5xl mx-auto shadow rounded overflow-auto">
      <div className="bg-primary w-max max-w-5xl grid grid-cols-[2fr_2fr_2fr_2fr_2fr_0.5fr] gap-1 text-center text-white border-b-2 border-secondary rounded">
        <div className="p-4 border-r-1 border-secondary">Шаблон</div>
        <div className="p-4 border-r-1 border-secondary">Ресурс</div>
        <div className="p-4 border-r-1 border-secondary">Последняя проверка</div>
        <div className="p-4 border-r-1 border-secondary">Последнее изменение</div>
        <div className="p-4 border-r-1 border-secondary">Частотность проверки</div>
        <div className="p-4 self-center m-auto cursor-pointer"><IoMdRefresh size={36} onClick={() => void onRefreshButtonClicked()} /></div>
      </div>
      {isLoading && <p>Загрузка...</p>}
      {!isLoading && (!observings || observings?.length == 0) && <p>У вас пока нет отслеживаемых ресурсов</p>}
      {observings?.map(o => (
        <div key={o.id}>
          <div className="w-max max-w-5xl grid grid-cols-[2fr_2fr_2fr_2fr_2fr_0.5fr] gap-1 hover:bg-secondary items-center justify-center">
            <Link to={ROUTES.OBSERVING.replace(":id", `${o.id}`)} className="contents">
              <div
                className="py-4 px-1 h-full min-w-0 text-center border-r-1 border-secondary"
                title={o.template.name}
              >
                {o.template.name}
              </div>

              <div
                className="py-4 px-1 h-full min-w-0 text-center border-r-1 border-secondary"
                title={o.resourceId}
                onClick={() => void copyToClipBoard(o.resourceId)}
              >
                <p className="truncate">{o.resourceId}</p>
                <button type="button" className="cursor-pointer" onClick={(e) => {
                  e.preventDefault();
                  void copyToClipBoard(o.resourceId);
                }}><FaClipboard size={24} color="purple" /></button>
              </div>

              <div
                className="py-4 px-1 h-full w-full min-w-0 text-center border-r-1 border-secondary"
                title={new Date(o.lastEntryAt).toLocaleString()}
              >
                {new Date(o.lastEntryAt).toLocaleString()}
              </div>

              <div
                className="py-4 px-1 h-full w-full min-w-0 text-center border-r-1 border-secondary"
                title={new Date(o.lastChangeAt).toLocaleString()}
              >
                {new Date(o.lastChangeAt).toLocaleString()}
              </div>

              <div
                className="py-4 px-1 h-full w-full min-w-0 border-r-1 border-secondary text-center"
                title={cronstrue.toString(o.cronExpression, { locale: navigator.language })}
              >
                {cronstrue.toString(o.cronExpression, { locale: navigator.language })}
              </div>
            </Link>

            <div
              className="p-4 text-center h-full w-full self-center m-auto"
            >
              <MdDelete size={36} color="red" className="cursor-pointer" onClick={(e) => {
                e.preventDefault();
                void onDeleteButtonClicked(o.id)
              }} />
            </div>
          </div>
          <hr className="text-secondary" />
        </div>
      ))}
    </div>
  </>;
}