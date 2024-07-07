import { Form, Formik, FormikHelpers } from "formik";
import * as Yup from 'yup';
import FormGroupText from "../utils/FormGroupText";
import Button from "../utils/Button";
import { Link } from "react-router-dom";
import FormGroupCheckbox from "../utils/FormGroupCheckbox";
import { creacionTransportistaDTO } from "./transportista.modulo";
import FormGroupFecha from "../utils/FormGroupFecha";

export default function FormularioTransportista(props: formularioTransportistaProps) {
    return (
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
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
                    .max(new Date(), 'La fecha debe menor a la de hoy.'),
                fechaVencLicencia: Yup.date().required('La fecha de vencimiento de la licencia es requerida.')
                    .min(new Date(), 'La fecha debe ser mayor a hoy.'),
                licencia: Yup.string().required('La licencia es requerida.')
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
                        campo="email"
                        type="text" />
                    <FormGroupFecha label="Fecha Nacimiento"
                        campo="fechaNacimiento" />
                    <FormGroupFecha label="Fecha Vencimiento de la Licencia"
                        campo="fechaVencLicencia" />
                    <FormGroupText label="Licencia"
                        campo='licencia'
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

interface formularioTransportistaProps {
    modelo: creacionTransportistaDTO;
    onSubmit(valores: creacionTransportistaDTO,
        acciones: FormikHelpers<creacionTransportistaDTO>): void;
}