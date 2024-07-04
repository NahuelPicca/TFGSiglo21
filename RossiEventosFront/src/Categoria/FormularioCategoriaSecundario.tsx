import { Form, Formik, FormikHelpers } from "formik";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";
import { creacionCategoriaDTO } from "./categoria.modulo";

export default function FormularioCategoria(props: formularioCategoriaProps) {
    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                nombre: Yup.string().required('El nombre es requerido.').primeraLetraMayuscula(),
                descripcion: Yup.string().required('La descripción es requerida.').primeraLetraMayuscula(),
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

interface formularioCategoriaProps {
    modelo: creacionCategoriaDTO;
    onSubmit(valores: creacionCategoriaDTO,
        acciones: FormikHelpers<creacionCategoriaDTO>): void;
}