import { useFormik, type FormikErrors } from "formik";
import type { ObservingTemplate } from "../../../../app/models/observing";
import { observingApi, type YouTubePlaylistCreateObservingDto } from "../../../../shared/api/observingApi";
import { alertError } from "../../../../shared/utils/alert";
import { toast } from "react-toastify";
import cronstrue from 'cronstrue/i18n';
import axios from "axios";

interface YouTubePlaylistObservingFormProps {
  template: ObservingTemplate;
  onSuccessSubmit: () => void;
}

export const YouTubePlaylistObservingForm: React.FC<YouTubePlaylistObservingFormProps> = ({ template, onSuccessSubmit }) => {
  const availableCrons = ["* * * * *", "0 * * * *", "0 0 1 * *"];

  const validate = (values: YouTubePlaylistCreateObservingDto): FormikErrors<YouTubePlaylistCreateObservingDto> => {
    const errors: FormikErrors<YouTubePlaylistCreateObservingDto> = {};

    if (!values.playlistUrl) {
      errors.playlistUrl = 'Обязательное поле';
    } else if (!/http[s]?:\/\/(?:www\.)?youtube\.com\/.*[&?]list=([^&]+)/.test(values.playlistUrl)) {
      errors.playlistUrl = 'Неправильная ссылка'
    }

    return errors;
  };

  const initialValues: YouTubePlaylistCreateObservingDto = {
    $type: template.type,
    templateId: template.id,
    playlistUrl: '',
    playlistId: '',
    cronExpression: availableCrons[availableCrons.length - 1]
  };

  const formik = useFormik({
    initialValues,
    validate,
    onSubmit: async (values: YouTubePlaylistCreateObservingDto) => {
      try {
        const res = await observingApi.create(values);
        toast(`Успешно создано отслеживание: ${res}`, { type: "success" });
        
        onSuccessSubmit();
      } catch (e) {
        if (!axios.isCancel(e)) { 
          alertError(e as Error); 
        }
      }
    }
  });
  return (
    <div className="m-2 flex gap-1 flex-col">
      <div className="bg-primary-darker text-center p-4 rounded-2xl font-bold">{template.name}</div>
      <div className="bg-primary-darker p-4 rounded-2xl">{template.description}</div>
      <form className="flex flex-col bg-primary-darker p-4 rounded-2xl" onSubmit={formik.handleSubmit}>
        <div className="flex flex-col m-2">
          <label htmlFor={"playlistUrl"} className="pb-2">Ссылка на плейлист (должен быть не приватным): </label>
          <input name={"playlistUrl"}
            className="bg-white text-black"
            onChange={formik.handleChange}
            value={formik.values.playlistUrl} />
          {formik.errors.playlistUrl ? <span className="text-red-700">{formik.errors.playlistUrl}</span> : null}
        </div>
        <div className="flex flex-row gap-2 m-2">
          <label htmlFor={"frequency"}>Частота проверки: </label>
          <select
            name="cronExpression"
            className="appearance-auto"
            value={formik.values.cronExpression}
            onChange={formik.handleChange}>
            {availableCrons.map((cr, i) => (
              <option key={i} value={cr}>{cronstrue.toString(cr, { locale: navigator.language })}</option>
            ))}
          </select>
        </div>
        <button className="cursor-pointer self-center bg-primary p-3 mt-2 rounded hover:bg-primary-ligther" type={"submit"}>Submit</button>
        <br />
      </form>
    </div>);
}