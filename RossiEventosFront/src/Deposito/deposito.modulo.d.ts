export interface depositoDTO{
    id:number;
    codigo: string;
    descripcion: string;
    direccion: string;
    localidad: string;
    provincia: string;
    habilitado: boolean;
}

export interface creacionDepositoDTO{
    codigo: string;
    descripcion: string;
    direccion: string;
    localidad: string;
    provincia: string;
    habilitado: boolean;
}

export interface deleteDepositoDTO{
    id: number;
}