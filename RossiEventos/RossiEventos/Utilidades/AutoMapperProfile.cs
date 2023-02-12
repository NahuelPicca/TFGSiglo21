using AutoMapper;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AsignacionVehicTransp, AsignacionVehicTranspDto>().ReverseMap();
            CreateMap<Calidad, CalidadDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<CreateUpdateAsignacionVehicTranspDto, AsignacionVehicTransp>();
            CreateMap<CreateUpdateCalidadDto, Calidad>(); 
            CreateMap<CreateUpdateCategoriaDto, Categoria>();
            CreateMap<CreateUpdateProductoDto, Producto>();
            CreateMap<CreateUpdateTransportistaDto, Transportista>();
            CreateMap<CreateUpdateTipoDeProductoDto, TipoProducto>();
            CreateMap<CreateUpdateTitularDto, Titular>();
            CreateMap<CreateUpdateUsuarioDto, Usuario>();
            CreateMap<CreateUpdateVehiculoDto, Vehiculo>();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Transportista, TransportistaDto>().ReverseMap();
            CreateMap<TipoProducto, TipoProductoDto>().ReverseMap();
            CreateMap<Titular, TitularDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Vehiculo, VehiculoDto>().ReverseMap();
        }
    }
}
