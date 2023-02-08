using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/calidad")]
    [ApiController]
    public class CalidadController : ControllerBase
    {
        private readonly ILogger<CalidadController> logger;
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public CalidadController(ILogger<CalidadController> logger,
                                 AppDbContext context,
                                 IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CalidadDto>> GetCalidadDto(int id)
        {
            logger.LogInformation("Obtiene un tipo de calidad");
            var calidad = await context.Calidad
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (calidad != null)
                return mapper.Map<CalidadDto>(calidad);
            return NotFound($"No se encontró el tipo de calidad con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<CalidadDto>>> GetListCalidadDto()
        {
            logger.LogInformation("Lista de calidades");
            var list = await context.Calidad
                                    .ToListAsync();
            return mapper.Map<List<CalidadDto>>(list);
        }

        [HttpPost()]
        public async Task<ActionResult> PostCalidadDto([FromBody] CreateCalidadDto calidadDto)
        {
            try
            {
                var calidad = mapper.Map<Calidad>(calidadDto);
                HidrataPropFaltantes(calidad);
                context.Add(calidad);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        private void HidrataPropFaltantes(Calidad calidad)
        {
            calidad.FechaInsercion = DateTime.Now;
            calidad.FechaModificacion = DateTime.Now;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCalidad(int id)
        {
            var calidad = await context.Calidad
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (calidad != null)
            {
                context.Calidad.Remove(calidad);
                var aa = context.SaveChanges();
                return Ok($"Se eliminó OK el tipo de CALIDAD " +
                          $"{calidad.Nombre}");
            }
            return NotFound($"No se encontró el tipo de CALIDAD con el Id: {id}");
        }
    }
}
