import EditarEntidad from "../utils/EditarEntidades";
import { urlTransportista, urlVehiculo } from "../utils/endpoints";
import FormularioVehiculo from "./FormularioVehiculo";
import { creacionVehiculoDTO, vehiculoDTO } from "./vehiculo.modulo";

export default function EditarVehiculo() {
    return (
        <EditarEntidad<creacionVehiculoDTO, vehiculoDTO>
            url={urlVehiculo} urlIndice="/vehiculo" nombreEntidad="Vehiculo"
        >
            {(entidad, editar) =>
                <FormularioVehiculo modelo={entidad}
                    onSubmit={async valores => {
                        await
                            editar(valores);
                    }} />}
        </EditarEntidad>
    )
}