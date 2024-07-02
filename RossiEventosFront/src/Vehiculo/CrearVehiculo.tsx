import axios from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { urlVehiculo } from "../utils/endpoints";
import { creacionVehiculoDTO } from "./vehiculo.modulo";
import FormularioVehiculo from "./FormularioVehiculo";

export default function CrearVehiculo() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function creaVehiculo(vehiculo: creacionVehiculoDTO) {
        try {
            await axios.post(`${urlVehiculo}/crear`, vehiculo)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Nuevo vehículo</h3>
            <MostrarErrores errores={errores} />
            <FormularioVehiculo
                modelo={{
                    patente: '', marca: '',
                    modelo: '', fechaVencPoliza: undefined,
                    nroPoliza: '', habilitado: true,
                    titularId: 0
                }}
                onSubmit={async valores => await creaVehiculo(valores)} />
        </>
    )
}