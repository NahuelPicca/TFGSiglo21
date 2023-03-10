using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/seguimiento")]
    [ApiController]
    public class SeguimientoPedidoController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<SeguimientoPedidoController> logger;
        private readonly IMapper mapper;

        public SeguimientoPedidoController(ILogger<SeguimientoPedidoController> logger,
                                           AppDbContext context,
                                           IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        async Task<List<SeguimientoPedido>> GetListSeguimiento()
        {
            return await context.SeguimientoPedido
                                .Include(r => r.Pedido)
                                .Include(r => r.ComprobanteEntre)
                                .Include(r => r.Localizaciones)
                                .ToListAsync();
        }

        async Task<SeguimientoPedido> GetSeguimieto(int id)
        {
            var lista = await GetListSeguimiento();
            return lista.FirstOrDefault(t => t.Id == id);
        }

        [HttpGet()]
        public async Task<ActionResult<List<SeguimientoPedidoDto>>> GetListSeguimientoPedDto()
        {
            logger.LogInformation("Lista de seguimientos");
            var listSeguimiento = await GetListSeguimiento();
            return mapper.Map<List<SeguimientoPedidoDto>>(listSeguimiento);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SeguimientoPedidoDto>> GetSeguimientoPedDto(int id)
        {
            logger.LogInformation("Obtiene un Seguimiento");
            var seguimiento = await GetSeguimieto(id);
            if (seguimiento != null)
                return mapper.Map<SeguimientoPedidoDto>(seguimiento);
            return NotFound($"No se encontró el Seguimiento con el Id: {id}");
        }


        [HttpPost()]
        public async Task<ActionResult> PostSeguimientoDto([FromBody] CreateUpdateSeguimientoPedidoDto create)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var seg = mapper.Map<SeguimientoPedido>(create);
                HidrataPropFaltante(create, seg);
                context.Add(seg);
                var change = await context.SaveChangesAsync();
                await context.Database.CommitTransactionAsync();
                return Ok(change);
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }

        void HidrataPropFaltante(CreateUpdateSeguimientoPedidoDto create
                               , SeguimientoPedido seguimiento)
        {
            if (seguimiento.Id > 0)
                seguimiento.FechaModificacion = DateTime.Now;

            foreach (var localiz in seguimiento.Localizaciones)
                localiz.Seguimiento = seguimiento;
        }
    }
}
