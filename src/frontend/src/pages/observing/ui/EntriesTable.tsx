import type { ObservingBase } from "../../../app/models/observing";
import { getSummaryFactory } from "../services/SummaryComponentFactory";
import { useEffect, useState } from "react";
import { Modal } from "../../../shared/ui/Modal/Modal";
import { DiffViewer } from "./DiffViewer";
import { PayloadViewer } from "./PayloadViewer";
import { FaArrowLeft, FaArrowRight } from "react-icons/fa";
import type { ObservingEntry } from "../../../app/models/observingEntry";
import { observingApi } from "../../../shared/api/observingApi";
import axios from "axios";
import { alertError, alertInfo } from "../../../shared/utils/alert";
import { CornerButton } from "../../../shared/ui/CornerButton/CornerButton";
import { IoMdRefresh } from "react-icons/io";

interface EntriesTableProps {
  observing: ObservingBase
}

export const EntriesTable: React.FC<EntriesTableProps> = ({ observing }) => {
  const pageSize = 5;

  const [entriesCount, setEntriesCount] = useState<number>(0);
  const [page, setPage] = useState<number>(1);
  const [entries, setEntries] = useState<ObservingEntry[]>();
  const [isDiffModalOpen, setIsDiffModalOpen] = useState(false);
  const [isPayloadModalOpen, setIsPayloadModalOpen] = useState(false);
  const [currentEntryId, setCurrentEntryId] = useState<number>();
  const factory = getSummaryFactory(observing.$type);

  const onDiffSummaryClick = (entryId: number) => {
    setCurrentEntryId(entryId)
    setIsDiffModalOpen(true)
  }

  const onPayloadSummaryClick = (entryId: number) => {
    setCurrentEntryId(entryId)
    setIsPayloadModalOpen(true)
  }

  const fetchData = async (observingId: number, page: number, pageSize: number, ac?: AbortSignal) => {
    try {
      const res = await observingApi.getEntries(observingId, page, pageSize, ac);
      setEntries(res.entries);
      setEntriesCount(res.length);
    } catch (e) {
      if (!axios.isCancel(e)) {
        alertError(e as Error);
      }
    }
  }

  useEffect(() => {
    const ac = new AbortController();

    void fetchData(observing.id, page, pageSize, ac.signal);

    return () => ac.abort();
  }, [observing, page]);

  const onRefreshButtonClicked = async (): Promise<void> => {
    alertInfo("Обновление списка...");
    await fetchData(observing.id, page, pageSize);
  }

  return <>
    <CornerButton
      onClick={() => void onRefreshButtonClicked()}
      children={<IoMdRefresh size={24} />} />
    <Modal
      isOpen={isDiffModalOpen}
      onRequestClose={() => setIsDiffModalOpen(false)}
    >
      <DiffViewer observingId={observing.id} entryId={currentEntryId ?? 0}></DiffViewer>
    </Modal>
    <Modal
      isOpen={isPayloadModalOpen}
      onRequestClose={() => setIsPayloadModalOpen(false)}
    >
      <PayloadViewer observingId={observing.id} entryId={currentEntryId ?? 0}></PayloadViewer>
    </Modal>
    <div className="bg-secondary-lighter max-w-5xl w-full mx-auto shadow rounded">
      <div className="bg-primary grid grid-cols-[2fr_2fr_2fr] gap-1 px-4 text-center text-white border-b-2 border-secondary rounded">
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Время проверки</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Объём изменений</div>
        <div className="p-4 line-clamp-2 overflow-hidden">Состояние ресурса</div>
      </div>
      {entries?.map(e => (
        <div key={e.id}>
          <div className="grid grid-cols-[2fr_2fr_2fr] px-4 items-center">
            <div
              className="py-4 px-1 text-center border-r-1 border-secondary line-clamp-2 overflow-hidden"
              title={new Date(e.occuredAt).toLocaleString()}
            >
              {new Date(e.occuredAt).toLocaleString()}
            </div>
            <div
              className="py-4 px-1 text-center border-r-1 border-secondary line-clamp-2 overflow-hidden hover:bg-secondary cursor-pointer"
              onClick={() => void onDiffSummaryClick(e.id)}
            >
              {factory.createDiffSummaryElement(e.lastDiff)}
            </div>
            <div
              className="py-4 px-1 text-centerline-clamp-2 overflow-hidden hover:bg-secondary cursor-pointer"
              onClick={() => void onPayloadSummaryClick(e.id)}
            >
              {factory.createPayloadSummaryElement(e.payloadSummary)}
            </div>
          </div>
          <hr className="text-secondary" />
        </div>
      ))}
      <div className="bg-primary flex flex-row items-center justify-center gap-4 p-2 rounded">
        <FaArrowLeft className="cursor-pointer" color="white" size={24}
          onClick={() => setPage(prev => {
            if (prev <= 1) {
              return prev;
            }
            return prev - 1;
          })} />
        <div className="text-white text-xl select-none">{page}</div>
        <FaArrowRight className="cursor-pointer" color="white" size={24}
          onClick={() => setPage(prev => {
            if ((prev + 1) * pageSize > entriesCount) {
              return prev;
            }
            return prev + 1;
          })} />
      </div>
    </div>
  </>;
}