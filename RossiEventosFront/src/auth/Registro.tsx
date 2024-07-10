import axios from "axios";
import { registroUsuario, respuestaAutenticacion } from "./auth.model";
import { useContext, useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import FormularioRegistro from "./FormularioRegistro";
import { urlUsuario } from "../utils/endpoints";
import { guardarTokenLocalStorage, obtenerClaims } from "./manejadorJWT";
import AutenticacionContext from "./AutenticacionContext";

//Falta ver el temal grabado e identity con jwt.
export default function Registro() {
    const { actualizar } = useContext(AutenticacionContext);
    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate()

    async function registrar(registro: registroUsuario) {
        try {
            const respuesta = await axios
                .post<respuestaAutenticacion>(`${urlUsuario}/crear`, registro);
            guardarTokenLocalStorage(respuesta.data);
            actualizar(obtenerClaims());
            console.log(respuesta.data)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    return (
        <>
            <h3>Registro</h3>
            <MostrarErrores errores={errores} />
            <FormularioRegistro modelo={{
                nombre: '', apellido: '', cuit: '',
                nroDni: '', direccion: '', telefono: '',
                codigoPostal: '', localidad: '',
                email: '', contraseña: '', fechaNacimiento: '',
                tipo: 4
            }}
                onSubmit={valores => registrar(valores)}
            />
        </>
    )
}

