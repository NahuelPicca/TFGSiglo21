using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/titulares")]
    [ApiController]
    public class TitularController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<TitularController> logger;
        private readonly IMapper mapper;

        public TitularController(ILogger<TitularController> logger,
                                AppDbContext context,
                                IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
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

        [HttpDelete()]
        public async Task<ActionResult> DeleteTitulares([FromBody] List<DeleteBaseDto> lista)
        {
            var cantidadRegistros = lista.Count;
            var contador = 0;
            foreach (var item in lista)
            {
                var producto = await context.Titular
                                            .FirstOrDefaultAsync(u => u.Id == item.Id);
                contador++;
                if (producto != null)
                {
                    context.Titular.Remove(producto);
                    if (contador == cantidadRegistros)
                    {
                        context.SaveChanges();
                        return Ok($"Se eliminó el rango de titulares seleccionado.");
                    }
                }
            }
            return NotFound($"No se pudo borrar el rango de titulares.");
        }

        [HttpGet("")]
        public async Task<ActionResult<List<TitularDto>>> GetListTitularesDto()
        {
            return await GetTitulares();
        }

        [HttpGet("combo")]
        public async Task<ActionResult<List<TitularComboDto>>> GetListTitularesTodosDto()
        {
            var listaFinal = new List<TitularComboDto>();
            var lista = await GetTitulares();
            foreach (TitularDto item in lista)
            {
                var titular = new TitularComboDto { Id = item.Id, CuitApellidoNombre = item.Cuit + " " + item.Apellido + " " + item.Nombre};
                listaFinal.Add(titular);
            }
            return listaFinal;
        }
        async Task<List<TitularDto>> GetTitulares()
        {
            logger.LogInformation("Lista de titulares");
            var listTitular = await context.Titular.ToListAsync();
            return mapper.Map<List<TitularDto>>(listTitular);
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

        [HttpPost()]
        public async Task<ActionResult> PostTitularDto([FromBody] CUTitularDto titularDto)
        {
            try
            {
                var titular = mapper.Map<Titular>(titularDto);
                context.Add(titular);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutTitularDto(int id, [FromBody] CUTitularDto create)
        {
            try
            {
                var titularDb = context.Titular.FirstOrDefault(t => t.Id == id);
                var titular = mapper.Map<CUTitularDto, Titular>(create, titularDb);
                titular.FechaModificacion = DateTime.Now;
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
