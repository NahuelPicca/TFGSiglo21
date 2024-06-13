using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<CategoriaController> logger;
        private readonly IMapper mapper;

        public CategoriaController(ILogger<CategoriaController> logger,
                                   AppDbContext context,
                                   IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategoria(int id)
        {
            var categoria = await context.Categoria
                                       .FirstOrDefaultAsync(u => u.Id == id);
            try
            {
                if (categoria != null)
                {
                    context.Categoria.Remove(categoria);
                    var aa = context.SaveChanges();
                    return Ok($"Se eliminó OK la categoria " +
                              $"{categoria.Nombre}");
                }
                return NotFound($"No se encontró la Categoria con el Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDto>> GetCategoriaDto(int id)
        {
            logger.LogInformation("Obtiene un categoria");
            var categoria = await context.Categoria
                                         .FirstOrDefaultAsync(u => u.Id == id);
            if (categoria != null)
                return mapper.Map<CategoriaDto>(categoria);
            return NotFound($"No se encontró la categoria con el Id: {id}");
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<CategoriaDto>>> GetListCategoriaDtoTodos()
        {
            return await GetListaCategoria();
        }

        async Task<ActionResult<List<CategoriaDto>>> GetListaCategoria()
        {
            logger.LogInformation("Lista de categoria");
            var listCategoria = await context.Categoria.ToListAsync();
            return mapper.Map<List<CategoriaDto>>(listCategoria);
        }

        [HttpGet()]
        public async Task<ActionResult<List<CategoriaDto>>> GetListCategoriaDto()
        {
            return await GetListaCategoria();
        }

        [HttpPost("crear")]
        public async Task<ActionResult> PostCategoriaDto([FromBody] CUCategoriaDto categoriaDto)
        {
            try
            {
                var categoria = mapper.Map<Categoria>(categoriaDto);
                context.Add(categoria);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCategoriaDto(int id, [FromBody] CUCategoriaDto create)
        {
            try
            {
                var categoriaDb = context.Categoria.FirstOrDefault(c => c.Id == id);
                var categoria = mapper.Map<CUCategoriaDto, Categoria>(create, categoriaDb);
                categoria.FechaModificacion = DateTime.Now;
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
