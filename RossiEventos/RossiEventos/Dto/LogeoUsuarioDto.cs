using System.ComponentModel.DataAnnotations;

namespace RossiEventos.Dto
{
    public class LogeoUsuarioDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
