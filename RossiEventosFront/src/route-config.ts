//import { LandingPage } from "./LandingPage";
//import CrearCalidad from "./Calidad/CrearCalidad";
import CrearCalidad from "./Calidad/CrearCalidad";
import EditarCalidad from "./Calidad/EditarCalidad";
import IndiceCalidad from "./Calidad/IndiceCalidad";
import CrearDeposito from "./Deposito/CrearDeposito";
import EditarDeposito from "./Deposito/EditarDeposito";
import IndiceDeposito from "./Deposito/IndiceDeposito";
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

    {path: '/calidad', componente: IndiceCalidad, esAdmin: true},
    {path: '/calidad/crear', componente: CrearCalidad, esAdmin: true},
    {path: '/calidad/editar/:id', componente: EditarCalidad, esAdmin: true},

    {path: '/deposito', componente: IndiceDeposito, esAdmin: true},
    {path: '/deposito/crear', componente: CrearDeposito, esAdmin: true},
    {path: '/deposito/editar/:id', componente: EditarDeposito, esAdmin: true},
    // {path: '/', componente: LandingPage, exact: true},
    {path: '*', componente: RedireccionarALanding}
]

export default rutas;