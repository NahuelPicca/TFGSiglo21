using PeliculasAPI.Dto;

namespace PeliculasAPI.Utilidades
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable,
                                               PaginacionDTO paginacionDto)
        {
           return queryable.Skip((paginacionDto.PagActual - 1) * paginacionDto.RecordsPorPagina)
                           .Take(paginacionDto.RecordsPorPagina);
        }
    }
}
