using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Entidades;
using RossiEventos.Dto;

namespace RossiEventos.Controllers
{
    [Route("api/asignacionVehicTransp")]
    [ApiController]
    public class AsignacionVehicTranspController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<AsignacionVehicTransp> logger;
        private readonly IMapper mapper;

        public AsignacionVehicTranspController(ILogger<AsignacionVehicTransp> logger,
                                               AppDbContext context,
                                               IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        void HidrataPropFaltante(CUAsignacionVehicTranspDto asigDto, AsignacionVehicTransp asig)
        {
            var transp = context.Transportista.FirstOrDefault(t => t.Id == asigDto.TransportitaId);
            var vehiculo = context.Vehiculo.FirstOrDefault(v => v.Id == asigDto.VehiculoId);
            asig.Transportista = transp;
            asig.Vehiculo = vehiculo;
            if (asig.Id > 0)
                asig.FechaModificacion = DateTime.Now;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsig(int id)
        {
            var asignacion = await context.AsignacionVehicTransp
                                          .Include("Vehiculo")
                                          .Include("Transportista")
                                          .FirstOrDefaultAsync(u => u.Id == id);
            if (asignacion != null)
            {
                var mensaje = $"Se eliminó OK la asignación entre " +
              $"el Transportista {asignacion.Transportista.Nombre} {asignacion.Transportista.Apellido}" +
              $"y el Vehiculo {asignacion.Vehiculo.Marca} Modelo {asignacion.Vehiculo.Modelo}";
                context.AsignacionVehicTransp.Remove(asignacion);
                context.SaveChanges();
                return Ok(mensaje);
            }
            return NotFound($"No se encontró el Vehiculo con el Id: {id}");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AsignacionVehicTranspDto>> GetAsigDto(int id)
        {
            logger.LogInformation("Obtiene una asignacion");
            var asignacion = await context.AsignacionVehicTransp
                                          .Include("Vehiculo")
                                          .Include("Transportista")
                                          .FirstOrDefaultAsync(u => u.Id == id);
            if (asignacion != null)
                return mapper.Map<AsignacionVehicTranspDto>(asignacion);
            return NotFound($"No se encontró la asignacion con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<AsignacionVehicTranspDto>>> GetListAsigDto()
        {
            logger.LogInformation("Lista de asignaciones");
            var listAsignacion = await context.AsignacionVehicTransp
                                              .Include("Vehiculo")
                                              .Include("Transportista")
                                              .ToListAsync();
            return mapper.Map<List<AsignacionVehicTranspDto>>(listAsignacion);
        }

        [HttpPost()]
        public async Task<ActionResult> PostVehiculoDto([FromBody] CUAsignacionVehicTranspDto create)
        {
            try
            {
                var asig = mapper.Map<AsignacionVehicTransp>(create);
                HidrataPropFaltante(create, asig);
                context.Add(asig);
                var cambios = await context.SaveChangesAsync();
                return Ok(cambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CUAsignacionVehicTranspDto create)
        {
            try
            {
                var asignDb = context.AsignacionVehicTransp.FirstOrDefault(c => c.Id == id);
                var asign = mapper.Map<CUAsignacionVehicTranspDto, AsignacionVehicTransp>(create, asignDb);
                HidrataPropFaltante(create, asign);
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

