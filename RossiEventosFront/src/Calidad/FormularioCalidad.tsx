import { Form, Formik, FormikHelpers } from "formik";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";
import { calidadCreacionDTO, calidadDTO } from "./calidad.modulo";

export default function FormularioCalidad(props: formularioCalidadProps) {
    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                codigo: Yup.string().required('El código es requerido.'),
                nombre: Yup.string().required('El nombre es requerido.').primeraLetraMayuscula(),
                descripcion: Yup.string().required('La descripcion es requerida.')
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
                    <FormGroupText label="Descripcion"
                        campo='descripcion'
                        type="text" />
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

interface formularioCalidadProps {
    modelo: calidadCreacionDTO;
    onSubmit(valores: calidadCreacionDTO,
        acciones: FormikHelpers<calidadCreacionDTO>): void;
}