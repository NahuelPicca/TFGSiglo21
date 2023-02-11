using AutoMapper;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Calidad, CalidadDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<CreateUpdateCalidadDto, Calidad>(); 
            CreateMap<CreateCategoriaDto, CategoriaDto>();
            CreateMap<CreateUpdateProductoDto, Producto>();
            CreateMap<CreateTransportistaDto, Transportista>();
            CreateMap<CreateTipoDeProductoDto, TipoProductoDto>();
            CreateMap<CreateTitularDto, Titular>();
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Transportista, TransportistaDto>().ReverseMap();
            CreateMap<TipoProducto, TipoProductoDto>().ReverseMap();
            CreateMap<Titular, TitularDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
