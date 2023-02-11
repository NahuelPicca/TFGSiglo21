using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> logger;
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public UsuarioController(ILogger<UsuarioController> logger,
                                AppDbContext context,
                                IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            logger.LogInformation("Get usuario");
            var usuario = await context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario != null)
                return mapper.Map<UsuarioDto>(usuario);
            return NotFound($"No se encontró el usuario con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<UsuarioDto>>> GetListUsuario()
        {
            logger.LogInformation("Lista de usuarios");
            var listUsuario = await context.Usuario.ToListAsync();
            return mapper.Map<List<UsuarioDto>>(listUsuario);
        }

        [HttpPost()]
        public async Task<ActionResult> PostUsuarioDto([FromBody] CreateUpdateUsuarioDto usuarioDto)
        {
            try
            {
                var usuario = mapper.Map<Usuario>(usuarioDto);
                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Contraseña);
                context.Add(usuario);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("id:int")]
        public async Task<ActionResult> PostUsuarioDto(int id, [FromBody] CreateUpdateUsuarioDto create)
        {
            try
            {
                var usuarioDb = context.Usuario.FirstOrDefault(p => p.Id == id);
                var usuario = mapper.Map<CreateUpdateUsuarioDto, Usuario>(create, usuarioDb);
                usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
                usuario.FechaModificacion = DateTime.Now;
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }


        //EXAMPLEEEEEEEEE
        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> PutProductoDto(int id, [FromBody] CreateUpdateProductoDto create)
        //{
        //    try
        //    {
        //        var productDb = context.Producto.FirstOrDefault(p => p.Id == id);

        //        var producto = mapper.Map<CreateUpdateProductoDto, Producto>(create, productDb);
        //        HidrataPropFaltante(create, producto);
        //        //context.Entry = EntityState.Modified;
        //        var aa = await context.SaveChangesAsync();
        //        return Ok(aa);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.InnerException.Message);
        //    }
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var usuario = await context.Usuario
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (usuario != null)
            {
                context.Usuario.Remove(usuario);
                var aa = context.SaveChanges();
                return Ok($"Se eliminó OK el usuario {usuario.Nombre + " " + usuario.Apellido + " " + usuario.Cuit}");
            }
            return NotFound($"No se encontró el Usuario con el Id: {id}");
        }



    }
}
