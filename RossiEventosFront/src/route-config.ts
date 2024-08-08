//import { LandingPage } from "./LandingPage";
//import CrearCalidad from "./Calidad/CrearCalidad";
import FormularioAsigVehiculoTransportista from "./AsigVehiculoTransportista/FormularioAsigVehiculoTransportista";
import FormularioCalidad from "./Calidad/FormularioCalidad";
import FormularioCategoria from "./Categoria/FormularioCategoria";
import FormularioDeposito from "./Deposito/FormularioDeposito";
import { LandingPage } from "./LandingPage/LandingPage";
//import CrearMovimiento from "./Movimientos/CrearMovimiento";
import FormularioProducto from "./Productos/FormularioProducto";
import RedireccionarALanding from "./RedireccionarALanding";
import FormularioTipoProducto from "./TipoProducto/FormularioTipoProducto";
import FormularioTransportistaPrincipal from "./Transportista/FormularioTransportista";
import FormularioUbicacion from "./Ubicacion/FormularioUbicacion";
import FormularioVehiculo from "./Vehiculo/FormularioVehiculoPrincipal";
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

    {path: '/calidad', componente: FormularioCalidad, esAdmin: true},
    // {path: '/calidad/crear', componente: CrearCalidad, esAdmin: true},
    // {path: '/calidad/editar/:id', componente: EditarCalidad, esAdmin: true},

    {path: '/deposito', componente: FormularioDeposito, esAdmin: true},
    // {path: '/deposito', componente: IndiceDeposito, esAdmin: true},
    // {path: '/deposito/crear', componente: CrearDeposito, esAdmin: true},
    // {path: '/deposito/editar/:id', componente: EditarDeposito, esAdmin: true},

     {path: '/categoria', componente: FormularioCategoria, esAdmin: true},
    // {path: '/categoria', componente: IndiceCategoria, esAdmin: true},
    // {path: '/categoria/crear', componente: CrearCategoria, esAdmin: true},
    // {path: '/categoria/editar/:id', componente: EditarCategoria, esAdmin: true},

     {path: '/ubicacion', componente: FormularioUbicacion, esAdmin: true},
    // {path: '/ubicacion', componente: IndiceUbicacion, esAdmin: true},
    // {path: '/ubicacion/crear', componente: CrearUbicacion, esAdmin: true},
    // {path: '/ubicacion/editar/:id', componente: EditarUbicacion, esAdmin: true},

    //{path: '/movimiento', componente: IndiceUbicacion, esAdmin: true},
    //{path: '/movimiento/crear', componente: CrearMovimiento, esAdmin: true},

    {path: '/producto', componente: FormularioProducto, esAdmin: true},
    // {path: '/producto', componente: IndiceProducto, esAdmin: true},
    // {path: '/producto/crear', componente: CrearProducto, esAdmin: true},
    // {path: '/producto/editar/:id', componente: EditarProducto, esAdmin: true},

     {path: '/tipoProducto', componente: FormularioTipoProducto, esAdmin: true},
    // {path: '/tipoProducto', componente: IndiceTipoProducto, esAdmin: true},
    // {path: '/tipoProducto/crear', componente: CrearTipoProducto, esAdmin: true},
    // {path: '/tipoProducto/editar/:id', componente: EditarTipoProducto, esAdmin: true},

    {path: '/transportista', componente: FormularioTransportistaPrincipal, esAdmin: true},
    // {path: '/transportista', componente: IndiceTransportista, esAdmin: true},
    // {path: '/transportista/crear', componente: CrearTransportista, esAdmin: true},
    // {path: '/transportista/editar/:id', componente: EditarTransportista, esAdmin: true},

      {path: '/vehiculo', componente: FormularioVehiculo, esAdmin: true},

      {path: '/asignacionVehicTransp', componente: FormularioAsigVehiculoTransportista, esAdmin: true},
    // {path: '/vehiculo', componente: IndiceVehiculo, esAdmin: true},
    // {path: '/vehiculo/crear', componente: CrearVehiculo, esAdmin: true},
    // {path: '/vehiculo/editar/:id', componente: EditarVehiculo, esAdmin: true},

    {path: '/', componente: LandingPage, exact: true},
    {path: '*', componente: RedireccionarALanding}
]

export default rutas;