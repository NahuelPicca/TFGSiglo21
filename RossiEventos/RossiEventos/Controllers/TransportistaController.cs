using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/transportista")]
    [ApiController]
    public class TransportistaController : ControllerBase
    {
        private readonly ILogger<TransportistaController> logger;
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public TransportistaController(ILogger<TransportistaController> logger,
                                       AppDbContext context,
                                       IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TransportistaDto>> GetTransportistaDto(int id)
        {
            logger.LogInformation("Obtiene un transportista");
            var transportista = await context.Transportista
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (transportista != null)
                return mapper.Map<TransportistaDto>(transportista);
            return NotFound($"No se encontró el transportista con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<TransportistaDto>>> GetListTransportistaDto()
        {
            logger.LogInformation("Lista de transportistas");
            var listTransportistas = await context.Transportista.ToListAsync();
            return mapper.Map<List<TransportistaDto>>(listTransportistas);
        }

        [HttpPost()]
        public async Task<ActionResult> PostTransportistaDto([FromBody] CreateUpdateTransportistaDto transportistaDto)
        {
            try
            {
                var transportista = mapper.Map<Transportista>(transportistaDto);
                context.Add(transportista);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateUpdateTransportistaDto create)
        {
            try
            {
                var transportistaDb = context.Transportista.FirstOrDefault(c => c.Id == id);
                //var transportista = mapper.Map<Transportista>(transportistaDto);
                var calidad = mapper.Map<CreateUpdateTransportistaDto, Transportista>(create, transportistaDb);
                calidad.FechaModificacion = DateTime.Now;
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> PutCalidadDto(int id, [FromBody] CreateUpdateCalidadDto create)
        //{
        //    try
        //    {
        //        var calidadDb = context.Calidad.FirstOrDefault(c => c.Id == id);
        //        var calidad = mapper.Map<CreateUpdateCalidadDto, Calidad>(create, calidadDb);
        //        calidad.FechaModificacion = DateTime.Now;
        //        var aa = await context.SaveChangesAsync();
        //        return Ok(aa);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.InnerException.Message);
        //    }
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTransportista(int id)
        {
            var transportista = await context.Transportista
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (transportista != null)
            {
                context.Transportista.Remove(transportista);
                var aa = context.SaveChanges();
                return Ok($"Se eliminó OK el transportista " +
                          $"{transportista.Nombre + " " + transportista.Apellido + " " + transportista.Cuit}");
            }
            return NotFound($"No se encontró el Transportista con el Id: {id}");
        }
    }
}
