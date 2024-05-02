import { Link, NavLink } from "react-router-dom";
//import Autorizado from "../auth/Autorizado";
//import Button from "./Button";
//mport { logout } from "../auth/manejadorJWT";
//import { useContext } from "react";
//import AutenticacionContext from "../auth/AutenticacionContext";

export default function Menu() {
    //const claseActiva = 'active';
    //const { actualizar, claims } = useContext(AutenticacionContext);

    // function obtenerNombreUsuario(): string {
    //     return claims.filter(x => x.nombre === "Email")[0]?.valor;
    // }

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light" >
            <div className="container-fluid">
                <NavLink /*className="navbar-brand"*/ className={({ isActive }) => { return isActive ? 'navbar-brand' : 'navbar-brand' }} to="/">React Películas</NavLink>
                <div className="collapse navbar-collapse"
                    style={{ display: 'flex', justifyContent: 'space-between' }}>
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                to='/peliculas/filtrar'>
                                Filtrar Películas
                            </NavLink>
                        </li>
                        {/* <Autorizado role="admin" */}
                        {/* autorizado={ */}
                        <>
                            <li className="nav-item">
                                <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                    to='/generos'>
                                    {/* ClassName de NavLink lo dejamos asi por las dudas, pero ese condicional no iría o hay que cambiarlo */}
                                    Generos
                                </NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                    to='/actores'>
                                    Actores
                                </NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                    to='/cines'>
                                    Cines
                                </NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                    to='/peliculas/crear'>
                                    Crear Películas
                                </NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className={({ isActive }) => { return isActive ? 'nav-link' : 'nav-link' }}
                                    to='/usuarios'>
                                    Usuarios
                                </NavLink>
                            </li>
                        </>
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
                            <>
                                <Link to="/Registro" className="nav-link btn btn-link">
                                    Registro
                                </Link>

                                <Link to='/Login' className="nav-link btn btn-link">
                                    Login
                                </Link>
                            </>
                            {/* } */}
                            {/* /> */}
                        </>
                    </div>
                </div>
            </div>
        </nav>
    )
}

//activeClassName = {}