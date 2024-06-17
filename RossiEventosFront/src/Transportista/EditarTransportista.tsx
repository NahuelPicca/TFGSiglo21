import EditarEntidad from "../utils/EditarEntidades";
import { urlTransportista } from "../utils/endpoints";
import FormularioTransportista from "./FormularioTransportista";
import { creacionTransportistaDTO, transportistaDTO } from "./transportista.modulo";

export default function EditarTransportista() {
    return (
        <EditarEntidad<creacionTransportistaDTO, transportistaDTO>
            url={urlTransportista} urlIndice="/transportista" nombreEntidad="Transportista"
        >
            {(entidad, editar) =>
                <FormularioTransportista modelo={entidad}
                    onSubmit={async valores => {
                        await
                            editar(valores);
                    }} />}
        </EditarEntidad>
    )
}