using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/vehiculos")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<VehiculoController> logger;
        private readonly IMapper mapper;

        public VehiculoController(ILogger<VehiculoController> logger,
                                  AppDbContext context,
                                  IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        static void HidrataPropiedadesFaltantes(Vehiculo vehiculo)
        {
            if (vehiculo.Id > 0)
                vehiculo.FechaModificacion = DateTime.Now;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteVehiculo(int id)
        {
            var vehiculo = await context.Vehiculo
                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (vehiculo != null)
            {
                context.Vehiculo.Remove(vehiculo);
                var cambios = context.SaveChanges();
                return Ok($"Se eliminó OK el vehiculo " +
                          $"de la Marca {vehiculo.Marca + " - Patente " + vehiculo.Patente}");
            }
            return NotFound($"No se encontró el Vehiculo con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<VehiculoDto>>> GetListVehiculoDto()
        {
            logger.LogInformation("Lista de vehiculo");
            var listVehiculo = await context.Vehiculo.ToListAsync();
            return mapper.Map<List<VehiculoDto>>(listVehiculo);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehiculoDto>> GetVehiculoDto(int id)
        {
            logger.LogInformation("Obtiene un vehiculo");
            var vehiculo = await context.Vehiculo
                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (vehiculo != null)
                return mapper.Map<VehiculoDto>(vehiculo);
            return NotFound($"No se encontró el vehiculo con el Id: {id}");
        }

        [HttpPost()]
        public async Task<ActionResult> PostVehiculoDto([FromBody] CUVehiculoDto vehiculoDto)
        {
            try
            {
                var vehiculo = mapper.Map<Vehiculo>(vehiculoDto);
                context.Add(vehiculo);
                var cambios = await context.SaveChangesAsync();
                return Ok(cambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CUVehiculoDto create)
        {
            try
            {
                var vehiculoDb = context.Vehiculo.FirstOrDefault(c => c.Id == id);
                var vehiculo = mapper.Map<CUVehiculoDto, Vehiculo>(create, vehiculoDb);
                HidrataPropiedadesFaltantes(vehiculo);
                var cambios = await context.SaveChangesAsync();
                return Ok(cambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
