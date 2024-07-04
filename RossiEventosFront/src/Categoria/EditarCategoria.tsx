import EditarEntidad from "../utils/EditarEntidades";
import { urlCategoria } from "../utils/endpoints";
import FormularioCategoria from "./FormularioCategoriaSecundario";
import { categoriaDTO, creacionCategoriaDTO } from "./categoria.modulo";

export default function EditarCategoria() {
    return (
        <EditarEntidad<creacionCategoriaDTO, categoriaDTO>
            url={urlCategoria} urlIndice="/categoria" nombreEntidad="Categoria"
        >
            {(entidad, editar) =>
                <FormularioCategoria modelo={entidad}
                    onSubmit={async valores => {
                        await
                            editar(valores);
                    }} />}
        </EditarEntidad>
    )
}