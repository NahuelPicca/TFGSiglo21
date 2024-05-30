import { urlDeposito } from "../utils/endpoints";
import IndiceEntidad from "../utils/IndiceEntidad";
import { depositoDTO } from "./deposito.modulo";

export default function IndiceDeposito() {
    return (
        <>
            <IndiceEntidad<depositoDTO>
                url={urlDeposito} urlCrear='/deposito/crear'
                titulo='Depositos' nombreEntidad="Deposito"
            >
                {(deposito, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Nombre</th>
                        </tr>
                    </thead>
                    <tbody>
                        {deposito?.map(deposito =>
                            <tr key={deposito.id}>
                                <td>
                                    {botones(`/deposito/editar/${deposito.id}`, deposito.id)}
                                </td>
                                <td>
                                    {deposito.codigo + ' - ' + deposito.descripcion}
                                </td>
                            </tr>)}
                    </tbody>
                </>
                }
            </IndiceEntidad >
        </>
    )
}