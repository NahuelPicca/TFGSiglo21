import { urlCategoria } from "../utils/endpoints";
import IndiceEntidad from "../utils/IndiceEntidad";
import { categoriaDTO } from "./categoria.modulo";

export default function IndiceCategoria() {
    return (
        <>
            <IndiceEntidad<categoriaDTO>
                url={urlCategoria} urlCrear='/categoria/crear'
                titulo='Categorias' nombreEntidad="Categoria"
            >
                {(categoria, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Nombre</th>
                        </tr>
                    </thead>
                    <tbody>
                        {categoria?.map(categoria =>
                            <tr key={categoria.id}>
                                <td>
                                    {botones(`/categoria/editar/${categoria.id}`, categoria.id)}
                                </td>
                                <td>
                                    {categoria.nombre + ' - ' + categoria.descripcion}
                                </td>
                            </tr>)}
                    </tbody>
                </>
                }
            </IndiceEntidad >
        </>
    )
}