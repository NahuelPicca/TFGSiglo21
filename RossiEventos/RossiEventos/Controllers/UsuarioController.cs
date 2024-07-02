using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RossiEventos.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RossiEventos.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        readonly AppDbContext context;
        readonly ILogger<UsuarioController> logger;
        readonly IMapper mapper;
        readonly UserManager<IdentityUser> userManager;
        readonly IConfiguration configuration;
        readonly SignInManager<IdentityUser> singInManager;

        public UsuarioController(ILogger<UsuarioController> logger,
                                AppDbContext context,
                                IMapper mapper,
                                UserManager<IdentityUser> userManeger,
                                IConfiguration configuration,
                                SignInManager<IdentityUser> singInManager)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManeger;
            this.configuration = configuration;
            this.singInManager = singInManager;
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
                return Ok($"Se eliminó OK el usuario {usuario.Nombre + " " + usuario.Apellido + " " + usuario.Cuit}");
            }
            return NotFound($"No se encontró el Usuario con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<UsuarioDto>>> GetListUsuario()
        {
            return await GetUsuariosTodos();
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<UsuarioDto>>> GetListUsuarioTodos()
        {
            return await GetUsuariosTodos();
        }

        async Task<ActionResult<List<UsuarioDto>>> GetUsuariosTodos()
        {
            logger.LogInformation("Lista de usuarios");
            var listUsuario = await context.Usuario.ToListAsync();
            return mapper.Map<List<UsuarioDto>>(listUsuario);
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

        [HttpPost("")]
        public async Task<ActionResult> PostUsuarioDto([FromBody] CUUsuarioDto usuarioDto)
        {
            try
            {
                int aa = await GuardaUsuario(usuarioDto);
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        async Task<int> GuardaUsuario(CUUsuarioDto usuarioDto)
        {
            var usuario = mapper.Map<Usuario>(usuarioDto);
            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Contraseña);
            context.Add(usuario);
            var aa = await context.SaveChangesAsync();
            return aa;
        }

        async Task<int> GuardaUsuario(CredencialesUsuarioDTO credenciales)
        {
            var usuario = mapper.Map<Usuario>(credenciales);
            var usuarioIdentity = await userManager.FindByEmailAsync(credenciales.Email);
            usuario.Contraseña = usuarioIdentity.PasswordHash;
            context.Add(usuario);
            return await context.SaveChangesAsync();
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Crear([FromBody] CredencialesUsuarioDTO credenciales)
        {
            var usuario = new IdentityUser { UserName = credenciales.Email, Email = credenciales.Email };
            var resultado = await userManager.CreateAsync(usuario, credenciales.Contraseña);
            if (resultado.Succeeded)
            {
                var respuesta = await ConstruirToken(credenciales);
                await GuardaUsuario(credenciales);
                return respuesta;
            }
            else
                return BadRequest(resultado.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> Login([FromBody] LogeoUsuarioDto logeo)
        {
            var resultado = await singInManager.PasswordSignInAsync(logeo.Email
                                                                  , logeo.Password
                                                                  , false, false);
            if (resultado.Succeeded)
                return await GetConstruirTokenLogueo(logeo);
            else
                return BadRequest("Login incorrecto");
        }

        async Task<RespuestaAutenticacionDTO> GetConstruirTokenLogueo(LogeoUsuarioDto credenciales)
        {
            return await ConstruirToken(new CredencialesUsuarioDTO
            {
                Email = credenciales.Email,
                Contraseña = credenciales.Password
            });
        }

        async Task<RespuestaAutenticacionDTO> ConstruirToken(CredencialesUsuarioDTO credenciales)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(credenciales.Email), credenciales.Email)
            };

            var usuario = await userManager.FindByEmailAsync(credenciales.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);
            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddYears(1);
            var token = new JwtSecurityToken(issuer: null
                                           , audience: null
                                           , claims: claims
                                           , expires: expiracion
                                           , signingCredentials: creds);
            return new RespuestaAutenticacionDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion
            };
        }

        [HttpPut("id:int")]
        public async Task<ActionResult> PostUsuarioDto(int id, [FromBody] CUUsuarioDto create)
        {
            try
            {
                var usuarioDb = context.Usuario.FirstOrDefault(p => p.Id == id);
                var usuario = mapper.Map<CUUsuarioDto, Usuario>(create, usuarioDb);
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
    }
}
