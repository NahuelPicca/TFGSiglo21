import axios from "axios";
import { urlUsuarios } from "../utils/endpoints";
import FormularioAuth from "./FormularoAuth";
import { credencialesUsuario } from "./auth.model";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
//import { guardarTokenLocalStorage, obtenerClaims } from "./manejadorJWT";
//import AutenticacionContext from "./AutenticacionContext";
import { useNavigate } from "react-router-dom";

export default function Login() {
    //const { actualizar } = useContext(AutenticacionContext);
    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function login(credenciales: credencialesUsuario) {
        try {
            const resultado = await axios.post(`${urlUsuarios}/login`, credenciales)
            //guardarTokenLocalStorage(resultado.data);
            //actualizar(obtenerClaims());
            console.log(resultado);
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Login</h3>
            <MostrarErrores errores={errores} />
            <FormularioAuth
                modelo={{ email: '', password: '' }}
                onSubmit={async valores => await login(valores)} />
        </>
    )
}