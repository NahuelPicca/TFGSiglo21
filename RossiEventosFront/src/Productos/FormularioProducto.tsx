import { Form, Formik, FormikHelpers } from "formik";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";
import FormGroupCheckbox from "../utils/FormGroupCheckbox";
import { tipoProductoDTO } from "../TipoProducto/tipoProducto.modulo";
import { calidadDTO } from "../Calidad/calidad.modulo";
import { useEffect, useState } from "react";
import { urlCalidad, urlTipoProducto } from "../utils/endpoints";
import axios, { AxiosResponse } from "axios";
import { creacionProductoDTO } from "./producto.modulo";
import FormGroupFecha from "../utils/FormGroupFecha";

export default function FormularioProducto(props: formularioProductoProps) {

    const [calidades, setCalidades] = useState<calidadDTO[]>([]);
    const [tipoProducto, setTipoProducto] = useState<tipoProductoDTO[]>([]);

    useEffect(() => {
        const url = `${urlCalidad}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<calidadDTO[]>) => {
                setCalidades(respuesta.data);
            })
    }, [])

    useEffect(() => {
        const url = `${urlTipoProducto}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<tipoProductoDTO[]>) => {
                setTipoProducto(respuesta.data);
            })
    }, [])

    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                codigo: Yup.string()
                    .required('El código es requerido.')
                    .max(20, 'El código tiene que tener 20 carácteres.'),
                descripcion: Yup.string()
                    .required('La descripción es requerida.')
                    .primeraLetraMayuscula(),
                marca: Yup.string()
                    .required('La marca es requerida.')
                    .primeraLetraMayuscula(),
                anio: Yup.date()
                    .required('El año es requerido.'),
                habilitado: Yup.bool(),
                precio: Yup.number()
                    .required('El precio es requerido.')
                    .min(0, 'El precio debe ser mayor a cero.'),
            })}
        >
            {formikProps => (
                <Form>
                    <FormGroupText label="Codigo"
                        campo='codigo'
                        type="text" />
                    <FormGroupText label="Descripcion"
                        campo='descripcion'
                        type="text" />
                    <FormGroupText label="Marca"
                        campo='marca'
                        type="text" />
                    <FormGroupFecha label="Año"
                        campo='anio' />
                    <FormGroupText label="Precio"
                        campo='precio'
                        type="number" />
                    <FormGroupCheckbox label="Habilitado"
                        campo='habilitado' />
                    <div className="form-group mb-2">
                        Calidad
                        <select className="form-control"
                            {...formikProps.getFieldProps('calidadId')}>
                            {calidades.map(calidad => <option key={calidad.id}
                                value={calidad.id}>{calidad.nombre}</option>)}
                        </select>
                    </div>
                    <div className="form-group mb-2">
                        Tipo de producto
                        <select className="form-control"
                            {...formikProps.getFieldProps('tipoProductoId')}>
                            {tipoProducto.map(tipoProducto => <option key={tipoProducto.id}
                                value={tipoProducto.id}>{tipoProducto.nombre}</option>)}
                        </select>
                    </div>
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

interface formularioProductoProps {
    modelo: creacionProductoDTO;
    onSubmit(valores: creacionProductoDTO,
        acciones: FormikHelpers<creacionProductoDTO>): void;
}