//import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import Menu from './utils/Menu';
import {
  Routes,
  Route,
  BrowserRouter
} from "react-router-dom";
import rutas from './route-config';
import validaciones from './validaciones/validaciones';

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
//           <img src={viteLogo} className="logo" alt="Vite logo" />
//         </a>
//         <a href="https://react.dev" target="_blank">
//           <img src={reactLogo} className="logo react" alt="React logo" />
//         </a>
//       </div>
//       <h1>Vite + React</h1>
//       <div className="card">
//         <button onClick={() => setCount((count) => count + 1)}>
//           count is {count}
//         </button>
//         <p>
//           Edit <code>src/App.tsx</code> and save to test HMR
//         </p>
//       </div>
//       <p className="read-the-docs">
//         Click on the Vite and React logos to learn more
//       </p>
//     </>
//   )
// }

// export default App
