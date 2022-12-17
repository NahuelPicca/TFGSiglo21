using AutoMapper;
using RossiEventos.Dto;

namespace RossiEventos.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            //CreateMap<GeneroCreacionDto, Genero>();
        }
    }
}
