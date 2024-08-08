using AutoMapper;
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

        async Task<ActionResult<List<CUAsignacionVehicTranspDto>>> GetAsignaciones()
        {
            logger.LogInformation("Lista de asignaciones");
            var listaFinal = new List<CUAsignacionVehicTranspDto>();
            var listAsignacion = await context.AsignacionVehicTransp
                                  .Include("Vehiculo")
                                  .Include("Transportista")
                                  .ToListAsync();
            foreach (var asignacion in listAsignacion)
            {
                var asign = new CUAsignacionVehicTranspDto
                {
                    Id = asignacion.Id,
                    Patente = asignacion.Vehiculo.Patente,
                    Modelo = asignacion.Vehiculo.Modelo,
                    NombreTransportista = asignacion.Transportista.Nombre,
                    ApellidoTransportista = asignacion.Transportista.Apellido,
                    Licencia = asignacion.Transportista.Licencia,
                    TransportitaId = asignacion.TransportitaId,
                    VehiculoId = asignacion.VehiculoId
                };
                listaFinal.Add(asign);
            }
            return listaFinal;
        }

        void HidrataPropFaltante(AsignacionVehicTransp asig)
        {
            var transp = context.Transportista.FirstOrDefault(t => t.Id == asig.TransportitaId);
            var vehiculo = context.Vehiculo.FirstOrDefault(v => v.Id == asig.VehiculoId);
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

        [HttpDelete()]
        public async Task<ActionResult> DeleteAsigRango([FromBody] List<DeleteAsignacionVehicTranspDto> lista)
        {
            var cantidadRegistros = lista.Count;
            var contador = 0;
            foreach (var item in lista)
            {
                var asig = await context.AsignacionVehicTransp
                                        .FirstOrDefaultAsync(u => u.Id == item.Id);
                contador++;
                if (asig != null)
                {
                    context.AsignacionVehicTransp.Remove(asig);
                    if (contador == cantidadRegistros)
                    {
                        context.SaveChanges();
                        return Ok($"Se eliminó la vinculación de Vehículo y Transportista.");
                    }
                }
            }
            return NotFound($"No se pudo borrar el rango de vinculaciones de Vehículo y Transportista.");
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
        public async Task<ActionResult<List<CUAsignacionVehicTranspDto>>> GetListAsigDto()
        {
            return await GetAsignaciones();
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<CUAsignacionVehicTranspDto>>> GetListAsigDtoTodos()
        {
            return await GetAsignaciones();
        }

        [HttpPost("crear")]
        public async Task<ActionResult> PostVehiculoDto([FromBody] AsignacionVehicTranspCreacionDto create)
        {
            try
            {
                var asig = mapper.Map<AsignacionVehicTransp>(create);
                HidrataPropFaltante(asig);
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
        public async Task<ActionResult> Put(int id, [FromBody] AsignacionVehicTranspCreacionDto create)
        {
            try
            {
                var asignDb = context.AsignacionVehicTransp.FirstOrDefault(c => c.Id == id);
                var asign = mapper.Map<AsignacionVehicTranspCreacionDto, AsignacionVehicTransp>(create, asignDb);
                HidrataPropFaltante(asign);
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

