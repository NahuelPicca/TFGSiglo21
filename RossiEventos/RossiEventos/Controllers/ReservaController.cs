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

        async Task<Reserva> GetReserva(int id)
        {
            var listReserva = await GetListReserva();
            return listReserva.FirstOrDefault(t => t.Id == id);
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
                if (reng.Id > 0)
                    reng.FechaModificacion = DateTime.Now;
                var producto = GetProducto(reng);
                reng.PrecioUnit = producto.Precio;
                reng.Producto = producto;
                reng.Reserva = reserva;
            }
        }

        void RemoveObject(Reserva reserva)
        {
            //Elimina la reserva con sus renglones.
            context.Reservas.Remove(reserva);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteReserva(int id)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var reserva = await GetReserva(id);
                if (reserva != null)
                {
                    var mensaje = $"Se eliminó OK la Reserva con " +
                                  $"Id {reserva.Id}" +
                                  $"NroReserva {reserva.NroReserva}" +
                                  $"con sus renglones correspondientes.";
                    // RestableceCantidad(reserva);
                    RemoveObject(reserva);
                    context.SaveChanges();
                    await context.Database.CommitTransactionAsync();
                    return Ok(mensaje);
                }
                return NotFound($"No se encontró el Movimiento con el Id: {id}");
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<List<ReservaDto>>> GetListaResrvaDto()
        {
            logger.LogInformation("Lista de reservas");
            var listReserva = await GetListReserva();
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
                await context.Database.BeginTransactionAsync();
                var reserva = mapper.Map<Reserva>(create);
                HidrataPropFaltante(create, reserva);
                context.Add(reserva);
                var cambios = await context.SaveChangesAsync();
                await context.Database.CommitTransactionAsync();
                return Ok(cambios);
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutReservaDto(int id, [FromBody] CreateUpdateReservaDto create)
        {
            try
            {
                Reserva reservaDb = await GetReserva(id);
                var reserva = mapper.Map<CreateUpdateReservaDto, Reserva>(create, reservaDb);
                HidrataPropFaltante(create, reserva);
                context.Reservas.Update(reserva);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
