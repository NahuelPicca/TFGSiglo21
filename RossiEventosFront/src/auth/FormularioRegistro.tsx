import { Form, Formik, FormikHelpers } from "formik";
import { registroUsuario } from "./auth.model";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";

export default function FormularioRegistro(props: formularioRegistroProps) {

    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                email: Yup.string()
                    .required('El email es requerido.')
                    .email('Debe colocar un email válido.'),
                contraseña: Yup.string().required('La contraseña es requerido.')
                    .min(8, 'La contraseña debe tener como mínimo 8 carácteres.')
                    .matches(
                        /[!@#$%^&*(),.?":{}|<>]/,
                        "La contraseña tiene que contener algún símbolo."
                    )
                    .matches(/[0-9]/, "La contraseña debe contener aunque sea un número.")
                    .matches(/[A-Z]/, "La contraseña debe contener letras mayúsculas.")
                    .matches(/[a-z]/, "La contraseña debe contener letras minúsculas."),
                confirmaPassword: Yup.string().oneOf([Yup.ref("contraseña")], "No coinciden las contraseñas."),
                nombre: Yup.string().required('El nombre es requerido.'),
                apellido: Yup.string().required('El apellido es requerido.'),
                nroDni: Yup.string().required('El DNI es requerido.'),
                cuit: Yup.string().required('El CUIT es requerido.'),
                direccion: Yup.string().required('La dirección es requerida.'),
                telefono: Yup.string()
                    .required('El teléfono es requerido.')
                    .matches(/^\d{10}$/, "El teléfono tiene que tener hasta 10 números."),
                codigoPostal: Yup.string().required('El código postal es requerido.'),
                localidad: Yup.string().required('La localidad es requerido.'),
            })}
        >
            {formikProps => (
                <Form>
                    <FormGroupText label="Nombre"
                        campo='nombre'
                        type="text" />
                    <FormGroupText label="Apellido"
                        campo='apellido'
                        type="text" />
                    <FormGroupText label="Nro Dni"
                        campo='nroDni'
                        type="text" />
                    <FormGroupText label="Cuit"
                        campo='cuit'
                        type="text" />
                    <FormGroupText label="Dirección"
                        campo='direccion'
                        type="text" />
                    <FormGroupText label="Teléfono"
                        campo='telefono'
                        type="text" />
                    <FormGroupText label="Codigo Postal"
                        campo='codigoPostal'
                        type="text" />
                    <FormGroupText label="Localidad"
                        campo='localidad'
                        type="text" />
                    <FormGroupText label="Email"
                        campo="email" />
                    <FormGroupText label="Contraseña"
                        campo='contraseña'
                        type="password" />
                    <FormGroupText label="Confirmar Contraseña"
                        campo='confirmaPassword'
                        type="password" />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">
                        Enviar
                    </Button>
                    <Link className="btn btn-secondary" to="/">Cancelar</Link>
                </Form>
            )}
        </Formik>
    )
}

interface formularioRegistroProps {
    modelo: registroUsuario;
    onSubmit(valores: registroUsuario,
        acciones: FormikHelpers<registroUsuario>): void;
}