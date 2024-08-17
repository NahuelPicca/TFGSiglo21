import { Form, Formik, FormikHelpers } from "formik";
import { registroUsuario } from "./auth.model";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import FormGroupFecha from "../utils/FormGroupFecha";

export default function FormularioRegistro(props: formularioRegistroProps) {

    return (
        <div className="test">
            <Formik initialValues={props.modelo}
                onSubmit={props.onSubmit}
                validationSchema={Yup.object({
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
                    email: Yup.string()
                        .required('El email es requerido.')
                        .email('Debe colocar un email válido.'),
                    nombre: Yup.string().required('El nombre es requerido.').primeraLetraMayuscula(),
                    apellido: Yup.string().required('El apellido es requerido.').primeraLetraMayuscula(),
                    nroDni: Yup.string().required('El DNI es requerido.').max(9, 'No puede superar 9 carácteres.'),
                    cuit: Yup.string().required('El CUIT es requerido.').max(12, 'No puede superar 12 carácteres.'),
                    direccion: Yup.string().required('La dirección es requerida.').primeraLetraMayuscula(),
                    telefono: Yup.string()
                        .required('El teléfono es requerido.')
                        .matches(/^\d{10}$/, "El teléfono tiene que tener hasta 10 números."),
                    codigoPostal: Yup.string().required('El código postal es requerido.'),
                    localidad: Yup.string().required('La localidad es requerido.').primeraLetraMayuscula(),
                    fechaNacimiento: Yup.date().required('La fecha es requerido.')
                        .min(new Date(1920, 1, 1), 'La fecha debe ser mayor a 1920.')
                        .max(new Date(), 'La fecha debe menor a la de hoy.')
                })}
            >
                {formikProps => (
                    <>
                        <div className="form-container">
                            <h3 className="h3-Registro">Registro de usuario</h3>
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
                                    campo="email"
                                    type="text" />
                                <FormGroupFecha label="Fecha Nacimiento"
                                    campo="fechaNacimiento" />
                                <FormGroupText label="Contraseña"
                                    campo='contraseña'
                                    type="password" />
                                <FormGroupText label="Confirmar Contraseña"
                                    campo='confirmaPassword'
                                    type="password" />
                                {/* <ChecksUsuarios modelo={props.modelo} /> */}
                                {/* <MiComponente objeto={{ nombre: 'perez' }} /> */}
                                <Button disabled={formikProps.isSubmitting}
                                    type="submit" className="boton-violeta-IniSReg">
                                    Continuar
                                </Button>
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                                <br></br>
                            </Form>
                        </div>
                    </>
                )}
            </Formik>
        </div>
    )
}

interface formularioRegistroProps {
    modelo: registroUsuario;
    onSubmit(valores: registroUsuario,
        acciones: FormikHelpers<registroUsuario>): void;
}