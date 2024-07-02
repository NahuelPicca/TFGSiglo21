using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;
using RossiEventos.Utilidades;

namespace RossiEventos.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        readonly AppDbContext context;
        readonly ILogger<ProductoController> logger;
        readonly IMapper mapper;
        readonly IAlmacenadorArchivos almacenamietoArchivos;
        readonly string contenedor = "productos";

        public ProductoController(ILogger<ProductoController> logger,
                                 AppDbContext context,
                                 IMapper mapper,
                                 IAlmacenadorArchivos almacenamietoArchivos)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.almacenamietoArchivos = almacenamietoArchivos;
        }

        async void HidrataPropFaltante(CUProductoDto productoDto
                                     , Producto producto
                                     , bool modifica = false)
        {
            var calidad = context.Calidad.FirstOrDefault(c => c.Id == productoDto.CalidadId);
            var tipo = context.TipoProducto.FirstOrDefault(c => c.Id == productoDto.TipoProductoId);
            producto.Calidad = calidad;
            producto.Tipo = tipo;
            producto.TipoProductoId = productoDto.TipoProductoId;
            producto.Poster1 = !string.IsNullOrEmpty(producto.Poster1) ? producto.Poster1 : string.Empty;
            producto.Poster2 = !string.IsNullOrEmpty(producto.Poster2) ? producto.Poster2 : string.Empty;
            producto.Poster3 = !string.IsNullOrEmpty(producto.Poster3) ? producto.Poster3 : string.Empty;

            if (producto.Id > 0)
                producto.FechaModificacion = DateTime.Now;

            if (modifica)
            {
                if (productoDto.Poster1 != null)
                    producto.Poster1 = await almacenamietoArchivos.EditarArchivo(contenedor
                                                                              , productoDto.Poster1
                                                                              , producto.Poster1);
                if (productoDto.Poster2 != null)
                    producto.Poster2 = await almacenamietoArchivos.EditarArchivo(contenedor
                                                                              , productoDto.Poster2
                                                                              , producto.Poster2);
                if (productoDto.Poster3 != null)
                    producto.Poster3 = await almacenamietoArchivos.EditarArchivo(contenedor
                                                                              , productoDto.Poster3
                                                                              , producto.Poster3);
            }
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

        [HttpDelete()]
        public async Task<ActionResult> DeleteProductos([FromBody] List<DeleteProductoDTO> lista)
        {
            var cantidadRegistros = lista.Count;
            var contador = 0;
            foreach (var item in lista)
            {
                var producto = await context.Producto
                                            .FirstOrDefaultAsync(u => u.Id == item.Id);
                contador++;
                if (producto != null)
                {
                    context.Producto.Remove(producto);
                    if (contador == cantidadRegistros)
                    {
                        context.SaveChanges();
                        return Ok($"Se eliminó el rango de productos seleccionado.");
                    }
                }
            }
            return NotFound($"No se pudo borrar el rango de productos.");
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
        public async Task<ActionResult<ProductoEditarDto>> GetProductoDto(int id)
        {
            logger.LogInformation("Obtiene un producto");
            var producto = await context.Producto
                                        .Include(c => c.Calidad)
                                        .Include(c => c.Tipo)
                                        .FirstOrDefaultAsync(u => u.Id == id);
            if (producto != null)
            {
                var proctoEditar = new ProductoEditarDto
                {
                    Id = producto.Id.ToString(),
                    Codigo = producto.Codigo,
                    Descripcion = producto.Descripcion,
                    Marca = producto.Marca,
                    Poster1 = producto.Poster1,
                    Poster2 = producto.Poster2,
                    Poster3 = producto.Poster3,
                    Anio = producto.Anio,
                    Habilitado = producto.Habilitado,
                    CalidadId = producto.CalidadId,
                    CodigoCalidad = producto.Calidad.Codigo,
                    TipoProductoId = producto.TipoProductoId,
                    Precio = producto.Precio
                };
                return proctoEditar;
            }
            //return mapper.Map<ProductoEditarDto>(producto);
            return NotFound($"No se encontró el producto con el Id: {id}");
        }

        [HttpPost("crear")]
        public async Task<ActionResult> PostProductoDto([FromForm] CUProductoDto productoDto)
        {
            try
            {
                var producto = mapper.Map<Producto>(productoDto);
                HidrataPropFaltante(productoDto, producto);
                await HidrataPosts(productoDto, producto);
                context.Add(producto);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        async Task HidrataPosts(CUProductoDto productoDto, Producto producto)
        {
            if (productoDto.Poster1 != null)
                producto.Poster1 = await almacenamietoArchivos.GuardarArchivo(contenedor, productoDto.Poster1);
            if (productoDto.Poster2 != null)
                producto.Poster2 = await almacenamietoArchivos.GuardarArchivo(contenedor, productoDto.Poster2);
            if (productoDto.Poster3 != null)
                producto.Poster3 = await almacenamietoArchivos.GuardarArchivo(contenedor, productoDto.Poster3);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutProductoDto(int id, [FromForm] CUProductoDto create)
        {
            try
            {
                var productDb = context.Producto.FirstOrDefault(p => p.Id == id);

                if (productDb == null)
                    return NotFound();

                var producto = mapper.Map<CUProductoDto, Producto>(create, productDb);
                HidrataPropFaltante(create, producto, true);
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
