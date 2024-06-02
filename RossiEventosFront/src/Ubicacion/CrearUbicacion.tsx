import axios from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { urlUbicacion } from "../utils/endpoints";
import { creacionUbicacionDTO } from "./ubicacion.modulo";
import FormularioUbicacion from "./FormularioUbicacion";

export default function CrearUbicacion() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function creaUbicacion(ubicacion: creacionUbicacionDTO) {
        try {
            await axios.post(`${urlUbicacion}/crear`, ubicacion)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Nuevo ubicación</h3>
            <MostrarErrores errores={errores} />
            <FormularioUbicacion
                modelo={{
                    codigo: '',
                    nombre: '',
                    fila: '',
                    estante: '',
                    columna: '',
                    habilitado: true
                }}
                onSubmit={async valores => await creaUbicacion(valores)} />
        </>
    )
}