export interface productoDTO{
    id: number;
    codigo: string;
    descripcion: string;
    marca: string;
    anio: Date;
    habilitado: bool;
    calidadId: int;
    tipoId: int;
    precio: number;
}

export interface creacionProductoDTO{
    codigo: string;
    descripcion: string;
    marca: string;
    anio?: Date;
    habilitado: bool;
    calidadId: int;
    tipoId: int;
    precio: number;
}