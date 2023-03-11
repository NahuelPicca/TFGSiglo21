using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/comprobanteEntrega")]
    [ApiController]
    public class ComprobanteEntregaController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<ComprobanteEntregaController> logger;
        private readonly IMapper mapper;

        public ComprobanteEntregaController(ILogger<ComprobanteEntregaController> logger,
                                            AppDbContext context,
                                            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        async Task<List<ComprobanteEntrega>> GetListCompEntrega()
        {
            return await context.ComprobanteEntrega
                                .ToListAsync();
        }

        async Task<ComprobanteEntrega> GetCompEntrega(int id)
        {
            var lista = await GetListCompEntrega();
            return lista.FirstOrDefault(t => t.Id == id);
        }

        [HttpGet()]
        public async Task<ActionResult<List<ComprobanteEntregaDto>>> GetListCompEntregaDto()
        {
            logger.LogInformation("Lista de comprobante entrega");
            var listCompEnt = await GetListCompEntrega();
            return mapper.Map<List<ComprobanteEntregaDto>>(listCompEnt);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ComprobanteEntregaDto>> GetCompEntregaDto(int id)
        {
            logger.LogInformation("Obtiene un Seguimiento");
            var compEntrega = await GetCompEntrega(id);
            if (compEntrega != null)
                return mapper.Map<ComprobanteEntregaDto>(compEntrega);
            return NotFound($"No se encontró el Seguimiento con el Id: {id}");
        }

        [HttpPost()]
        public async Task<ActionResult> PostCompEntregaDto([FromBody] CUComprobanteEntrega create)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var cEntrega = mapper.Map<ComprobanteEntrega>(create);
                HidrataPropFaltante(create, cEntrega);
                context.Add(cEntrega);
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CUComprobanteEntrega create)
        {
            try
            {
                var compEntrega = await GetCompEntrega(id);
                if (compEntrega != null)
                {
                    var comp = mapper.Map<CUComprobanteEntrega, ComprobanteEntrega>(create, compEntrega);
                    HidrataPropFaltante(create, comp);
                    var aa = await context.SaveChangesAsync();
                    return Ok(aa);
                }
                return NotFound($"No se encontró el Comprobante de Entrega con el Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        void HidrataPropFaltante(CUComprobanteEntrega create
                               , ComprobanteEntrega cEntrega)
        {
            if (cEntrega.Id > 0)
                cEntrega.FechaModificacion = DateTime.Now;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCompEntrega(int id)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var comp = await GetCompEntrega(id);
                if (comp != null)
                {
                    var mensaje = $"Se eliminó OK el Comprobante Entrega con " +
                                  $"Id {comp.Id}";
                    context.ComprobanteEntrega.Remove(comp);
                    context.SaveChanges();
                    await context.Database.CommitTransactionAsync();
                    return Ok(mensaje);
                }
                return NotFound($"No se encontró el Comprobante Entrega con el Id: {id}");
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }

    }
}
