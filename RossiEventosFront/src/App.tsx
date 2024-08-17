import './App.css';
//import 'bootstrap/dist/css/bootstrap.min.css'
import Menu from './utils/Menu';
import {
  Routes,
  Route,
  BrowserRouter
} from "react-router-dom";
import rutas from './route-config';
import validaciones from './validaciones/validaciones';
import 'primeicons/primeicons.css';
import { useEffect, useState } from 'react';
import { obtenerClaims } from './auth/manejadorJWT';
import { claim } from './auth/auth.model';
import AutenticacionContext from './auth/AutenticacionContext';
//import 'primeflex/primeflex.css';

validaciones();

function App() {

  const [claims, setClaims] = useState<claim[]>([]);

  useEffect(() => {
    setClaims(obtenerClaims())
  }, [])

  function actualizar(claims: claim[]) {
    setClaims(claims)
  }

  function esAdmin() {
    return claims.findIndex(claim => claim.nombre === 'role' && claim.valor === 'admin') > -1;
  }

  return (
    <>
      <BrowserRouter>
        {/* <AutenticacionContext.Provider value={{ claims, actualizar }}> */}
        <Menu />
        <div className='container vh-100'>
          <Routes>
            {rutas.map(ruta =>
              // <Route key={ruta.path}
              //   path={ruta.path} element={
              //     ruta.esAdmin && !esAdmin() ? <>
              //       No tiene permiso para acceder a este componente
              //     </> : <ruta.componente />
              //   }>
              // </Route>
              <Route key={ruta.path}
                path={ruta.path} element={
                  <ruta.componente />
                }>
              </Route>
            )}
            {/* 
                          <Route path='/generos' element={
                              <>
                                  <IndiceGeneros />
                              </>
                          }>

                        </Route> */}
          </Routes >
        </div >
        <footer className="bg-black text-light py-4">
          <div className="container">
            <div className="row">
              <div className="col-md-5 text-center">
                <p>2024 Rossi Eventos. Todos los derechos reservados.</p>
              </div>


              <div className="col-md-3 d-flex gap-1 text-center">
                <img src="/img/rossilogoblack.png" width="25" height="25" />
                <p>Rossi eventos</p>
              </div>

              <div className="col-md-4 text-center">
                <ul className="nav col-md-4 justify-content-end list-unstyled d-flex">
                  <li className="ms-3"><a className="text-body-secondary" href="#"><i className="fa-brands fa-facebook text-white"></i></a></li>
                  <li className="ms-3"><a className="text-body-secondary" href="#"><i className="fa-brands fa-instagram text-white"></i></a></li>

                </ul>
              </div>
            </div>
          </div>
        </footer>
        {/* </AutenticacionContext.Provider> */}
      </BrowserRouter >
    </>
  );
}

export default App;
