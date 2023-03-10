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
            CreateMap<CreateUpdateDepositoDto, Deposito>();
            CreateMap<CreateUpdateEncabezadoMovStkDto, EncabezadoMovStk>();
            CreateMap<CreateUpdateLocalizacionesDto, Localizacion>();
            CreateMap<CreateUpdatePedidoDto, Pedido>();
            CreateMap<CreateUpdateProductoDto, Producto>();
            CreateMap<CreateUpdateRenglonDeReservaDto, RenglonReserva>();
            CreateMap<CreateUpdateRenglonMovStkDto, RenglonMovStk>();
            CreateMap<CreateUpdateReservaDto, Reserva>();
            CreateMap<CreateUpdateSaldoUbiDto, SaldoUbicacion>();
            CreateMap<CreateUpdateSeguimientoPedidoDto, SeguimientoPedido>();
            CreateMap<CreateUpdateTransportistaDto, Transportista>();
            CreateMap<CreateUpdateTipoDeProductoDto, TipoProducto>();
            CreateMap<CreateUpdateTitularDto, Titular>();
            CreateMap<CreateUpdateUbicacionDto, Ubicacion>();
            CreateMap<CreateUpdateUsuarioDto, Usuario>();
            CreateMap<CreateUpdateVehiculoDto, Vehiculo>();
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
