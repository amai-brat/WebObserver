import type { ObservingBase } from "../../../app/models/observing";
import { Link } from "react-router-dom";
import { ROUTES } from "../../../app/consts/routes";
import { getSummaryFactory } from "../services/SummaryComponentFactory";

interface EntriesTableProps {
  observing: ObservingBase
}

export const EntriesTable: React.FC<EntriesTableProps> = ({ observing }) => {
  const factory = getSummaryFactory(observing.$type);


  return <>
    <div className="bg-secondary-lighter max-w-5xl w-full mx-auto shadow rounded">
      <div className="bg-primary grid grid-cols-[2fr_2fr_2fr] gap-1 px-4 text-center text-white border-b-2 border-secondary rounded">
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Время проверки</div>
        <div className="p-4 border-r-1 border-secondary line-clamp-2 overflow-hidden">Объём изменений</div>
        <div className="p-4 line-clamp-2 overflow-hidden">Состояние ресурса</div>
      </div>
      {observing?.entries.map(e => (
        <div key={e.id}>
          <div className="grid grid-cols-[2fr_2fr_2fr] gap-1 px-4 hover:bg-secondary items-center">
            <Link to={ROUTES.OBSERVING.replace(":id", `${e.id}`)} className="contents">
              <div
                className="py-4 px-1 text-center border-r-1 border-secondary line-clamp-2 overflow-hidden"
                title={new Date(e.occuredAt).toLocaleString()}
              >
                {new Date(e.occuredAt).toLocaleString()}
              </div>
              <div
                className="py-4 px-1 text-center border-r-1 border-secondary line-clamp-2 overflow-hidden"
              >
                {factory.createDiffSummaryElement(e.lastDiff)}
              </div>
              <div
                className="py-4 px-1 text-centerline-clamp-2 overflow-hidden"
              >
                {factory.createPayloadSummaryElement(e.payloadSummary)}
              </div>
            </Link>
          </div>
          <hr className="text-secondary" />
        </div>
      ))}
    </div>
  </>;
}