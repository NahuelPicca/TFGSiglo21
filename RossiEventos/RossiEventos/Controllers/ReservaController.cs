using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/reserva")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<ReservaController> logger;
        private readonly IMapper mapper;

        public ReservaController(ILogger<ReservaController> logger,
                                 AppDbContext context,
                                 IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        async Task<List<Reserva>> GetListReserva()
        {
            return await context.Reservas
                                .Include(r => r.Renglones)
                                .ThenInclude(r => r.Producto)
                                .ToListAsync();
        }

        Producto GetProducto(RenglonReserva reng)
        {
            return context.Producto
                          .FirstOrDefault(p => p.Id == reng.ProductoId);
        }

        Usuario GetUsuario(CreateUpdateReservaDto create)
        {
            return context.Usuario
                          .FirstOrDefault(p => p.Id == create.UsuarioId);
        }

        void HidrataPropFaltante(CreateUpdateReservaDto create
                               , Reserva reserva)
        {
            if (reserva.Id > 0)
                reserva.FechaModificacion = DateTime.Now;
            reserva.Usuario = GetUsuario(create);
            foreach (var reng in reserva.Renglones)
            {
                //DefineCantidad(create, reng, saldo);
                var producto = GetProducto(reng);
                reng.PrecioUnit = producto.Precio;
                reng.Producto = producto;
                reng.Reserva = reserva;
            }
        }

        [HttpGet()]
        public async Task<ActionResult<List<ReservaDto>>> GetListaResrvaDto()
        {
            logger.LogInformation("Lista de reservas");
            var listReserva = GetListReserva();
            return mapper.Map<List<ReservaDto>>(listReserva);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReservaDto>> GetReservaDto(int id)
        {
            logger.LogInformation("Obtiene una Reserva");
            var listReserva = await GetListReserva();
            var reserva = listReserva.FirstOrDefault(r => r.Id == id);
            if (reserva != null)
                return mapper.Map<ReservaDto>(reserva);
            return NotFound($"No se encontró la Reserva con el Id: {id}");
        }

        [HttpPost()]
        public async Task<ActionResult> PostReservaDto([FromBody] CreateUpdateReservaDto create)
        {
            try
            {
                context.Database.BeginTransactionAsync();
                var reserva = mapper.Map<Reserva>(create);
                HidrataPropFaltante(create, reserva);
                context.Add(reserva);
                var cambios = await context.SaveChangesAsync();
                context.Database.CommitTransactionAsync();
                return Ok(cambios);
            }
            catch (Exception ex)
            {
                context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
