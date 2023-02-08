﻿using AutoMapper;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Titular, TitularDto>().ReverseMap();
            CreateMap<Transportista, TransportistaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<TipoProducto, TipoProductoDto>().ReverseMap();
            CreateMap<CreateCalidadDto, Calidad>();
            CreateMap<Calidad, CalidadDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<CreateProductoDto, Producto>();
            //CreateMap<GeneroCreacionDto, Genero>();
        }
    }
}
