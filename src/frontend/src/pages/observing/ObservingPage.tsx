import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { alertError } from "../../shared/utils/alert";
import type { ObservingBase } from "../../app/models/observing";
import { observingApi } from "../../shared/api/observingApi";
import axios from "axios";
import { ObservingWithResourceId } from "../../app/models/observingWithResourceId";
import { FrequencyChanger } from "./ui/FrequencyChanger";

export const ObservingPage = () => {
  const { id } = useParams();
  const [observing, setObserving] = useState<ObservingBase>();

  
  useEffect(() => {
    if (!id) return;

    const ac = new AbortController();
    void (async () => {
      try {
        const res = await observingApi.get(+id, ac.signal);
        setObserving(res);
      } catch (e) {
        if (!axios.isCancel(e)) {
          alertError(e as Error);
        }
      }
    })()

    return () => ac.abort();
  }, [id]);

  return (
    <div className="m-2 flex flex-col max-w-5xl mx-auto">
      {!observing && <div className="m-auto center p-4">Отслеживания по этой ссылке не существует, или у Вас нет к ней доступа</div>}
      {observing &&
        <div className="flex flex-col gap-2">
          <div
            className="bg-secondary-lighter p-4 text-2xl rounded-2xl overflow-hidden text-ellipsis text-nowrap"
            title={ObservingWithResourceId.getResourceId(observing)}
          >{observing.template.name}: {ObservingWithResourceId.getResourceId(observing)}</div>
          <FrequencyChanger observing={observing}/>
        </div>}
    </div>);
}