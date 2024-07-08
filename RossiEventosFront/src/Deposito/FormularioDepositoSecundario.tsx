import { Form, Formik, FormikHelpers } from "formik";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";
import { creacionDepositoDTO } from "./deposito.modulo";
import FormGroupCheckbox from "../utils/FormGroupCheckbox";

export default function FormularioDeposito(props: formularioDepositoProps) {
    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                codigo: Yup.string().required('El código es requerido.').max(4, 'El código tiene que tener 4 carácteres.'),
                descripcion: Yup.string().required('La descripción es requerida.').primeraLetraMayuscula(),
                direccion: Yup.string().required('La dirección es requerida.').primeraLetraMayuscula(),
                localidad: Yup.string().required('La localidad es requerida.').primeraLetraMayuscula(),
                provincia: Yup.string().required('La provincia es requerida.').primeraLetraMayuscula(),
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
                    <FormGroupText label="Dirección"
                        campo='direccion'
                        type="text" />
                    <FormGroupText label="Localidad"
                        campo='localidad'
                        type="text" />
                    <FormGroupText label="Provincia"
                        campo='provincia'
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

interface formularioDepositoProps {
    modelo: creacionDepositoDTO;
    onSubmit(valores: creacionDepositoDTO,
        acciones: FormikHelpers<creacionDepositoDTO>): void;
}