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
        <nav className="navbar navbar-expand-lg navbar-light bg-light" >
            <div className="container-fluid">
                <NavLink className='navbar-brand' to="/">Rossi Eventos</NavLink>
                <div className="collapse navbar-collapse"
                    style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <ul className="nav justify-content-end">
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/reservas'>
                                Reservas
                            </NavLink>
                        </li>
                        {/* <Autorizado role="admin" */}
                        {/* autorizado={ */}
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/pedidos'>
                                {/* ClassName de NavLink lo dejamos asi por las dudas, pero ese condicional no iría o hay que cambiarlo */}
                                Pedidos
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/stock'>
                                Stock
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/productos'>
                                Productos
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/gestion'>
                                Gestión
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/contacto'>
                                Contacto
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/calidad'>
                                Calidad
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/deposito'>
                                Depositos
                            </NavLink>
                        </li>
                        {/* } /> */}
                    </ul>
                    <div className="d-flex">
                        {/* <Autorizado
                 autorizado={ */}
                        <>
                            {/* <span className="nav-link">Hola, {obtenerNombreUsuario()}</span>
                 <Button className='nav-link btn btn-link'
                     onClick={() => {
                         logout();
                         actualizar([]);
                     }}>Log out</Button> */}
                            {/* </>} */}
                            {/* noAutorizado={ */}
                            <div className="d-flex">
                                <>
                                    <Button className="nav-link btn btn-link"
                                        onClick={() => {
                                            obtenerNombreUsuario(false)
                                        }}>
                                        Registro
                                    </Button>

                                    <Button className="nav-link btn btn-link"
                                        onClick={() => {
                                            obtenerNombreUsuario(true)
                                        }}>
                                        Inicio de Sessión
                                    </Button>
                                </>
                            </div>
                            {/* } */}
                            {/* /> */}
                        </>
                    </div>
                </div>
            </div>
        </nav>
    )
    // return (
    //     <>
    //         <ul className="nav justify-content-end">
    //             <li className="nav-item">
    //                 <a className="nav-link active" href="#">Active</a>
    //             </li>
    //             <li className="nav-item">
    //                 <a className="nav-link" href="#">Link</a>
    //             </li>
    //             <li className="nav-item">
    //                 <a className="nav-link" href="#">Link</a>
    //             </li>
    //             <li className="nav-item">
    //                 <a className="nav-link disabled" href="#">Disabled</a>
    //             </li>
    //         </ul>
    //     </>

    // )
}

//activeClassName = {}