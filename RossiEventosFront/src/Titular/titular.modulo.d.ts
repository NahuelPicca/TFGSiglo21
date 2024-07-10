export interface titularDTO{
    id: number;
    nombre: string; 
    apellido: string;
    tipoDni: string;
    nroDni: int;
    cuit: string;
    direccion: string;
    telefono: string;
    localidad: string;
    email: string;
    codigoPostal: string;
}

export interface creacionTitularDTO{
    nombre: string; 
    apellido: string;
    tipoDni: string;
    nroDni: int;
    cuit: string;
    direccion: string;
    telefono: string;
    localidad: string;
    email: string;
    codigoPostal: string;
}

export interface deleteTitularDTO{
    id: number;
}

export interface comboTitularDTO{
    id: number;
    cuitApellidoNombre: string;
}