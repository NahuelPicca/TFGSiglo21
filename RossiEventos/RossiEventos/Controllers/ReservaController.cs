using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/reserva")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<ReservaController> logger;
        private readonly IMapper mapper;

        public ReservaController(ILogger<ReservaController> logger,
                                 AppDbContext context,
                                 IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<ReservaDto>>> GetListaResrvaDto()
        {
            logger.LogInformation("Lista de reservas");
            var listReserva = await GetListReserva();
            return mapper.Map<List<ReservaDto>>(listReserva);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReservaDto>> GetReservaDto(int id)
        {
            logger.LogInformation("Obtiene una Reserva");
            var listReserva = await GetListReserva();
            var reserva = listReserva.FirstOrDefault(r=>r.Id == id);
            if (reserva != null)
                return mapper.Map<ReservaDto>(reserva);
            return NotFound($"No se encontró la Reserva con el Id: {id}");
        }

        async Task<List<Reserva>> GetListReserva()
        {
            return await context.Reservas
                                .Include("Renglones")
                                .ToListAsync();
        }

    }
}
