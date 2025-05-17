import {FormSignUp} from "../forms/FormSignUp";
import pageStyles from './page.module.scss';

export const SignUpPage = () => {
  return (
    <div className={pageStyles.page}>
      <FormSignUp></FormSignUp>
    </div>
  );
};