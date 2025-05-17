import { type FormikErrors, useFormik } from "formik";
import formStyles from './form.module.scss';
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { authApi, type SignUpDto } from '../../../shared/api/authApi';
import { ROUTES } from "../../../app/consts/routes";
import { alertError } from "../../../shared/utils/alert";

export const FormSignUp = () => {
  const navigate = useNavigate();

  const validate = (values: SignUpDto): FormikErrors<SignUpDto> => {
    const errors: FormikErrors<SignUpDto> = {};
    if (!values.name) {
      errors.name = 'Required';
    } else if (!/[^\s-]/.test(values.name)) {
      errors.name = 'Name is not whitespace';
    }

    if (!values.email) {
      errors.email = 'Required';
    } else if (!/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(values.email)) {
      errors.email = 'Invalid email'
    }

    if (!values.password) {
      errors.password = 'Required';
    } else if (!/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/.test(values.password)) {
      errors.password = 'Minimum eight characters, at least one upper case letter, one lower case letter, one number and one special character.';
    }

    return errors;
  };

  const formik = useFormik<SignUpDto>({
    initialValues: { name: '', email: '', password: '' },
    validate,
    onSubmit: async (values: SignUpDto) => {
      try {
        const data = await authApi.signup(values);
        toast("Успешная регистрация. Перенаправление на вход...", { type: "success" });
        setTimeout(() => navigate(ROUTES.SIGNIN), 1000);
        console.log(data);
      }
      catch (error) {
        alertError(error as Error);
      }
    }
  });

  return (
    <form className={formStyles.authForm} onSubmit={formik.handleSubmit}>
      <div className={formStyles.inputWrapper}>
        <label htmlFor={"name"}>Name</label>
        <input name={"name"}
          onChange={formik.handleChange}
          value={formik.values.name} />
        {formik.errors.name ? <span>{formik.errors.name}</span> : null}
      </div>
      <div className={formStyles.inputWrapper}>
        <label htmlFor={"email"}>Email</label>
        <input name={"email"}
          onChange={formik.handleChange}
          value={formik.values.email} />
        {formik.errors.email ? <span>{formik.errors.email}</span> : null}
      </div>
      <div className={formStyles.inputWrapper}>
        <label htmlFor={"password"}>Password</label>
        <input name={"password"}
          type="password"
          onChange={formik.handleChange}
          value={formik.values.password} />
        {formik.errors.password ? <span>{formik.errors.password}</span> : null}
      </div>
      <button className={formStyles.submitButton} type={"submit"}>Submit</button>
      <br />
    </form>
  )
}