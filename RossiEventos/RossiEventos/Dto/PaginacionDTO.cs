namespace PeliculasAPI.Dto
{
    public class PaginacionDTO
    {
        private int recordsPorPagina = 10;
        private readonly int cantMaxRecordsPorPag = 50;
        public int PagActual { get; set; } = 1;

        public int RecordsPorPagina
        {
            get { return recordsPorPagina; }
            set
            {
                recordsPorPagina = (value > cantMaxRecordsPorPag) ? cantMaxRecordsPorPag : value;
            }
        }
    }
}
