import { useEffect, useState } from "react"
import type { ObservingTemplate } from "../../../app/models/observing";
import { templateApi } from "../../../shared/api/templateApi";
import { alertError } from "../../../shared/utils/alert";
import { YouTubePlaylistObservingForm } from "./forms/YouTubePlaylistObservingForm";
import { TextObservingForm } from "./forms/TextObservingForm";

interface ObservingFormSelectorProps {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void; 
}

export const ObservingFormSelector: React.FC<ObservingFormSelectorProps> = ({ setIsModalOpen }) => {
  const [templates, setTemplates] = useState<ObservingTemplate[]>([]);
  const [selectedTemplate, setSelectedTemplate] = useState<ObservingTemplate | null>(null);

  useEffect(() => {
    const ac = new AbortController();

    (async () => {
      try {
        const result = await templateApi.getAll();
        setTemplates(result);
      } catch (e) {
        alertError(e as Error);
      }
    })()

    return () => {
      ac.abort();
    };
  }, []);

  const handleTemplateSelect = (templateId: number) => {
    const template = templates.find(t => t.id === templateId);
    setSelectedTemplate(template || null);
  };

  const renderForm = () => {
    if (!selectedTemplate) return null;
    
    switch (selectedTemplate.type) {
      case 'YouTubePlaylist':
        return <YouTubePlaylistObservingForm template={selectedTemplate} onSuccessSubmit={() => setIsModalOpen(false)}/>;
      case 'Text':
        return <TextObservingForm template={selectedTemplate} onSuccessSubmit={() => setIsModalOpen(false)}/>;
      default:
        return <div>Unknown template type</div>;
    }
  };

  return (
    <div className="flex-1 m-4 grid grid-cols-[2.5fr_5fr] gap-2 rounded-2xl text-white">
      <div className="bg-white rounded-2xl min-w-0 overflow-scroll">
        <div className="rounded-2xl bg-primary-darker m-2">
          <div className="text-center p-4 font-bold">Шаблоны</div>
          <div className="p-4 bg-primary rounded-2xl w-full">
            {templates && templates.map(t => (
              <div
                key={t.id}
                title={t.name}
                onClick={() => handleTemplateSelect(t.id)}
                className={`mb-1 cursor-pointer hover:font-bold overflow-hidden overflow-ellipsis text-nowrap ${
                  selectedTemplate?.id === t.id ? 'font-bold' : ''
                }`}>
                {t.name}
              </div>))}
          </div>
        </div>
      </div>
      <div className="bg-white rounded-2xl w-full overflow-scroll">
        {renderForm() || <div className="p-4 text-primary-darker">Выберите шаблон слева</div>}
      </div>
    </div>);
}