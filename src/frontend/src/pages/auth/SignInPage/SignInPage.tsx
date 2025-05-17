import {FormSignIn} from "../forms/FormSignIn";
import pageStyles from '../SignUpPage/page.module.scss';

export const SignInPage = () => {
  return (
    <div className={pageStyles.page}>
      <FormSignIn></FormSignIn>
    </div>
  );
};