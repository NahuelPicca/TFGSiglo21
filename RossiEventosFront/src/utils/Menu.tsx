import { Link, NavLink, Navigate, useNavigate } from "react-router-dom";
//import Autorizado from "../auth/Autorizado";
import Button from "./Button";
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

  return (
    <div className="container">
      <nav className="d-flex flex-wrap align-items-center justify-content-center justify-content-md-between py-3">
        <div className="col-md-3 mb-2 mb-md-0">
          <a href="/" className="d-inline-flex link-body-emphasis text-decoration-none">
            <img src="https://startbootstrap.com/assets/img/sb-logo.svg" width="100" height="50"></img>
          </a>
        </div>

        <ul className="nav col-12 col-md-auto mb-2 justify-content-center mb-md-0">
          <li><a href="/" className="nav-link px-2 link-secondary">Home</a></li>
          <li><a href="/reservas" className="nav-link px-2">Reservas</a></li>
          <li><a href="/pedidos" className="nav-link px-2">Pedidos</a></li>


          <li className="nav-item dropdown">
            <a className="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
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
            <a className="nav-link dropdown-toggle" href="/gestion" role="button" data-bs-toggle="dropdown" aria-expanded="false">
              Gestión
            </a>
            <ul className="dropdown-menu">
              <li><a className="dropdown-item" href="/calidad">Calidad</a></li>
              <li><a className="dropdown-item" href="/tipoProducto">Tipo de producto</a></li>
              <li><a className="dropdown-item" href="/categoria">Categoría</a></li>
              <li><a className="dropdown-item" href="/vehiculo">Vehiculos</a></li>
              <li><a className="dropdown-item" href="/transportista">Transportista</a></li>
              <li><a className="dropdown-item" href="#">Asignar Vehiculos</a></li>
              <li><a className="dropdown-item" href="/usuario">Usuarios</a></li>
            </ul>
          </li>
        </ul>

        <div className="col-md-3 text-end">
          <a href="/Login" className="btn btn-outline-primary me-2" role="button" aria-pressed="true">Iniciar Sesión</a>
          <a href="/registro" className="btn btn-primary" role="button" aria-pressed="true">Registro</a>
        </div>
      </nav>
    </div>
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

