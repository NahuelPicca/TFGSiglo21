using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/deposito")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<Deposito> logger;
        private readonly IMapper mapper;

        public DepositoController(ILogger<Deposito> logger,
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
            var deposito = await context.Deposito
                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (deposito != null)
            {
                var mensaje = $"Se eliminó OK el Depósito " +
                              $"{deposito.Codigo} {deposito.Descripcion}";
                context.Deposito.Remove(deposito);
                context.SaveChanges();
                return Ok(mensaje);
            }
            return NotFound($"No se encontró el Depósito con el Id: {id}");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepositoDto>> GetDepositoDto(int id)
        {
            logger.LogInformation("Obtiene un Depósito");
            var deposito = await context.Deposito
                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (deposito != null)
                return mapper.Map<DepositoDto>(deposito);
            return NotFound($"No se encontró el Depósito con el Id: {id}");
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<DepositoDto>>> GetListDepDtoTodos()
        {
            return await GetListaDeposito();
        }

        [HttpGet()]
        public async Task<ActionResult<List<DepositoDto>>> GetListDepDto()
        {
            return await GetListaDeposito();
        }

        async Task<ActionResult<List<DepositoDto>>> GetListaDeposito()
        {
            logger.LogInformation("Lista de depositos");
            var listDepositos = await context.Deposito
                                              .ToListAsync();
            return mapper.Map<List<DepositoDto>>(listDepositos);
        }

        [HttpPost("crear")]
        public async Task<ActionResult> PostDepositoDto([FromBody] CUDepositoDto create)
        {
            try
            {
                var dep = mapper.Map<Deposito>(create);
                HidrataPropFaltante(create, dep);
                context.Add(dep);
                var cambios = await context.SaveChangesAsync();
                return Ok(cambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        void HidrataPropFaltante(CUDepositoDto create, Deposito dep)
        {
            if (dep.Id > 0)
                dep.FechaModificacion = DateTime.Now;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CUDepositoDto create)
        {
            try
            {
                var depDb = context.Deposito.FirstOrDefault(c => c.Id == id);
                if (depDb != null)
                {
                    var deposito = mapper.Map<CUDepositoDto, Deposito>(create, depDb);
                    HidrataPropFaltante(create, deposito);
                    var aa = await context.SaveChangesAsync();
                    return Ok(aa);
                }
                return NotFound($"No se encontró el Depósito con el Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
