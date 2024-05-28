//import { LandingPage } from "./LandingPage";
import CrearCalidad from "./Calidad/CrearCalidad";
import RedireccionarALanding from "./RedireccionarALanding";
// import Registro from "./auth/Registro";
 import Login from "./auth/Login";
import Registro from "./auth/Registro";
// import IndiceUsuarios from "./auth/IndiceUsuarios";

//:id(\\d+) Se usa para poder recibir en la ruta relativa un ID de tipo
//int como parámetro
//Ejemplo /actores/editar/:id
const rutas = [
     {path: '/registro', componente: Registro},
     {path: '/login', componente: Login},
     {path: '/calidad', componente: CrearCalidad, esAdmin: true},

    // {path: '/', componente: LandingPage, exact: true},
    {path: '*', componente: RedireccionarALanding}
]

export default rutas;