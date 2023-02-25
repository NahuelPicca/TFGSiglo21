using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/saldo")]
    [ApiController]
    public class SaldoUbiController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<SaldoUbiController> logger;
        private readonly IMapper mapper;

        public SaldoUbiController(ILogger<SaldoUbiController> logger,
                                  AppDbContext context,
                                  IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        async Task<List<SaldoUbicacion>> GetSaldoUbi()
        {
            return await context.SaldoUbicacion
                                .Include("Deposito")
                                .Include("Producto")
                                .Include("Ubicacion")
                                .ToListAsync();
        }

        void HidrataPropFaltante(CreateUpdateSaldoUbiDto saldoDto
                               , SaldoUbicacion saldo)
        {
            saldo.Producto = context.Producto
                                    .FirstOrDefault(p => p.Id == saldoDto.ProductoId);
            saldo.Ubicacion = context.Ubicacion
                                     .FirstOrDefault(p => p.Id == saldoDto.UbicacionId);
            saldo.Deposito = context.Deposito
                                    .FirstOrDefault(p => p.Id == saldoDto.DepositoId);
        }

        [HttpGet()]
        public async Task<ActionResult<List<SaldoUbicacionDto>>> GetListSaldosDto()
        {
            logger.LogInformation("Lista de saldos");
            var listSaldo = await GetSaldoUbi();
            return mapper.Map<List<SaldoUbicacionDto>>(listSaldo);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SaldoUbicacionDto>> GetSaldoDto(int id)
        {
            logger.LogInformation("Obtiene un saldo de una ubicación");
            var listSaldo = await GetSaldoUbi();
            var saldo = listSaldo.FirstOrDefault(s => s.Id == id);
            if (saldo != null)
                return mapper.Map<SaldoUbicacionDto>(saldo);
            return NotFound($"No se encontró el Saldo con el Id: {id}");
        }

        [HttpPost()]
        public async Task<ActionResult> PostProductoDto([FromBody] CreateUpdateSaldoUbiDto saldoDto)
        {
            try
            {
                var saldo = mapper.Map<SaldoUbicacion>(saldoDto);
                HidrataPropFaltante(saldoDto, saldo);
                context.Add(saldo);
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
