using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;

namespace RossiEventos.Controllers
{
    [Route("api/titulares")]
    [ApiController]
    public class TitularController : ControllerBase
    {
        private readonly ILogger<TitularController> logger;
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public TitularController(ILogger<TitularController> logger,
                                AppDbContext context,
                                IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TitularDto>> GetTitularDto(int id)
        {
            logger.LogInformation("Obtiene un titular");
            var titular = await context.Transportista
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (titular != null)
                return mapper.Map<TitularDto>(titular);
            return NotFound($"No se encontró el titular con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<TitularDto>>> GetListTitularesDto()
        {
            logger.LogInformation("Lista de titulares");
            var listTitular = await context.Transportista.ToListAsync();
            return mapper.Map<List<TitularDto>>(listTitular);
        }

        [HttpPost()]
        public async Task<ActionResult> PostTitularDto([FromBody] TitularDto titularDto)
        {
            try
            {
                var titular = mapper.Map<TitularDto>(titularDto);
                context.Add(titular);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTitular(int id)
        {
            var titular = await context.Transportista
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (titular != null)
            {
                context.Transportista.Remove(titular);
                var aa = context.SaveChanges();
                return Ok($"Se eliminó OK el titular " +
                          $"{titular.Nombre + " " + titular.Apellido + " " + titular.Cuit}");
            }
            return NotFound($"No se encontró el Titular con el Id: {id}");
        }

    }
}
