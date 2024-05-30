import axios from "axios";
import { useState } from "react";
import MostrarErrores from "../utils/MostrarErrores";
import { useNavigate } from "react-router-dom";
import { creacionDepositoDTO } from "./deposito.modulo";
import { urlDeposito } from "../utils/endpoints";
import FormularioDeposito from "./FormularioDeposito";

export default function CrearDeposito() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();
    async function creaDeposito(deposito: creacionDepositoDTO) {
        try {
            await axios.post(`${urlDeposito}/crear`, deposito)
            navigate("/");
        } catch (error: any) {
            setErrores(error.response.data)
        }
    }

    return (
        <>
            <h3>Nuevo depósito</h3>
            <MostrarErrores errores={errores} />
            <FormularioDeposito
                modelo={{
                    codigo: '',
                    descripcion: '',
                    direccion: '',
                    localidad: '',
                    provincia: '',
                    habilitado: true
                }}
                onSubmit={async valores => await creaDeposito(valores)} />
        </>
    )
}