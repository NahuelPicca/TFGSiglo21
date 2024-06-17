//import { LandingPage } from "./LandingPage";
//import CrearCalidad from "./Calidad/CrearCalidad";
import CrearCalidad from "./Calidad/CrearCalidad";
import EditarCalidad from "./Calidad/EditarCalidad";
import IndiceCalidad from "./Calidad/IndiceCalidad";
import CrearCategoria from "./Categoria/CrearCategoria";
import EditarCategoria from "./Categoria/EditarCategoria";
import IndiceCategoria from "./Categoria/IndiceCategoria";
import CrearDeposito from "./Deposito/CrearDeposito";
import EditarDeposito from "./Deposito/EditarDeposito";
import IndiceDeposito from "./Deposito/IndiceDeposito";
import { LandingPage } from "./LandingPage/LandingPage";
//import CrearMovimiento from "./Movimientos/CrearMovimiento";
import CrearProducto from "./Productos/CrearProducto";
import EditarProducto from "./Productos/EditarProducto";
import IndiceProducto from "./Productos/IndiceProducto";
import RedireccionarALanding from "./RedireccionarALanding";
import CrearTipoProducto from "./TipoProducto/CrearTipoProducto";
import EditarTipoProducto from "./TipoProducto/EditarTipoProducto";
import IndiceTipoProducto from "./TipoProducto/IndiceTipoProducto";
import CrearTransportista from "./Transportista/CrearTransportista";
import EditarTransportista from "./Transportista/EditarTransportista";
import IndiceTransportista from "./Transportista/IndiceTransportista";
import CrearUbicacion from "./Ubicacion/CrearUbicacion";
import EditarUbicacion from "./Ubicacion/EditarUbicacion";
import IndiceUbicacion from "./Ubicacion/IndiceUbicacion";
// import Registro from "./auth/Registro";
 import Login from "./auth/Login";
import Registro from "./auth/Registro";
//import Menu from "./utils/Menu";
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

    {path: '/categoria', componente: IndiceCategoria, esAdmin: true},
    {path: '/categoria/crear', componente: CrearCategoria, esAdmin: true},
    {path: '/categoria/editar/:id', componente: EditarCategoria, esAdmin: true},

    {path: '/ubicacion', componente: IndiceUbicacion, esAdmin: true},
    {path: '/ubicacion/crear', componente: CrearUbicacion, esAdmin: true},
    {path: '/ubicacion/editar/:id', componente: EditarUbicacion, esAdmin: true},

    //{path: '/movimiento', componente: IndiceUbicacion, esAdmin: true},
    //{path: '/movimiento/crear', componente: CrearMovimiento, esAdmin: true},

    {path: '/producto', componente: IndiceProducto, esAdmin: true},
    {path: '/producto/crear', componente: CrearProducto, esAdmin: true},
    {path: '/producto/editar/:id', componente: EditarProducto, esAdmin: true},

    {path: '/tipoProducto', componente: IndiceTipoProducto, esAdmin: true},
    {path: '/tipoProducto/crear', componente: CrearTipoProducto, esAdmin: true},
    {path: '/tipoProducto/editar/:id', componente: EditarTipoProducto, esAdmin: true},

    {path: '/transportista', componente: IndiceTransportista, esAdmin: true},
    {path: '/transportista/crear', componente: CrearTransportista, esAdmin: true},
    {path: '/transportista/editar/:id', componente: EditarTransportista, esAdmin: true},

    {path: '/', componente: LandingPage, exact: true},
    {path: '*', componente: RedireccionarALanding}
]

export default rutas;