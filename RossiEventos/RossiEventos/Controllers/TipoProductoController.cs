using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;

namespace RossiEventos.Controllers
{
    [Route("api/tipoProducto")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private readonly ILogger<TipoProductoController> logger;
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public TipoProductoController(ILogger<TipoProductoController> logger,
                                      AppDbContext context,
                                      IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoProductoDto>> GetTipoProductoDto(int id)
        {
            logger.LogInformation("Obtiene un tipo de producto");
            var tipoProducto = await context.TipoProducto
                                            .FirstOrDefaultAsync(u => u.Id == id);
            if (tipoProducto != null)
                return mapper.Map<TipoProductoDto>(tipoProducto);
            return NotFound($"No se encontró el tipo de producto con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<TipoProductoDto>>> GetListTipoProductoDto()
        {
            logger.LogInformation("Lista de tipo de productos");
            var listTipoProducto = await context.TipoProducto
                                                .ToListAsync();
            return mapper.Map<List<TipoProductoDto>>(listTipoProducto);
        }

        [HttpPost()]
        public async Task<ActionResult> PostTipoProductoDto([FromBody] TipoProductoDto tipoProductoDto)
        {
            try
            {
                var tipoProducto = mapper.Map<TipoProductoDto>(tipoProductoDto);
                context.Add(tipoProducto);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTipoProduto(int id)
        {
            var tipoProducto = await context.TipoProducto
                                            .FirstOrDefaultAsync(u => u.Id == id);
            if (tipoProducto != null)
            {
                context.TipoProducto.Remove(tipoProducto);
                var aa = context.SaveChanges();
                return Ok($"Se eliminó OK el tipo de producto " +
                          $"{tipoProducto.Nombre}");
            }
            return NotFound($"No se encontró el Tipo de Producto con el Id: {id}");
        }
    }
}
