import axios from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { urlTipoProducto } from "../utils/endpoints";
import FormularioTipoProducto from "./FormularioTipoProducto";
import { creacionTipoProductoDTO } from "./tipoProducto.modulo";

export default function CrearCategoria() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function creaTipoProducto(tipoProducto: creacionTipoProductoDTO) {
        try {
            await axios.post(`${urlTipoProducto}/crear`, tipoProducto)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Nuevo Tipo de Producto</h3>
            <MostrarErrores errores={errores} />
            <FormularioTipoProducto
                modelo={{
                    nombre: '',
                    descripcion: '',
                    categoriaId: 0
                }}
                onSubmit={async valores => await creaTipoProducto(valores)} />
        </>
    )
}