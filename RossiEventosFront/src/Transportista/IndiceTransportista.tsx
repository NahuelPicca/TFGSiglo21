import IndiceEntidad from "../utils/IndiceEntidad";
import { urlTransportista } from "../utils/endpoints";
import { transportistaDTO } from "./transportista.modulo";

export default function IndiceTransportista() {
    return (
        <>
            <IndiceEntidad<transportistaDTO>
                url={urlTransportista} urlCrear='/transportista/crear'
                titulo='Listado de Transportistas' nombreEntidad="Transportista"
            >
                {(transportista, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Nombre</th>
                        </tr>
                    </thead>
                    <tbody>
                        {transportista?.map(transportista =>
                            <tr key={transportista.id}>
                                <td>
                                    {botones(`/transportista/editar/${transportista.id}`, transportista.id)}
                                </td>
                                <td>
                                    {transportista.nombre + ' - ' + transportista.apellido}
                                </td>
                            </tr>)}
                    </tbody>
                </>
                }
            </IndiceEntidad >
        </>
    )
}