import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { alertError, alertInfo } from "../../shared/utils/alert";
import type { ObservingBase } from "../../app/models/observing";
import { observingApi } from "../../shared/api/observingApi";
import axios from "axios";
import { ObservingWithResourceId } from "../../app/models/observingWithResourceId";
import { FrequencyChanger } from "./ui/FrequencyChanger";
import { EntriesTable } from "./ui/EntriesTable";
import { CornerButton } from "../../shared/ui/CornerButton/CornerButton";
import { IoMdRefresh } from "react-icons/io";

export const ObservingPage = () => {
  const { id } = useParams();
  const [observing, setObserving] = useState<ObservingBase>();

  const fetchData = async (id: number, ac?: AbortSignal) => {
    try {
      const res = await observingApi.get(id, ac);
      setObserving(res);
    } catch (e) {
      if (!axios.isCancel(e)) {
        alertError(e as Error);
      }
    }
  }

  useEffect(() => {
    if (!id) return;

    const ac = new AbortController();
    void fetchData(+id, ac.signal);

    return () => ac.abort();
  }, [id]);

  const onRefreshButtonClicked = async (): Promise<void> => {
    if (!id) return;

    alertInfo("Обновление списка...");
    await fetchData(+id);
  }

  return (
    <div className="m-2 flex flex-col max-w-5xl mx-auto">

      <CornerButton
        onClick={() => void onRefreshButtonClicked()}
        children={<IoMdRefresh size={24} />} />
      {!observing && <div className="m-auto center p-4">Отслеживания по этой ссылке не существует, или у Вас нет к ней доступа</div>}
      {observing &&
        <div className="flex flex-col gap-2">
          <div
            className="bg-secondary-lighter p-4 text-2xl rounded-2xl overflow-hidden text-ellipsis text-nowrap"
            title={ObservingWithResourceId.getResourceId(observing)}
          >{observing.template.name}: {ObservingWithResourceId.getResourceId(observing)}</div>
          <FrequencyChanger observing={observing} />
          <EntriesTable observing={observing} />
        </div>}
    </div>);
}