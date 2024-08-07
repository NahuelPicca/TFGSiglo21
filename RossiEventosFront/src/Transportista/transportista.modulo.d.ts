export interface transportistaDTO{
    id: number;
    apellido: string;
    nombre: string;
    nroDni: string; 
    direccion: string;
    cuit: string;
    localidad: string;
    telefono: string;
    codigoPostal: string;
    email: string;
    fechaNacimiento: Date;
    fechaVencLicencia: Date;
    licencia: string;
    habilitado: bool;
    apellidoNombreLicencia: string;
}

export interface creacionTransportistaDTO{
    apellido: string;
    nombre: string;
    nroDni: string; 
    direccion: string;
    cuit: string;
    localidad: string;
    telefono: string;
    codigoPostal: string;
    email: string;
    fechaNacimiento?: Date;
    fechaVencLicencia?: Date;
    licencia: string;
    habilitado: bool;
}

export interface deleteTransportistaDTO{
    id: number;
}