import { Form, Formik, FormikHelpers } from "formik";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";
import { creacionTipoProductoDTO } from "./tipoProducto.modulo";
import { useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { urlCategoria } from "../utils/endpoints";
import { categoriaDTO } from "../Categoria/categoria.modulo";

export default function FormularioTipoProducto(props: formularioTipoProductoProps) {

    const [categoria, setCategoria] = useState<categoriaDTO[]>([]);

    useEffect(() => {
        const url = `${urlCategoria}/todos`
        axios.get(url)
            .then((respuesta: AxiosResponse<categoriaDTO[]>) => {
                setCategoria(respuesta.data);
            })
    }, [])

    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                nombre: Yup.string()
                    .required('El nombre es requerido.')
                    .primeraLetraMayuscula()
                    .max(100, 'No puede superar los 100 carácteres en el nombre.'),
                descripcion: Yup.string()
                    .required('La descripción es requerida.')
                    .primeraLetraMayuscula()
                    .max(1000, 'No puede superar los 1000 carácteres en la descripción.'),
            })}
        >
            {formikProps => (
                <Form>
                    <FormGroupText label="Nombre"
                        campo='nombre'
                        type="text" />
                    <FormGroupText label="Descripcion"
                        campo='descripcion'
                        type="text" />
                    <div className="form-group mb-2">
                        Categoría
                        <select className="form-control"
                            {...formikProps.getFieldProps('categoriaId')}>
                            {categoria.map(categoria => <option key={categoria.id}
                                value={categoria.id}>{categoria.nombre}</option>)}
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

interface formularioTipoProductoProps {
    modelo: creacionTipoProductoDTO;
    onSubmit(valores: creacionTipoProductoDTO,
        acciones: FormikHelpers<creacionTipoProductoDTO>): void;
}