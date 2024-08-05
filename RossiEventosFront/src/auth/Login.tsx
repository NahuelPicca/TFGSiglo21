import axios from "axios";
import { urlUsuario } from "../utils/endpoints";
import FormularioAuth from "./FormularoAuth";
import { credencialesUsuario } from "./auth.model";
import { useContext, useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { guardarTokenLocalStorage, obtenerClaims } from "./manejadorJWT";
import AutenticacionContext from "./AutenticacionContext";

export default function Login() {
    const { actualizar } = useContext(AutenticacionContext);
    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function login(credenciales: credencialesUsuario) {
        try {
            const resultado = await axios.post(`${urlUsuario}/login`, credenciales)
            guardarTokenLocalStorage(resultado.data);
            actualizar(obtenerClaims());
            console.log(resultado);
            navigate("/producto");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <MostrarErrores errores={errores} />
            <FormularioAuth
                modelo={{ email: '', password: '' }}
                onSubmit={async valores => await login(valores)} />
        </>
    )
}