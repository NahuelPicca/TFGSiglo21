import axios from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { urlCategoria } from "../utils/endpoints";
import { creacionCategoriaDTO } from "./categoria.modulo";
import FormularioCategoria from "./FormularioCategoriaSecundario";

export default function CrearCategoria() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function creaCategoria(categoria: creacionCategoriaDTO) {
        try {
            await axios.post(`${urlCategoria}/crear`, categoria)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Nueva categoría</h3>
            <MostrarErrores errores={errores} />
            <FormularioCategoria
                modelo={{
                    nombre: '',
                    descripcion: '',
                }}
                onSubmit={async valores => await creaCategoria(valores)} />
        </>
    )
}