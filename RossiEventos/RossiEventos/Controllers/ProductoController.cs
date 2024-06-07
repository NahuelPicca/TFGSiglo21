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
        private readonly AppDbContext context;
        private readonly ILogger<ProductoController> logger;
        private readonly IMapper mapper;

        public ProductoController(ILogger<ProductoController> logger,
                                 AppDbContext context,
                                 IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        void HidrataPropFaltante(CUProductoDto productoDto, Producto producto)
        {
            var calidad = context.Calidad.FirstOrDefault(c => c.Id == productoDto.CalidadId);
            var tipo = context.TipoProducto.FirstOrDefault(c => c.Id == productoDto.TipoProductoId);
            producto.Calidad = calidad;
            producto.Tipo = tipo;
            producto.TipoProductoId = productoDto.TipoProductoId;
            if (producto.Id > 0)
                producto.FechaModificacion = DateTime.Now;
        }

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

        [HttpPost("crear")]
        public async Task<ActionResult> PostProductoDto([FromBody] CUProductoDto productoDto)
        {
            try
            {
                var producto = mapper.Map<Producto>(productoDto);
                HidrataPropFaltante(productoDto, producto);
                context.Add(producto);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutProductoDto(int id, [FromBody] CUProductoDto create)
        {
            try
            {
                var productDb = context.Producto.FirstOrDefault(p => p.Id == id);

                var producto = mapper.Map<CUProductoDto, Producto>(create, productDb);
                HidrataPropFaltante(create, producto);
                //context.Entry = EntityState.Modified;
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
