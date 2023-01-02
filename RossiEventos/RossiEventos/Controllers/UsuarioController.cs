using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;

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
        public async Task<ActionResult> PostUsuarioDto([FromBody] UsuarioDto usuarioDto)
        {
            var usuario = mapper.Map<Usuario>(usuarioDto);
            context.Add(usuario);
            var aa = await context.SaveChangesAsync();
            return Ok(aa);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var usuario = await context.Usuario
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (usuario != null)
            {
                context.Usuario.Remove(usuario);
                var aa = context.SaveChanges();
                return Ok(aa);
            }
            return NotFound($"No se encontró el Usuario con el Id: {id}");
        }
    }
}
