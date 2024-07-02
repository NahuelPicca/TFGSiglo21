import IndiceEntidad from "../utils/IndiceEntidad";
import { urlVehiculo } from "../utils/endpoints";
import { vehiculoDTO } from "./vehiculo.modulo";

export default function IndiceVehiculo() {
    return (
        <>
            <IndiceEntidad<vehiculoDTO>
                url={urlVehiculo} urlCrear='/vehiculos/crear'
                titulo='Listado de Vehiculos' nombreEntidad="Vehiculos"
            >
                {(vehiculo, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Nombre</th>
                        </tr>
                    </thead>
                    <tbody>
                        {vehiculo?.map(vehiculo =>
                            <tr key={vehiculo.id}>
                                <td>
                                    {botones(`/vehiculo/editar/${vehiculo.id}`, vehiculo.id)}
                                </td>
                                <td>
                                    {vehiculo.patente + ' - ' + vehiculo.marca}
                                </td>
                            </tr>)}
                    </tbody>
                </>
                }
            </IndiceEntidad >
        </>
    )
}