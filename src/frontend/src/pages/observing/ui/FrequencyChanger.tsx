import { useFormik } from "formik";
import type { ObservingBase } from "../../../app/models/observing";
import { observingApi, type EditObservingDto } from "../../../shared/api/observingApi";
import axios from "axios";
import { alertError, alertSuccess } from "../../../shared/utils/alert";
import cronstrue from 'cronstrue/i18n';

interface FrequencyChangerProps {
  observing: ObservingBase;
}

export const FrequencyChanger: React.FC<FrequencyChangerProps> = ({ observing }) => {
  const availableCrons = ["* * * * *", "0 * * * *", "0 0 1 * *"];

  const initialValues: EditObservingDto = {
    cronExpression: observing.cronExpression
  };

  const formik = useFormik({
    initialValues,
    onSubmit: async (values: EditObservingDto) => {
      try {
        await observingApi.edit(observing.id, values);
        alertSuccess(`Успешно изменено`);

      } catch (e) {
        if (!axios.isCancel(e)) {
          alertError(e as Error);
        }
      }
    }
  });

  return (
    <form
      className="bg-secondary-lighter p-2 max-w-fit rounded-2xl flex flex-row gap-2 items-center"
      onSubmit={formik.handleSubmit}
    >
      <label htmlFor={"cronExpression"}>Частота проверки: </label>
      <select
        id="cronExpression"
        name="cronExpression"
        className="appearance-auto"
        value={formik.values.cronExpression}
        onChange={formik.handleChange}>
        {availableCrons.map((cr, i) => (
          // eslint-disable-next-line react-x/no-array-index-key
          <option key={i} value={cr}>{cronstrue.toString(cr, { locale: navigator.language })}</option>
        ))}
      </select>
      <button className="cursor-pointer self-center bg-primary p-2 mt-2 rounded text-white hover:bg-primary-ligther" type={"submit"}>Изменить</button>
    </form>
  )
}