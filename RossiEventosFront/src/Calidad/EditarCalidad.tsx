import EditarEntidad from "../utils/EditarEntidades";
import { urlCalidad } from "../utils/endpoints";
import FormularioCalidad from "./FormularioCalidadSecundario";
import { calidadCreacionDTO, calidadDTO } from "./calidad.modulo";

export default function EditarCalidad() {
    return (
        <EditarEntidad<calidadCreacionDTO, calidadDTO>
            url={urlCalidad} urlIndice="/calidad" nombreEntidad="Calidad"
        >
            {(entidad, editar) =>
                <FormularioCalidad modelo={entidad}
                    onSubmit={async valores => {
                        await
                            editar(valores);
                    }} />}
        </EditarEntidad>
    )
}