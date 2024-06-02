import EditarEntidad from "../utils/EditarEntidades";
import { urlDeposito } from "../utils/endpoints";
import FormularioDeposito from "./FormularioDeposito";
import { creacionDepositoDTO, depositoDTO } from "./deposito.modulo";

export default function EditarDeposito() {
    return (
        <EditarEntidad<creacionDepositoDTO, depositoDTO>
            url={urlDeposito} urlIndice="/deposito" nombreEntidad="Deposito"
        >
            {(entidad, editar) =>
                <FormularioDeposito modelo={entidad}
                    onSubmit={async valores => {
                        await
                            editar(valores);
                    }} />}
        </EditarEntidad>
    )
}