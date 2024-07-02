import axios, { AxiosResponse } from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { urlProducto } from "../utils/endpoints";
import FormularioProducto from "./FormularioProductoSuplente";
import { creacionProductoDTO } from "./producto.modulo";
import { convertirProductoAFormData } from "./baseFormDataProducto";
//import { convertirPeliculaAFormData } from "../utils/formDataUtils";

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

    async function crear(producto: creacionProductoDTO) {
        try {
            const formData = convertirProductoAFormData(producto);
            await axios({
                method: 'post',
                url: `${urlProducto}/crear`,
                data: formData,
                headers: { 'Content-Type': 'multipart/form-data' }
            }).then((respuesta: AxiosResponse<number>) => {
                navigate(`/producto/${respuesta.data}`);
            })
        } catch (error: any) {
            setErrores(error.response.data);
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
                    tipoProductoId: 0,
                    precio: 0,
                    habilitado: true,
                    codigoCalidad: ''
                }}
                onSubmit={async valores => await crear(valores)} />
        </>
    )
}

// export function convertirProductoAFormData(producto: creacionProductoDTO): FormData {
//     const formData = new FormData();
//     formData.append('codigo', producto.codigo);
//     formData.append('descripcion', producto.descripcion);
//     formData.append('marca', producto.marca);

//     if (producto.poster1) {
//         formData.append("poster1", producto.poster1);
//     }
//     if (producto.poster2) {
//         formData.append("poster2", producto.poster2);
//     }
//     if (producto.poster3) {
//         formData.append("poster3", producto.poster3);
//     }
//     if (producto.anio) {
//         formData.append('anio', formatearFecha(producto.anio));
//     }
//     formData.append("calidadId", JSON.stringify(producto.calidadId));
//     formData.append("tipoId", JSON.stringify(producto.tipoId));
//     formData.append("precio", JSON.stringify(producto.precio));
//     return formData;
// }

// export function formatearFecha(date: Date) {
//     date = new Date(date);
//     const formato = new Intl.DateTimeFormat("en", {
//         year: 'numeric',
//         month: '2-digit',
//         day: '2-digit'
//     });

//     const [
//         { value: month }, ,
//         { value: day }, ,
//         { value: year }
//     ] = formato.formatToParts(date);

//     return `${year}-${month}-${day}`
// }