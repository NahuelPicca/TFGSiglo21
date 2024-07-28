using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/tipoProducto")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<TipoProductoController> logger;
        private readonly IMapper mapper;

        public TipoProductoController(ILogger<TipoProductoController> logger,
                                      AppDbContext context,
                                      IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        void HidrataPropFaltante(CUTipoDeProductoDto cTipoDto, TipoProducto tipoDto)
        {
            tipoDto.Categoria = context.Categoria
                                       .FirstOrDefault(c => c.Id == cTipoDto.CategoriaId);
            if (tipoDto.Id > 0)
                tipoDto.FechaModificacion = DateTime.Now;
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteTipoProducto([FromBody] List<DeleteTipoProductoDto> lista)
        {
            var cantidadRegistros = lista.Count;
            var contador = 0;
            foreach (var item in lista)
            {
                var tipo = await context.TipoProducto
                                        .FirstOrDefaultAsync(u => u.Id == item.Id);
                contador++;
                if (tipo != null)
                {
                    context.TipoProducto.Remove(tipo);
                    if (contador == cantidadRegistros)
                    {
                        context.SaveChanges();
                        return Ok($"Se eliminó el rango de tipo de productos seleccionadas.");
                    }
                }
            }
            return NotFound($"No se pudo borrar el rango de tipo de productos.");
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

        [HttpGet("todos")]
        public async Task<ActionResult<List<TipoProductoDto>>> GetListTodosTipoProductoDto()
        {
            return await GetTipoProducto();
        }

        async Task<ActionResult<List<TipoProductoDto>>> GetTipoProducto()
        {
            logger.LogInformation("Lista de tipo de productos");
            var listTipoProducto = await context.TipoProducto
                                                .ToListAsync();
            return mapper.Map<List<TipoProductoDto>>(listTipoProducto);
        }

        [HttpGet()]
        public async Task<ActionResult<List<TipoProductoDto>>> GetListTipoProductoDto()
        {
            return await GetTipoProducto();
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

        [HttpPost("crear")]
        public async Task<ActionResult> PostTipoProductoDto([FromBody] CUTipoDeProductoDto tipoProductoDto)
        {
            try
            {
                var tipoProducto = mapper.Map<TipoProducto>(tipoProductoDto);
                HidrataPropFaltante(tipoProductoDto, tipoProducto);
                context.Add(tipoProducto);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutTipoProductoDto(int id
                                                        , [FromBody] CUTipoDeProductoDto tipoProductoDto)
        {
            try
            {
                var tipoDb = context.TipoProducto.FirstOrDefault(c => c.Id == id);
                var tipo = mapper.Map<CUTipoDeProductoDto, TipoProducto>(tipoProductoDto, tipoDb);
                HidrataPropFaltante(tipoProductoDto, tipo);
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
