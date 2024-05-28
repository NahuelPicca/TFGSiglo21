import { StringSchema } from "yup";

//Se define el Schema para YUP
//Ver documentación de YUP cualquier cosa.
declare module 'yup'{
    class StringSchema{
        primeraLetraMayuscula():this;
    }
}