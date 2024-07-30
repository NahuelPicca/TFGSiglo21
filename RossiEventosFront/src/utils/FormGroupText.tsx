import { ErrorMessage, Field } from "formik";
import MostrarErrorCampo from "./MostrarErrorCampo";

export default function FormGroupText(props: formGroupTextProps) {
    return (
        <div className=" p-1">
            {props.label ? <label htmlFor={props.campo}>{props.label}</label> : null}
            <Field type={props.type} name={props.campo} className="form-control"
                placeholder={props.placeholder} />
            <ErrorMessage name={props.campo}>{mensaje =>
                <MostrarErrorCampo mensaje={mensaje} />}</ErrorMessage>
        </div>
    )
}

interface formGroupTextProps {
    campo: string;
    label?: string;
    placeholder?: string;
    type: 'text' |
    'password' |
    'number';
}

FormGroupText.defaultProps = {
    type: 'text'
}