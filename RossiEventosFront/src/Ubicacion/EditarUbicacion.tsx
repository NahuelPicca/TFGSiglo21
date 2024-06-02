import EditarEntidad from "../utils/EditarEntidades";
import { urlUbicacion } from "../utils/endpoints";
import FormularioUbicacion from "./FormularioUbicacion";
import { creacionUbicacionDTO, ubicacionDTO } from "./ubicacion.modulo";

export default function EditarUbicacion() {
    return (
        <EditarEntidad<creacionUbicacionDTO, ubicacionDTO>
            url={urlUbicacion} urlIndice="/ubicacion" nombreEntidad="Editar Ubicación"
        >
            {(entidad, editar) =>
                <FormularioUbicacion modelo={entidad}
                    onSubmit={async valores => {
                        await
                            editar(valores);
                    }} />}
        </EditarEntidad>
    )
}