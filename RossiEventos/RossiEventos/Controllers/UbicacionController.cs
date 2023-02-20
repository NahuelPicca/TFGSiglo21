using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/ubicacion")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<Ubicacion> logger;
        private readonly IMapper mapper;

        public UbicacionController(ILogger<Ubicacion> logger,
                                   AppDbContext context,
                                   IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDeposito(int id)
        {
            var ubicacion = await context.Ubicacion
                                         .FirstOrDefaultAsync(u => u.Id == id);
            if (ubicacion != null)
            {
                var mensaje = $"Se eliminó OK la ubicación " +
                              $"{ubicacion.Codigo} {ubicacion.Nombre}";
                context.Ubicacion.Remove(ubicacion);
                context.SaveChanges();
                return Ok(mensaje);
            }
            return NotFound($"No se encontró el Ubicación con el Id: {id}");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UbicacionDto>> GetUbicacionDto(int id)
        {
            logger.LogInformation("Obtiene una Ubicación");
            var ubicacion = await context.Ubicacion
                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (ubicacion != null)
                return mapper.Map<UbicacionDto>(ubicacion);
            return NotFound($"No se encontró la Ubicación con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<UbicacionDto>>> GetListUbicacionesDto()
        {
            logger.LogInformation("Lista de ubicaciones");
            var listUbicaciones = await context.Ubicacion
                                               .ToListAsync();
            return mapper.Map<List<UbicacionDto>>(listUbicaciones);
        }

        [HttpPost()]
        public async Task<ActionResult> PostUbicacionDto([FromBody] CreateUpdateUbicacionDto create)
        {
            try
            {
                var ubi = mapper.Map<Ubicacion>(create);
                HidrataPropFaltante(create, ubi);
                context.Add(ubi);
                var cambios = await context.SaveChangesAsync();
                return Ok(cambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        void HidrataPropFaltante(CreateUpdateUbicacionDto create, Ubicacion ubi)
        {
            if (ubi.Id > 0)
                ubi.FechaModificacion = DateTime.Now;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateUpdateUbicacionDto create)
        {
            try
            {
                var ubiDb = context.Ubicacion
                                   .FirstOrDefault(c => c.Id == id);
                if (ubiDb != null)
                {
                    var ubicacion = mapper.Map<CreateUpdateUbicacionDto, Ubicacion>(create, ubiDb);
                    HidrataPropFaltante(create, ubicacion);
                    var aa = await context.SaveChangesAsync();
                    return Ok(aa);
                }
                return NotFound($"No se encontró la Ubicación con el Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
