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
            logger.LogInformation("Lista de usuarios");
            var usuario = await context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
            return mapper.Map<UsuarioDto>(usuario);
        }

        [HttpGet()]
        public async Task<ActionResult<List<UsuarioDto>>> GetListUsuario()
        {
            logger.LogInformation("Lista de usuarios");
            var listUsuario = await context.Usuario.ToListAsync();
            return mapper.Map<List<UsuarioDto>>(listUsuario);
        }

        //[HttpPost()]
        //public async Task<ActionResult<List<UsuarioDto>>> GetListUsuario(int id)
        //{
        //    logger.LogInformation("Lista de usuarios");
        //    var listUsuario = await context.Usuario.ToListAsync();
        //    return mapper.Map<List<UsuarioDto>>(listUsuario);
        //}
    }
}
