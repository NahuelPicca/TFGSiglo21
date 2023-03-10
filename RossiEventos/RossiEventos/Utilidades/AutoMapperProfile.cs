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
            CreateMap<CUAsignacionVehicTranspDto, AsignacionVehicTransp>();
            CreateMap<CUCalidadDto, Calidad>(); 
            CreateMap<CUCategoriaDto, Categoria>();
            CreateMap<CUDepositoDto, Deposito>();
            CreateMap<CUEncabezadoMovStkDto, EncabezadoMovStk>();
            CreateMap<CULocalizacionesDto, Localizacion>();
            CreateMap<CUPedidoDto, Pedido>();
            CreateMap<CUProductoDto, Producto>();
            CreateMap<CURenglonDeReservaDto, RenglonReserva>();
            CreateMap<CURenglonMovStkDto, RenglonMovStk>();
            CreateMap<CUReservaDto, Reserva>();
            CreateMap<CUSaldoUbiDto, SaldoUbicacion>();
            CreateMap<CUSeguimientoPedidoDto, SeguimientoPedido>();
            CreateMap<CUTransportistaDto, Transportista>();
            CreateMap<CUTipoDeProductoDto, TipoProducto>();
            CreateMap<CUTitularDto, Titular>();
            CreateMap<CUUbicacionDto, Ubicacion>();
            CreateMap<CUUsuarioDto, Usuario>();
            CreateMap<CUVehiculoDto, Vehiculo>();
            CreateMap<Deposito, DepositoDto>().ReverseMap();
            CreateMap<EncabezadoMovStkDto, EncabezadoMovStk>().ReverseMap();
            CreateMap<LocalizacionDto, Localizacion>().ReverseMap();
            CreateMap<Pedido, PedidoDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<RenglonMovStk, RenglonMovStkDto>().ReverseMap();
            CreateMap<RenglonReserva, RenglonReservaDto>().ReverseMap();
            CreateMap<Reserva, ReservaDto>().ReverseMap();
            CreateMap<SaldoUbicacion, SaldoUbicacionDto>().ReverseMap();
            CreateMap<SeguimientoPedido, SeguimientoPedidoDto>().ReverseMap();
            CreateMap<Transportista, TransportistaDto>().ReverseMap();
            CreateMap<TipoProducto, TipoProductoDto>().ReverseMap();
            CreateMap<Titular, TitularDto>().ReverseMap();
            CreateMap<Ubicacion, UbicacionDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Vehiculo, VehiculoDto>().ReverseMap();
        }
    }
}
