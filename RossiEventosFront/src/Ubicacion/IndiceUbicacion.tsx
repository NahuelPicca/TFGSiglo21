import { urlUbicacion } from "../utils/endpoints";
import IndiceEntidad from "../utils/IndiceEntidad";
import { ubicacionDTO } from "./ubicacion.modulo";

export default function IndiceUbicacion() {
    return (
        <>
            <IndiceEntidad<ubicacionDTO>
                url={urlUbicacion} urlCrear='/ubicacion/crear'
                titulo='Ubicaciones disponibles' nombreEntidad="Ubicacion"
            >
                {(ubicacion, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Nombre</th>
                        </tr>
                    </thead>
                    <tbody>
                        {ubicacion?.map(ubicacion =>
                            <tr key={ubicacion.id}>
                                <td>
                                    {botones(`/ubicacion/editar/${ubicacion.id}`, ubicacion.id)}
                                </td>
                                <td>
                                    {ubicacion.codigo + ' - ' + ubicacion.nombre}
                                </td>
                            </tr>)}
                    </tbody>
                </>
                }
            </IndiceEntidad >
        </>
    )
}