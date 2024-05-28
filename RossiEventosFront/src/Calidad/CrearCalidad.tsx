import axios from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import FormularioCalidad from "./FormularioCalidad";
import { calidadDTO } from "../Modelo/rossiEventos.modelo";
import { urlCalidad } from "../utils/endpoints";

export default function CrearCalidad() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function creaCalidad(calidad: calidadDTO) {
        try {
            await axios.post(`${urlCalidad}/crear`, calidad)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Calidad de productos</h3>
            <MostrarErrores errores={errores} />
            <FormularioCalidad
                modelo={{
                    id: 0,
                    codigo: '', nombre: '',
                    descripcion: ''
                }}
                onSubmit={async valores => await creaCalidad(valores)} />
        </>
    )
}