using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> logger;
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ProductoController(ILogger<ProductoController> logger,
                                 AppDbContext context,
                                 IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDto>> GetProductoDto(int id)
        {
            logger.LogInformation("Obtiene un producto");
            var producto = await context.Producto
                                        .Include(c => c.Calidad)
                                        .Include(c => c.Tipo)
                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (producto != null)
                return mapper.Map<ProductoDto>(producto);
            return NotFound($"No se encontró el producto con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<ProductoDto>>> GetListProductosDto()
        {
            logger.LogInformation("Lista de productos");
            var list = await context.Producto
                                    .Include(c => c.Calidad)
                                    .Include(c => c.Tipo)
                                    .ToListAsync();
            return mapper.Map<List<ProductoDto>>(list);
        }

        [HttpPost()]
        public async Task<ActionResult> PostProductoDto([FromBody] ProductoDto productoDto)
        {
            try
            {
                var calidad = context.Calidad.FirstOrDefault(c => c.Id == productoDto.CalidadId);
                var tipo = context.TipoProducto.FirstOrDefault(c => c.Id == productoDto.TipoId);
                productoDto.Calidad = calidad;
                productoDto.Tipo = tipo;
                var producto = mapper.Map<ProductoDto>(productoDto);
                context.Add(producto);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        //async void SeteaObjetosFaltantes(ProductoDto productoDto)
        //{
  
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            var producto = await context.Producto
                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (producto != null)
            {
                context.Producto.Remove(producto);
                var aa = context.SaveChanges();
                return Ok($"Se eliminó OK el producto con el código " +
                          $"{producto.Codigo} {producto.Marca}");
            }
            return NotFound($"No se encontró el producto con el Id: {id}");
        }
    }
}
