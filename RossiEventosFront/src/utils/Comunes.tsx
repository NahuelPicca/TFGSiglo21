export function convierteFormatoFecha(fecha: Date): Date {
    var fecha = new Date(fecha)
    return new Date(fecha.getFullYear(), fecha.getMonth(), fecha.getDay());
}