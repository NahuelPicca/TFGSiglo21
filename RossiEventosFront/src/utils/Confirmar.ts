import Swal from 'sweetalert2';

export default function confirmar(
    onConfirm: any,
    titulo: string = '¿Desea borrar el registro?',
    textoBotonConfirmacion: string = 'Borrar'
) {
    // Se instaló el paquete sweetalert2 con el comando npm i sweetalert2
    // para el manejo de los confirmas.
Swal.fire({
    title: titulo,
    confirmButtonText: textoBotonConfirmacion,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
}).then(result => {
    if(result.isConfirmed){
        onConfirm();
    }
})


}