using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<PedidoController> logger;
        private readonly IMapper mapper;

        public PedidoController(ILogger<PedidoController> logger,
                                AppDbContext context,
                                IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        async Task<List<Pedido>> GetListPedido()
        {
            return await context.Pedidos
                                .Include(p => p.Asignacion)
                                .ThenInclude(p => p.Transportista)
                                .Include(p => p.Asignacion)
                                .ThenInclude(p => p.Vehiculo)
                                .Include(p => p.Reserva)
                                .ToListAsync();
        }

        async Task<Pedido> GetPedido(int id)
        {
            var listPedido = await GetListPedido();
            return listPedido.FirstOrDefault(p => p.Id == id);
        }


        [HttpGet()]
        public async Task<ActionResult<List<Pedido>>> GetListaPedidoDto()
        {
            logger.LogInformation("Lista de pedidos");
            var listPedidos = await GetListPedido();
            return mapper.Map<List<Pedido>>(listPedidos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pedido>> GetPedidoDto(int id)
        {
            logger.LogInformation("Obtiene una pedido");
            var pedido = await GetPedido(id);
            if (pedido != null)
                return mapper.Map<Pedido>(pedido);
            return NotFound($"No se encontró el Pedido con el Id: {id}");
        }

        [HttpPost()]
        public async Task<ActionResult> PostPedidoDto([FromBody] CreateUpdatePedidoDto create)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var pedido = mapper.Map<Pedido>(create);
                HidrataPropFaltante(create, pedido);
                context.Add(pedido);
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

        void HidrataPropFaltante(CreateUpdatePedidoDto create, Pedido pedido)
        {
            if (pedido.Id > 0)
                pedido.FechaModificacion = DateTime.Now;
            pedido.Factura = Guid.NewGuid().ToString().Substring(1, 13);
            pedido.NroPedido = Guid.NewGuid().ToString().Substring(1, 13);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutPedidoDto(int id, [FromBody] CreateUpdatePedidoDto create)
        {
            try
            {
                Pedido pedidoDb = await GetPedido(id);
                var pedido = mapper.Map<CreateUpdatePedidoDto, Pedido>(create, pedidoDb);
                HidrataPropFaltante(create, pedido);
                context.Pedidos.Update(pedido);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePedido(int id)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var pedido = await GetPedido(id);
                if (pedido != null)
                {
                    var mensaje = $"Se eliminó OK el pedido con " +
                                  $"Id {pedido.Id}" +
                                  $"NroPedido {pedido.NroPedido}";
                    // RestableceCantidad(reserva);
                    RemoveObject(pedido);
                    context.SaveChanges();
                    await context.Database.CommitTransactionAsync();
                    return Ok(mensaje);
                }
                return NotFound($"No se encontró el Pedido con el Id: {id}");
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }

        void RemoveObject(Pedido pedido)
        {
            //Elimina el pedido.
            context.Pedidos.Remove(pedido);
        }
    }
}
