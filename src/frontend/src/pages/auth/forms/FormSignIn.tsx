import {type FormikErrors, useFormik} from "formik";
import {useNavigate} from 'react-router-dom';
import formStyles from './form.module.scss';
import { toast } from "react-toastify";
import { useAuth } from "../../../app/hooks/useAuth.hook";
import type { SignInDto } from "../../../shared/api/authApi";
import { alertError } from "../../../shared/utils/alert";
import { ROUTES } from "../../../app/consts/routes";

export const FormSignIn = () => {
  const navigate = useNavigate();
  const { signin } = useAuth();

  const validate = (values: SignInDto): FormikErrors<SignInDto> => {
    const errors: FormikErrors<SignInDto> = {};

    if (!values.email){
      errors.email = 'Required';
    } else if (!/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(values.email)) {
      errors.email = 'Invalid email'
    }

    if (!values.password) {
      errors.password = 'Required';
    }
    
    return errors;
  };

  const initialValues: SignInDto = {
    email: '',
    password: ''
  };

  const formik = useFormik({
    initialValues,
    validate,
    onSubmit: async (values: SignInDto) => {
      try {
        await signin(values);
        toast("Успешний вход", {type: "success"});
        setTimeout(() => navigate(ROUTES.HOME), 2000);
      } catch (error) {
        alertError(error as Error);
      }
    }
  });
  
  return (
    <form className={formStyles.authForm + " shadow"} onSubmit={formik.handleSubmit}>
      <div className={formStyles.inputWrapper}>
        <label htmlFor={"email"}>Email</label>
        <input name={"email"}
               onChange={formik.handleChange}
               value={formik.values.email}/>
        {formik.errors.email ? <span>{formik.errors.email}</span> : null}
      </div>
      <div className={formStyles.inputWrapper}>
        <label htmlFor={"password"}>Password</label>
        <input name={"password"}
               type={"password"}
               onChange={formik.handleChange}
               value={formik.values.password}/>
        {formik.errors.password ? <span>{formik.errors.password}</span> : null}
      </div>
      <button className={formStyles.submitButton} type={"submit"}>Submit</button>
      <br/>
    </form>
  )
}