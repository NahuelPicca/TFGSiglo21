import EditarEntidad from "../utils/EditarEntidades";
import { urlProducto } from "../utils/endpoints";
import FormularioProducto from "./FormularioProducto";
import { creacionProductoDTO, productoDTO } from "./producto.modulo";

export default function EditarProducto() {
    return (
        <EditarEntidad<creacionProductoDTO, productoDTO>
            url={urlProducto} urlIndice="/producto" nombreEntidad="Productos"
        >
            {(entidad, editar) =>
                <FormularioProducto modelo={entidad}
                    onSubmit={async valores => {
                        await
                            editar(valores);
                    }} />}
        </EditarEntidad>
    )
}