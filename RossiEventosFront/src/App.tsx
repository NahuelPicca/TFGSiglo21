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
//import 'primeflex/primeflex.css';

validaciones();

function App() {
  return (
    <>
      <BrowserRouter>
        <Menu />
        <div className='container  vh-100'>
          <Routes>
            {rutas.map(ruta =>
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
              <div className="col-md-4 text-center">
                <p>2024 Rossi Eventos. Todos los derechos reservados.</p>
              </div>


              <div className="col-md-4 d-flex gap-1 text-center">
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
      </BrowserRouter >
    </>
  );
}

export default App;
// import { useState } from 'react'
// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
// import './App.css'

// function App() {
//   const [count, setCount] = useState(0)

//   return (
//     <>
//       <div>
//         <a href="https://vitejs.dev" target="_blank">
//           <img src={viteLogo} classNameNameNameName="logo" alt="Vite logo" />
//         </a>
//         <a href="https://react.dev" target="_blank">
//           <img src={reactLogo} classNameNameNameName="logo react" alt="React logo" />
//         </a>
//       </div>
//       <h1>Vite + React</h1>
//       <div classNameNameNameName="card">
//         <button onClick={() => setCount((count) => count + 1)}>
//           count is {count}
//         </button>
//         <p>
//           Edit <code>src/App.tsx</code> and save to test HMR
//         </p>
//       </div>
//       <p classNameNameNameName="read-the-docs">
//         Click on the Vite and React logos to learn more
//       </p>
//     </>
//   )
// }

// export default App
