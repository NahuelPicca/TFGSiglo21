import { Form, Formik, FormikHelpers } from "formik";
import { credencialesUsuario } from "./auth.model";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";

export default function FormularioAuth(props: formularioAuthProps) {

    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                email: Yup.string().required('Este campo es requerido')
                    .email('Debe colocar un email válido'),
                password: Yup.string().required('Este campo es requerido')
            })}
        >
            {formikProps => (
                <>
                    <div className="form-container">
                        <h3 className="h3-InicioSesion">Inicio de sesión</h3>
                        <Form>
                            <FormGroupText label="Email"
                                campo="email" />
                            <FormGroupText label="Constraseña"
                                campo='password'
                                type="password" />
                            <Link to='../' className="label-olvidarContrasena">Olvidaste tu contraseña</Link>
                            <Button disabled={formikProps.isSubmitting}
                                type="submit" className="boton-violeta-IniSReg">
                                Enviar
                            </Button>
                        </Form>
                    </div>
                </>
            )}
        </Formik>
    )
}

interface formularioAuthProps {
    modelo: credencialesUsuario;
    onSubmit(valores: credencialesUsuario,
        acciones: FormikHelpers<credencialesUsuario>): void;
}