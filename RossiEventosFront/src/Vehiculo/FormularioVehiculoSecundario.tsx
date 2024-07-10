import { Form, Formik, FormikHelpers } from "formik";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";
import FormGroupCheckbox from "../utils/FormGroupCheckbox";
import FormGroupFecha from "../utils/FormGroupFecha";
import { creacionVehiculoDTO } from "./vehiculo.modulo";
import { useEffect, useState } from "react";
import { ListBox } from 'primereact/listbox';
import { urlUsuario } from "../utils/endpoints";
import axios, { AxiosResponse } from "axios";
import { usuarioDTO } from "../auth/auth.model";

export default function FormularioVehiculo(props: formularioVehiculoProps) {
    const [usuarios, setUsuarios] = useState<usuarioDTO[]>([]);

    useEffect(() => {
        const url = `${urlUsuario}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<usuarioDTO[]>) => {
                setUsuarios(respuesta.data);
            })
    }, [])

    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                patente: Yup.string().required('La patente es requerida.').primeraLetraMayuscula(),
                marca: Yup.string().required('La marca es requerida.').primeraLetraMayuscula(),
                modelo: Yup.string().required('El modelo es requerido.'),
                fechaVencPoliza: Yup.string().required('La fecha de la poliza es requerida.'),
                nroPoliza: Yup.string().required('el número de póliza es requerida.').primeraLetraMayuscula()
            })}
        >
            {formikProps => (
                <Form>
                    <FormGroupText label="Patente"
                        campo='patente'
                        type="text" />
                    <FormGroupText label="Marca"
                        campo='marca'
                        type="text" />
                    <FormGroupText label="Modelo"
                        campo='modelo'
                        type="text" />
                    <FormGroupFecha label="Fecha Vencimiento Poliza"
                        campo='fechaVencPoliza' />
                    <FormGroupText label="Nº Poliza"
                        campo='nroPoliza'
                        type="text" />
                    {/* <div className="card flex justify-content-center"> */}
                    <div className="card flex justify-content-center">
                        {/* <Dropdown value={usuarios}
                            onChange={(e) => setUsuarios(e.value)}
                            options={usuarios} optionLabel="nombre"
                            placeholder="Seleccione el titular"
                            className="w-full md:w-14rem" */}
                        {/* filter valueTemplate={selectedCountryTemplate} */}
                        {/* itemTemplate={countryOptionTemplate} */}
                        {/* /> */}
                        <ListBox filter value={usuarios}
                            onChange={(e: any) => setUsuarios(e.value)}
                            options={usuarios}
                            optionLabel="nombre"
                            multiple={false}
                            className="w-full md:w-14rem" />
                    </div>
                    <FormGroupCheckbox label="Habilitado"
                        campo='habilitado' />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">
                        Aceptar
                    </Button>
                    <Link className="btn btn-secondary" to="/">Cancelar</Link>
                </Form>
            )}
        </Formik>
    )
}

interface formularioVehiculoProps {
    modelo: creacionVehiculoDTO;
    onSubmit(valores: creacionVehiculoDTO,
        acciones: FormikHelpers<creacionVehiculoDTO>): void;
}
