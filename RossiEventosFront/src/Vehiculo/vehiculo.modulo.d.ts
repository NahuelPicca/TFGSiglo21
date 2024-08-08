export interface vehiculoDTO{
    id: number;
    patente: string; 
    marca : string;
    modelo: string;
    fechaVencPoliza: Date; 
    nroPoliza: string;
    titularId: number;
    habilitado: bool;
    patenteModelo: string;
}

export interface creacionVehiculoDTO{
    patente: string; 
    marca : string;
    modelo: string;
    fechaVencPoliza?: Date; 
    nroPoliza: string;
    titularId: number;
    habilitado: bool;
}

export interface deleteVehiculoDTO{
    id: number;
}
