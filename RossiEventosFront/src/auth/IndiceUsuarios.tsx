import axios from "axios";
import Button from "../utils/Button";
import IndiceEntidad from "../utils/IndiceEntidad";
import { urlCuentas } from "../utils/endpoints";
import { usuarioDTO } from "./auth.model";
import Swal from "sweetalert2";
import confirmar from "../utils/Confirmar";

export default function IndiceUsuarios() {

    async function hacerAdmin(id: string) {
        await editarAdmin(`${urlCuentas}/HacerAdmin`, id);
    }

    async function removeAdmin(id: string) {
        await editarAdmin(`${urlCuentas}/RemoveAdmin`, id);
    }

    async function editarAdmin(url: string, id: string) {
        await axios.post(url, JSON.stringify(id),
            {
                headers: { 'Content-Type': 'application/json' }
            }
        )
        Swal.fire({
            title: 'Éxito',
            text: 'Operación realizada con éxito',
            icon: 'success'
        })
    }


    return (
        <IndiceEntidad<usuarioDTO>
            url={`${urlCuentas}/listadoUsuarios`}
            titulo="Usuarios">
            {usuarios => <>
                <thead>
                    <tr>
                        <th></th>
                        <th>Nombre</th>
                    </tr>
                </thead>
                <tbody>
                    {usuarios?.map(usuario =>
                        <tr key={usuario.id}>
                            <td>
                                <Button onClick={() => confirmar(() =>
                                    hacerAdmin(usuario.id),
                                    `¿Desea hacer a ${usuario.email} admin?`, 'Realizar')}>Hacer Admin</Button>
                                <Button className='btn btn-danger' style={{ marginLeft: '1rem' }}
                                    onClick={() => confirmar(() =>
                                        removeAdmin(usuario.id),
                                        `¿Desea remover a ${usuario.email} como admin?`, 'Realizar')}>Remover Admin</Button>
                            </td>
                            <td>
                                {usuario.email}
                            </td>
                        </tr>
                    )}
                </tbody>
            </>
            }
        </IndiceEntidad>
    )
}