import { useNavigate, useParams } from "react-router-dom";
import EditarEntidad from "../utils/EditarEntidades";
import { urlProducto } from "../utils/endpoints";
import FormularioProducto from "./FormularioProducto";
import { creacionProductoDTO, productoDTO } from "./producto.modulo";
import { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { convertirProductoAFormData } from "./baseFormDataProducto";
import MostrarErrores from "../utils/MostrarErrores";
import Cargando from "../utils/Cargando";

export default function EditarProducto() {

    const [errores, setErrores] = useState<string[]>([]);
    const [productoCreacionDTO, setProductoCreacionDTO] = useState<creacionProductoDTO>();
    const [producto, setProducto] = useState<productoDTO>();
    const { id }: any = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        axios.get(`${urlProducto}/${id}`)
            .then((respuesta: AxiosResponse<productoDTO>) => {
                const modelo: creacionProductoDTO = {
                    codigo: respuesta.data.codigo,
                    descripcion: respuesta.data.descripcion,
                    marca: respuesta.data.marca,
                    posterURL1: respuesta.data.poster1,
                    posterURL2: respuesta.data.poster2,
                    posterURL3: respuesta.data.poster3,
                    anio: respuesta.data.anio,
                    habilitado: respuesta.data.habilitado,
                    precio: respuesta.data.precio,
                    calidadId: respuesta.data.calidadId,
                    tipoProductoId: respuesta.data.tipoProductoId
                }
                setProductoCreacionDTO(modelo);
                setProducto(respuesta.data);
            });
    }, [id])

    async function editar(productoEditar: creacionProductoDTO) {
        try {
            const formData = convertirProductoAFormData(productoEditar);
            await axios({
                method: 'put',
                url: `${urlProducto}/${id}`,
                data: formData,
                headers: { 'Content-Type': 'multipart/form-data' }
            });
            navigate(`/producto/${id}`)
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }

    // return (
    //     <EditarEntidad<creacionProductoDTO, productoDTO>
    //         url={urlProducto} urlIndice="/producto" nombreEntidad="Productos"
    //     >
    //         {(entidad, editar) =>
    //             <FormularioProducto modelo={entidad}
    //                 onSubmit={async valores => {
    //                     await
    //                         editar(valores);
    //                 }} />}
    //     </EditarEntidad>
    // )
    return (
        <>
            <h3>Editar Producto</h3>
            {/*El ! es para evitar que sea indefinido*/}
            <MostrarErrores errores={errores} />
            {productoCreacionDTO && producto ? <FormularioProducto
                modelo={productoCreacionDTO}
                onSubmit={async v => editar(v)}
            /> : <Cargando />
            }
        </>
    )
}

