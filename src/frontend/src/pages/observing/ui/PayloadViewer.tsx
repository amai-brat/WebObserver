import axios from "axios";
import { useEffect, useState } from "react"
import { observingApi } from "../../../shared/api/observingApi";
import { alertError } from "../../../shared/utils/alert";

interface PayloadViewerProps {
  observingId: number,
  entryId: number,
}

export const PayloadViewer: React.FC<PayloadViewerProps> = ({ observingId, entryId }) => {
  const [payload, setPayload] = useState<string>('');

  useEffect(() => {
    const ac = new AbortController();

    void (async () => {
      try {
        const res = await observingApi.getEntryPayload(observingId, entryId, ac.signal);
        setPayload(JSON.stringify(res, null, 2));
      } catch (e) {
        if (!axios.isCancel(e)) {
          alertError(e as Error);
        }
      }
    })()

    return () => ac.abort();
  }, [observingId, entryId]);

  return (
    <div className="m-8 flex flex-col w-full gap-2">
      <div className="text-white text-3xl text-center">Содержимое записи:</div>
      <textarea className="bg-white rounded-t-xs h-full" readOnly value={payload}></textarea>
    </div>
  )
}