import { Route, Routes } from 'react-router-dom'
import { Header } from './shared/ui/Header/Header'
import { ROUTES } from './app/consts/routes'
import { MainPage } from './pages/main/MainPage'
import { SignUpPage } from './pages/auth/SignUpPage/SignUpPage'
import { SignInPage } from './pages/auth/SignInPage/SignInPage'
import { Toast } from './shared/ui/Toast/Toast'
import { HomePage } from './pages/home/HomePage'
import { ProtectedRoute } from './shared/ui/ProtectedRoute/ProtectedRoute'

function App() {

  return (
    <>
      <Toast />
      <Header></Header>
      <Routes>
        <Route path={ROUTES.MAIN} element={<MainPage />}></Route>
        <Route element={<ProtectedRoute />}>
          <Route path={ROUTES.HOME} element={<HomePage />}></Route>
        </Route>
        <Route path={ROUTES.SIGNUP} element={<SignUpPage />}></Route>
        <Route path={ROUTES.SIGNIN} element={<SignInPage />}></Route>
      </Routes>
    </>
  )
}

export default App
