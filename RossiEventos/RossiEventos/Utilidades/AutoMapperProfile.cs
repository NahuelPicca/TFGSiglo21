using AutoMapper;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Titular, TitularDto>().ReverseMap();
            //CreateMap<GeneroCreacionDto, Genero>();
        }
    }
}
