import IndiceEntidad from "../utils/IndiceEntidad";
import { urlProducto } from "../utils/endpoints";
import { productoDTO } from "./producto.modulo";

export default function IndiceProducto() {
    return (
        <>
            <IndiceEntidad<productoDTO>
                url={urlProducto} urlCrear='/producto/crear'
                titulo='Productos' nombreEntidad="Producto"
            >
                {(producto, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Nombre</th>
                        </tr>
                    </thead>
                    <tbody>
                        {producto?.map(producto =>
                            <tr key={producto.id}>
                                <td>
                                    {botones(`/producto/editar/${producto.id}`, producto.id)}
                                </td>
                                <td>
                                    {producto.codigo + ' - ' + producto.descripcion}
                                </td>
                            </tr>)}
                    </tbody>
                </>
                }
            </IndiceEntidad >
        </>
    )
}