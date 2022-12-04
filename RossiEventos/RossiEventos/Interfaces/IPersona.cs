namespace RossiEventos.Interfaces
{
    public interface IPersona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDni { get; set; }
        public int NroDni { get; set; }
        public string Cuit { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Email { get; set; }
        public string CodigoPostal { get; set; }
    }
}
