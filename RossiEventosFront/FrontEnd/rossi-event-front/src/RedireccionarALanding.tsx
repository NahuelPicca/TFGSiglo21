//import { Redirect } from "react-router";
import { Navigate } from "react-router-dom";

export default function RedireccionarALanding() {
    //Retornamos a la LandingPage cuando no existe la dirección que 
    //ingresa el usuario
    return <Navigate to="/" />
}