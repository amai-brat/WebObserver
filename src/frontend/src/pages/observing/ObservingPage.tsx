import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { alertError } from "../../shared/utils/alert";
import type { ObservingBase } from "../../app/models/observing";
import { observingApi } from "../../shared/api/observingApi";
import axios from "axios";
import { ObservingWithResourceId } from "../../app/models/observingWithResourceId";
import { FrequencyChanger } from "./ui/FrequencyChanger";
import { EntriesTable } from "./ui/EntriesTable";
import { getSummaryFactory } from "./services/ObservingElementFactory";

export const ObservingPage = () => {
  const { id } = useParams();
  const [observing, setObserving] = useState<ObservingBase>();
  const factory = getSummaryFactory(observing?.$type ?? "NONE");

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

  return (
    <div className="m-2 flex flex-col max-w-5xl mx-auto">
      {!observing && <div className="m-auto center p-4">Отслеживания по этой ссылке не существует, или у Вас нет к ней доступа</div>}
      {observing &&
        <div className="flex flex-col gap-2">
          <div
            className="bg-secondary-lighter p-4 text-2xl rounded-2xl w-full"
            title={ObservingWithResourceId.getResourceId(observing)}
          >{observing.template.name}: {"\n"} 
            {ObservingWithResourceId.getResourceId(observing)}
          </div>
          <FrequencyChanger observing={observing} />
          <EntriesTable observing={observing} elementFactory={factory} />
          {factory.createAdditionalBlock(observing)}
        </div>}
    </div>);
}