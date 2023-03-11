using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/localizacion")]
    [ApiController]
    public class LocalizacionController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<LocalizacionController> logger;
        private readonly IMapper mapper;

        public LocalizacionController(ILogger<LocalizacionController> logger,
                                      AppDbContext context,
                                      IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        async Task<List<Localizacion>> GetListLocalizaciones()
        {
            return await context.Localizacion
                                .Include(r => r.Seguimiento)
                                .ToListAsync();
        }

        async Task<Localizacion> GetLocalizacion(int id)
        {
            var lista = await GetListLocalizaciones();
            return lista.FirstOrDefault(t => t.Id == id);
        }

        [HttpGet()]
        public async Task<ActionResult<List<LocalizacionDto>>> GetListlocalizacionDto()
        {
            logger.LogInformation("Lista de loacalizaciones");
            var listLocaliza = await GetListLocalizaciones();
            return mapper.Map<List<LocalizacionDto>>(listLocaliza);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LocalizacionDto>> GetLocalizacionDto(int id)
        {
            logger.LogInformation("Obtiene una localizacion");
            var localizacion = await GetLocalizacion(id);
            if (localizacion != null)
                return mapper.Map<LocalizacionDto>(localizacion);
            return NotFound($"No se encontró la Localizacion con el Id: {id}");
        }

        [HttpPost()]
        public async Task<ActionResult> PostLocalizacionDto([FromBody] CLocalizacionDto create)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var loc = mapper.Map<Localizacion>(create);
                HidrataPropFaltanteAdd(create, loc);
                context.Add(loc);
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

        void HidrataPropFaltanteAdd(CLocalizacionDto create
                                  , Localizacion localizacion)
        {
            if (localizacion.Id == 0)
                localizacion.Seguimiento = context.SeguimientoPedido
                                                  .FirstOrDefault(s => s.Id == localizacion.SeguimientoId);
        }

        void HidrataPropFaltanteUpd(ULocalizacionDto create
                                  , Localizacion localizacion)
        {
            if (localizacion.Id > 0)
                localizacion.FechaModificacion = DateTime.Now;
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ULocalizacionDto create)
        {
            try
            {
                var locDb = await GetLocalizacion(id);
                if (locDb != null)
                {
                    var seg = mapper.Map<ULocalizacionDto, Localizacion>(create, locDb);
                    HidrataPropFaltanteUpd(create, seg);
                    var aa = await context.SaveChangesAsync();
                    return Ok(aa);
                }
                return NotFound($"No se encontró la Localizacion con el Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteLocalizacion(int id)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var loc = await GetLocalizacion(id);
                if (loc != null)
                {
                    var mensaje = $"Se eliminó OK la Localizacion " +
                                  $"Id {loc.Id}";
                    context.Remove(loc);
                    context.SaveChanges();
                    await context.Database.CommitTransactionAsync();
                    return Ok(mensaje);
                }
                return NotFound($"No se encontró la Localización con el Id: {id}");
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
