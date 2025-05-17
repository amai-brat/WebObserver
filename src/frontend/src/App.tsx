import { Route, Routes } from 'react-router-dom'
import { Header } from './shared/ui/Header/Header'
import { ROUTES } from './app/consts/routes'
import { MainPage } from './pages/main/MainPage'
import { SignUpPage } from './pages/auth/SignUpPage/SignUpPage'
import { SignInPage } from './pages/auth/SignInPage/SignInPage'

function App() {

  return (
    <>
      <Header></Header>
      <Routes>
        <Route path={ROUTES.MAIN} element={<MainPage />}></Route>
        <Route path={ROUTES.SIGNUP} element={<SignUpPage />}></Route>
        <Route path={ROUTES.SIGNIN} element={<SignInPage />}></Route>
      </Routes>
    </>
  )
}

export default App
