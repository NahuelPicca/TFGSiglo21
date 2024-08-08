export interface asigVehiculoTransportistaDTO{
    id: number;
    patente: string;
    modelo: string;
    nombreTransportista: string;
    apellidoTransportista: string;
    licencia: string;
    transportitaId: number;
    vehiculoId: number;
}

export interface asigVehiculoTransportistaCreacionDTO{
    patente: string;
    modelo: string;
    nombreTransportista: string;
    apellidoTransportista: string;
    licencia: string;
    transportitaId: number;
    vehiculoId: number;
}

export interface deleteAsigVehiculoTransportistaDTO{
    id: number;
}

export interface asigVehiculoTransportistaEditarDTO{
    id: number;
    patente: string;
    modelo: string;
    nombreTransportista: string;
    apellidoTransportista: string;
    licencia: string;
    transportistaId: number;
    vehiculoId: number;
}