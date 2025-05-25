import type { JSX } from "react";
import type { DiffSummary, ObservingPayloadSummary, TextDiffSummary, TextPayloadSummary, YouTubePlaylistDiffSummary, YouTubePlaylistPayloadSummary } from "../../../app/models/observingEntry";
import type { ObservingTypeValue } from "../../../app/models/observing";

export interface SummaryElementFactory {
  createPayloadSummaryElement(summary: ObservingPayloadSummary): JSX.Element;
  createDiffSummaryElement(summary?: DiffSummary): JSX.Element;
}

const fallbackWhenNull = () => {
  return (
    <div className="flex flex-row gap-1 justify-center">
      <div className="text-gray-500">-</div>
    </div>
  )
}

export class TextSummaryFactory implements SummaryElementFactory {
  createPayloadSummaryElement(summary: ObservingPayloadSummary): JSX.Element {
    if (!summary) return fallbackWhenNull();

    const t = summary as TextPayloadSummary;
    return (
      <div className="flex flex-row gap-1 justify-center">
        <div>Длина: {t.length}</div>
        <div>Строк: {t.linesCount}</div>
      </div>
    );
  }

  createDiffSummaryElement(summary?: DiffSummary): JSX.Element {
    if (!summary) return fallbackWhenNull();

    const t = summary as TextDiffSummary;
    return (
      <div className="flex flex-row gap-1 justify-center">
        <div className="text-green-500">+{t.added}</div>
        <div className="text-red-500"> -{t.removed}</div>
      </div>
    );
  }
}

export class YouTubePlaylistSummaryFactory implements SummaryElementFactory {
  createPayloadSummaryElement(summary: ObservingPayloadSummary): JSX.Element {
    if (!summary) return fallbackWhenNull();

    const t = summary as YouTubePlaylistPayloadSummary;
    return (
      <div className="flex flex-row gap-1 justify-center">
        <div>Количество роликов: {t.itemsCount}, недоступных: {t.unavailableItemsCount}</div>
      </div>
    );
  }

  createDiffSummaryElement(summary?: DiffSummary): JSX.Element {
    if (!summary) return fallbackWhenNull();

    const t = summary as YouTubePlaylistDiffSummary;
    return (
      <div className="flex flex-row gap-1 justify-center"
        title={`добавлено: ${t.added}, удалено: ${t.removed}, изменено: ${t.changed}, стало недоступно: ${t.unavailable}`}>
        <div className="text-green-500">+{t.added}</div>
        <div className="text-red-800">-{t.removed}</div>
        <div className="text-yellow-500">~{t.changed}</div>
        <div className="text-red-500">×{t.unavailable}</div>
      </div>
    );
  }
}

export function getSummaryFactory(type: ObservingTypeValue): SummaryElementFactory {
  switch (type) {
    case 'Text':
      return new TextSummaryFactory();
    case 'YouTubePlaylist':
      return new YouTubePlaylistSummaryFactory();
    case 'NONE':
    default:
      throw new Error(`No factory found for ${type}`);
  }
}