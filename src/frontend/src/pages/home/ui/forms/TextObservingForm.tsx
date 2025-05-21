import { useFormik, type FormikErrors } from "formik";
import type { ObservingTemplate } from "../../../../app/models/observing"
import { observingApi, type TextCreateObservingDto } from "../../../../shared/api/observingApi";
import cronstrue from 'cronstrue/i18n';
import { alertError, alertSuccess } from "../../../../shared/utils/alert";
import axios from "axios";

interface TextObservingFormProps {
  template: ObservingTemplate;
  onSuccessSubmit: () => void;
}

export const TextObservingForm: React.FC<TextObservingFormProps> = ({ template, onSuccessSubmit }) => {
  const availableCrons = ["* * * * *", "0 * * * *", "0 0 1 * *"];

  const validate = (values: TextCreateObservingDto): FormikErrors<TextCreateObservingDto> => {
    const errors: FormikErrors<TextCreateObservingDto> = {};

    if (!values.url) {
      errors.url = 'Обязательное поле';
    } else if (!/http[s]?:\/\/[\w-]+\.[\w]+.*/.test(values.url)) {
      errors.url = 'Неправильная ссылка'
    }

    return errors;
  };

  const initialValues: TextCreateObservingDto = {
    $type: template.type,
    templateId: template.id,
    url: '',
    cronExpression: availableCrons[availableCrons.length - 1]
  };

  const formik = useFormik({
    initialValues,
    validate,
    onSubmit: async (values: TextCreateObservingDto) => {
      try {
        const res = await observingApi.create(values);
        alertSuccess(`Успешно создано отслеживание: ${res}`);
        
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
          <label htmlFor={"url"} className="pb-2">Ссылка на ресурс: </label>
          <input name={"url"}
            className="bg-white text-black"
            onChange={formik.handleChange}
            value={formik.values.url} />
          {formik.errors.url ? <span className="text-red-700">{formik.errors.url}</span> : null}
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