import { urlCalidad } from "../utils/endpoints";
import IndiceEntidad from "../utils/IndiceEntidad";
import { calidadDTO } from "./calidad.modulo";

export default function IndiceCalidad() {
    return (
        <>
            <IndiceEntidad<calidadDTO>
                url={urlCalidad} urlCrear='/calidad/crear'
                titulo='Calidad de los productos' nombreEntidad="Calidad"
            >
                {(calidad, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Nombre</th>
                        </tr>
                    </thead>
                    <tbody>
                        {calidad?.map(calidad =>
                            <tr key={calidad.id}>
                                <td>
                                    {botones(`/calidad/editar/${calidad.id}`, calidad.id)}
                                </td>
                                <td>
                                    {calidad.nombre}
                                </td>
                            </tr>)}
                    </tbody>
                </>
                }
            </IndiceEntidad >
        </>
    )
}