import axios from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { urlProducto } from "../utils/endpoints";
import FormularioProducto from "./FormularioProducto";
import { creacionProductoDTO } from "./producto.modulo";

export default function CrearProducto() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function creaProducto(producto: creacionProductoDTO) {
        try {
            await axios.post(`${urlProducto}/crear`, producto)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Nuevo producto</h3>
            <MostrarErrores errores={errores} />
            <FormularioProducto
                modelo={{
                    codigo: '',
                    descripcion: '',
                    marca: '',
                    calidadId: 0,
                    tipoId: 0,
                    precio: 0,
                    habilitado: true
                }}
                onSubmit={async valores => await creaProducto(valores)} />
        </>
    )
}