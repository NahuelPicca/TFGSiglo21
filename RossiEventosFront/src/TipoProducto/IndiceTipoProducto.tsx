import IndiceEntidad from "../utils/IndiceEntidad";
import { urlTipoProducto } from "../utils/endpoints";
import { tipoProductoDTO } from "./tipoProducto.modulo";

export default function IndiceTipoProducto() {
    return (
        <>
            <IndiceEntidad<tipoProductoDTO>
                url={urlTipoProducto} urlCrear='/tipoProducto/crear'
                titulo='Tipo Producto' nombreEntidad="TipoProducto"
            >
                {(tipoProducto, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Nombre</th>
                        </tr>
                    </thead>
                    <tbody>
                        {tipoProducto?.map(tipoProducto =>
                            <tr key={tipoProducto.id}>
                                <td>
                                    {botones(`/tipoProducto/editar/${tipoProducto.id}`, tipoProducto.id)}
                                </td>
                                <td>
                                    {tipoProducto.nombre + ' - ' + tipoProducto.descripcion}
                                </td>
                            </tr>)}
                    </tbody>
                </>
                }
            </IndiceEntidad >
        </>
    )
}