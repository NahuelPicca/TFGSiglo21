import { creacionProductoDTO, productoDTO } from "./producto.modulo";

export function convertirProductoAFormData(producto: creacionProductoDTO): FormData {
    const formData = new FormData();
    formData.append('codigo', producto.codigo);
    formData.append('descripcion', producto.descripcion);
    formData.append('marca', producto.marca);

    // if (producto.poster1) {
    //     formData.append("poster1", producto.poster1);
    // }
    // producto.poster1 ? formData.append("poster1", producto.poster1) : formData.append("poster1", "");
    // producto.poster2 ? formData.append("poster2", producto.poster2) : formData.append("poster2", "");
    // producto.poster3 ? formData.append("poster3", producto.poster3) : formData.append("poster3", "");
    // if (producto.poster2) {
    //     formData.append("poster2", producto.poster2);
    // }
    // if (producto.poster3) {
    //     formData.append("poster3", producto.poster3);
    // }
    if (producto.anio) {
        formData.append('anio', formatearFecha(producto.anio));
    }
    formData.append("calidadId", JSON.parse(producto.calidadId));
    formData.append("tipoProductoId", JSON.parse(producto.tipoProductoId));
    formData.append("precio", JSON.stringify(producto.precio));
    return formData;
}

export function convertirProductoAFormDataProductoDTO(producto: productoDTO): FormData {
    const formData = new FormData();
    formData.append('codigo', producto.codigo);
    formData.append('descripcion', producto.descripcion);
    formData.append('marca', producto.marca);

    // if (producto.poster1) {
    //     formData.append("poster1", producto.poster1);
    // }
    producto.poster1 ? formData.append("poster1", producto.poster1) : formData.append("poster1", "");
    producto.poster2 ? formData.append("poster2", producto.poster2) : formData.append("poster2", "");
    producto.poster3 ? formData.append("poster3", producto.poster3) : formData.append("poster3", "");
    // if (producto.poster2) {
    //     formData.append("poster2", producto.poster2);
    // }
    // if (producto.poster3) {
    //     formData.append("poster3", producto.poster3);
    // }
    if (producto.anio) {
        formData.append('anio', formatearFecha(producto.anio));
    }
    formData.append("calidadId", JSON.parse(producto.calidadId));
    formData.append("tipoProductoId", JSON.parse(producto.tipoProductoId));
    formData.append("precio", JSON.stringify(producto.precio));
    formData.append("habilitado", producto.habilitado);
    return formData;
}

export function formatearFecha(date: Date) {
    date = new Date(date);
    const formato = new Intl.DateTimeFormat("en", {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
    });

    const [
        { value: month }, ,
        { value: day }, ,
        { value: year }
    ] = formato.formatToParts(date);

    return `${year}-${month}-${day}`
}