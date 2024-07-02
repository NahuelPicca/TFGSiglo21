//import './App.css';
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
        <div className='container'>
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
        <div className="container">
          <footer className="d-flex flex-wrap justify-content-between align-items-center py-5">
            <div className="col-md-4 d-flex align-items-center">
              <a href="/" className="mb-3 me-2 mb-md-0 text-body-secondary text-decoration-none lh-1">
                <img src="https://startbootstrap.com/assets/img/sb-logo.svg" width="100" height="50"></img>
              </a>
              <span className="mb-3 mb-md-0 text-body-secondary">© 2024 Rossi Eventos</span>
            </div>

            <ul className="nav col-md-4 justify-content-end list-unstyled d-flex">
              <li className="ms-3"><a className="text-body-secondary" href="#"><i className="fa-brands fa-facebook"></i></a></li>
              <li className="ms-3"><a className="text-body-secondary" href="#"><i className="fa-brands fa-instagram"></i></a></li></ul>
          </footer>
        </div>
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
//           <img src={viteLogo} classNameNameName="logo" alt="Vite logo" />
//         </a>
//         <a href="https://react.dev" target="_blank">
//           <img src={reactLogo} classNameNameName="logo react" alt="React logo" />
//         </a>
//       </div>
//       <h1>Vite + React</h1>
//       <div classNameNameName="card">
//         <button onClick={() => setCount((count) => count + 1)}>
//           count is {count}
//         </button>
//         <p>
//           Edit <code>src/App.tsx</code> and save to test HMR
//         </p>
//       </div>
//       <p classNameNameName="read-the-docs">
//         Click on the Vite and React logos to learn more
//       </p>
//     </>
//   )
// }

// export default App
