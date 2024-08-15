import { Link, NavLink, Navigate, useLocation, useNavigate } from "react-router-dom";
//import Autorizado from "../auth/Autorizado";
import { useEffect, useState } from "react";
import Autorizado from "../auth/Autorizado";
import { logout } from "../auth/manejadorLogin";

//mport { logout } from "../auth/manejadorJWT";
//import { useContext } from "react";
//import AutenticacionContext from "../auth/AutenticacionContext";

export default function Menu() {
  const navegar = useNavigate();
  //const claseActiva = 'active';
  //const { actualizar, claims } = useContext(AutenticacionContext);

  // function obtenerNombreUsuario(): string {
  //     return claims.filter(x => x.nombre === "Email")[0]?.valor;
  // }

  function obtenerNombreUsuario(logueado: boolean): void {
    logueado ? navegar('/Login') : navegar('/Registro')
  }

  const [showMenu, setShowMenu] = useState(false);
  const [hidePedidosLink, setHidePedidosLink] = useState(false);
  const [hideRegIniSesion, setHideRegIniSesion] = useState(false);
  const [sesionActiva, setSesionActiva] = useState(false);

  useEffect(() => {
    // Verificar la URL actual
    const currentUrl = window.location.href;
    if (currentUrl.toUpperCase().includes('/REGISTRO') ||
      currentUrl.toUpperCase().includes('/LOGIN') ||
      currentUrl.toLowerCase() === 'http://localhost:5173/') {
      setShowMenu(true);
      setHidePedidosLink(true);
      setSesionActiva(true);
      setHideRegIniSesion(false);
    } else {
      setShowMenu(false);
      setHidePedidosLink(false);
      setSesionActiva(false);
      setHideRegIniSesion(true);
    }
  });

  return (
    <>
      <div className="container-full border-bottom">
        <div className="container">
          <nav className="d-flex flex-wrap align-items-center justify-content-center justify-content-md-between py-3 border-1">
            <div className="col-md-3 mb-2 mb-md-0">
              <a href="/" className="d-inline-flex link-body-emphasis text-decoration-none">
                <img src="/img/rossilogowhite.png" width="50" height="50" />
                <h4 className="h4-RossiEvento">Rossi eventos</h4>
              </a>
            </div>

            <ul className="nav col-12 col-md-auto mb-2 justify-content-center mb-md-0 align-items-center ">
              <li><a href="/" className="nav-link px-2 link-secondary text-black">Inicio</a></li>
              {showMenu && (
                // <Autorizado
                //   autorizado={<></>}
                //   noAutorizado={
                <>
                  <li><strong><a href="/reservas" className="nav-link px-2 text-black">Reservas</a></strong></li>
                  <li><a href="/contacto" className="nav-link px-2 text-black">Contacto</a></li>
                  <li><i className="fa-solid fa-cart-shopping"></i></li>
                </>
                //} />
              )}
              {!hidePedidosLink && (
                <>
                  {/* <Autorizado role="admin"
                    autorizado={ */}
                  <>
                    <li><a className="nav-link px-2 text-black" href="">Pedidos</a></li>
                    <li className="nav-item dropdown">
                      <a className="nav-link px-2 text-black" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Stock
                      </a>
                      <ul className="dropdown-menu">
                        <li><a className="dropdown-item" href="/deposito">Depositos</a></li>
                        <li><a className="dropdown-item" href="/ubicacion">Ubicación</a></li>
                        <li><a className="dropdown-item" href="#">Movimiento de Stock</a></li>
                        <li><a className="dropdown-item" href="/producto">Productos</a></li>
                        <li><a className="dropdown-item" href="#">Saldos Ubicación</a></li>
                      </ul>
                    </li>
                    <li className="nav-item dropdown">
                      <a className="nav-link px-2 text-black" href="/gestion" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Gestión
                      </a>
                      <ul className="dropdown-menu">
                        <li><a className="dropdown-item" href="/calidad">Calidad</a></li>
                        <li><a className="dropdown-item" href="/tipoProducto">Tipo de producto</a></li>
                        <li><a className="dropdown-item" href="/categoria">Categoría</a></li>
                        <li><a className="dropdown-item" href="/vehiculo">Vehiculos</a></li>
                        <li><a className="dropdown-item" href="/transportista">Transportista</a></li>
                        <li><a className="dropdown-item" href="/asignacionVehicTransp">Asig. vehículo</a></li>
                        <li><a className="dropdown-item" href="">Usuarios</a></li>
                        <li><a className="dropdown-item" href="">Comprobantes</a></li>
                      </ul>
                    </li>
                  </>
                  {/* } /> */}
                </>
              )}
            </ul>
            {!hideRegIniSesion &&
              <div className="col-md-3 text-end">
                <a href="/Login" className="boton-violeta-ini-sesion" role="button" aria-pressed="true">Iniciar Sesión</a>
                <a href="/registro" className="boton-violeta" role="button" aria-pressed="true">Registrarse</a>
              </div>
            }
            {!sesionActiva &&
              <div className="col-md-3 text-end">
                <div className="d-flex align-items-center">
                  <i className="fa-solid fa-user custom-icon me-1 "></i>
                  <div className="dropdown">
                    <button className="btn btn-dropdown dropdown" type="button"
                      id="dropdownMenuButton" data-bs-toggle="dropdown" aria-expanded="false">
                      Picca, Marcelo José
                    </button>
                    <p className="dropdown-subtitle">(Administrador)</p>
                    <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                      <li><a className="dropdown-item" href="/.">Mis datos</a></li>
                      <li><a className="dropdown-item" href="/.">Pedidos</a></li>
                      <li><a className="dropdown-item" href="/." onClick={logout}>Cerrar sesión</a></li>
                    </ul>
                  </div>
                </div>
              </div>
            }
          </nav>
        </div>
      </div>
    </>
  )
  // return (
  //     <>
  //         <ul classNameNameNameNameName="nav justify-content-end">
  //             <li classNameNameNameNameName="nav-item">
  //                 <a classNameNameNameNameName="nav-link active" href="#">Active</a>
  //             </li>
  //             <li classNameNameNameNameName="nav-item">
  //                 <a classNameNameNameNameName="nav-link" href="#">Link</a>
  //             </li>
  //             <li classNameNameNameNameName="nav-item">
  //                 <a classNameNameNameNameName="nav-link" href="#">Link</a>
  //             </li>
  //             <li classNameNameNameNameName="nav-item">
  //                 <a classNameNameNameNameName="nav-link disabled" href="#">Disabled</a>
  //             </li>
  //         </ul>
  //     </>

  // )
}
//activeclassNameNameNameNameName = {}

