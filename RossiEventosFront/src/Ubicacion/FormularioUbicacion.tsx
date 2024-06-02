import { Form, Formik, FormikHelpers } from "formik";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";
import FormGroupCheckbox from "../utils/FormGroupCheckbox";
import { creacionUbicacionDTO } from "./ubicacion.modulo";

export default function FormularioUbicacion(props: formularioUbicacionProps) {
    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                codigo: Yup.string().required('El código es requerido.').max(4, 'El código tiene que tener 4 carácteres.'),
                nombre: Yup.string().required('El nombre es requerida.').primeraLetraMayuscula(),
                columna: Yup.string().required('La columna es requerida.').primeraLetraMayuscula().max(10, 'La columna tiene que tener como máximo 10 carácteres.'),
                estante: Yup.string().required('La estante es requerida.').primeraLetraMayuscula().max(10, 'El estante tiene que tener como máximo 10 carácteres.'),
                fila: Yup.string().required('La fila es requerida.').primeraLetraMayuscula().max(10, 'La fila tiene que tener como máximo 10 carácteres.'),
            })}
        >
            {formikProps => (
                <Form>
                    <FormGroupText label="Codigo"
                        campo='codigo'
                        type="text" />
                    <FormGroupText label="Nombre"
                        campo='nombre'
                        type="text" />
                    <FormGroupText label="Columna"
                        campo='columna'
                        type="text" />
                    <FormGroupText label="Estante"
                        campo='estante'
                        type="text" />
                    <FormGroupText label="Fila"
                        campo='fila'
                        type="text" />
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

interface formularioUbicacionProps {
    modelo: creacionUbicacionDTO;
    onSubmit(valores: creacionUbicacionDTO,
        acciones: FormikHelpers<creacionUbicacionDTO>): void;
}