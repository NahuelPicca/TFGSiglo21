export interface claim{
    nombre: string;
    valor: string;
}

export interface credencialesUsuario{
    email: string;
    password: string;
}

export interface respuestaAutenticacion{
    token: string;
    expiracion: Date;
}

export interface usuarioDTO{
    id: string;
    email: string;
}

export interface registroUsuario{
    nombre: string;
    apellido: string;
    nroDni: string;
    cuit: string;
    direccion: string;
    telefono: string;
    codigoPostal: string;
    localidad: string;
    email: string;
    contraseña: string;
    fechaNacimiento: string;
    tipo: number;
}