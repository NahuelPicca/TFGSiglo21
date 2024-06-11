export interface productoDTO{
    id: number;
    codigo: string;
    descripcion: string;
    marca: string;
    poster1: string;
    poster2: string;
    poster3: string;
    anio: Date;
    habilitado: bool;
    calidadId: int;
    tipoProductoId: int;
    precio: number;
}

export interface creacionProductoDTO{
    codigo: string;
    descripcion: string;
    marca: string;
    poster1?: File;
    poster2?: File;
    poster3?: File;
    posterURL1?: string;
    posterURL2?: string;
    posterURL3?: string;
    anio?: Date;
    habilitado: bool;
    calidadId: int;
    tipoProductoId: int;
    precio: number;
}