import EditarEntidad from "../utils/EditarEntidades";
import { urlTipoProducto } from "../utils/endpoints";
import FormularioTipoProducto from "./FormularioTipoProducto";
import { creacionTipoProductoDTO, tipoProductoDTO } from "./tipoProducto.modulo";

export default function EditarTipoProducto() {
    return (
        <EditarEntidad<creacionTipoProductoDTO, tipoProductoDTO>
            url={urlTipoProducto} urlIndice="/tipoProducto" nombreEntidad="TipoProducto"
        >
            {(entidad, editar) =>
                <FormularioTipoProducto modelo={entidad}
                    onSubmit={async valores => {
                        await editar(valores);
                    }} />}
        </EditarEntidad>
    )
}