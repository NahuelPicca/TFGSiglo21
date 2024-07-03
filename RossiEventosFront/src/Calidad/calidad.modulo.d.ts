export interface calidadDTO{
    id: number;
    codigo: string;
    nombre: string;
    descripcion: string;
}

export interface calidadCreacionDTO{
    codigo: string;
    nombre: string;
    descripcion: string;
}

export interface deleteCalidadDTO{
    id: number;
    codigo: string;
}

export interface calidadEditarDTO{
    id: number;
    codigo: string;
    nombre: string;
    descripcion: string;
}