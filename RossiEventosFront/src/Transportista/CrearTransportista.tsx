import axios from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { urlTransportista } from "../utils/endpoints";
import { creacionTransportistaDTO } from "./transportista.modulo";
import FormularioTransportista from "./FormularioTransportista";

export default function CrearTransportista() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function creaTransportista(transportista: creacionTransportistaDTO) {
        try {
            await axios.post(`${urlTransportista}/crear`, transportista)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Nuevo transportista</h3>
            <MostrarErrores errores={errores} />
            <FormularioTransportista
                modelo={{
                    nombre: '', apellido: '',
                    nroDni: '', direccion: '', cuit: '',
                    localidad: '', telefono: '',
                    codigoPostal: '', email: '',
                    fechaNacimiento: undefined,
                    fechaVencLicencia: undefined,
                    licencia: '', habilitado: true
                }}
                onSubmit={async valores => await creaTransportista(valores)} />
        </>
    )
}