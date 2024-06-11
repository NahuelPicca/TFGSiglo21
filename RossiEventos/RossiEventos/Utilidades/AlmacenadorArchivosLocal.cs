

namespace RossiEventos.Utilidades
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        readonly IWebHostEnvironment env;
        readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor) 
        {
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
        }

        public Task BorrarArchivo(string ruta
                                , string contenedor)
        {
            if(string.IsNullOrEmpty(ruta))
                return Task.CompletedTask;

            var nomArchivo = Path.GetFileName(ruta);
            var direcArchivo = Path.Combine(env.WebRootPath, contenedor, nomArchivo);
            if (File.Exists(direcArchivo))
                File.Delete(direcArchivo);

            return Task.CompletedTask;
        }

        public async Task<string> EditarArchivo(string contenedor
                                        , IFormFile archivo
                                        , string ruta)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenedor, archivo);
        }

        public async Task<string> GuardarArchivo(string contenedor
                                         , IFormFile archivo)
        {
            var extension = Path.GetExtension(archivo.FileName);
            var nomArchivo = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, contenedor);
            if(!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string ruta = Path.Combine(folder,nomArchivo);
            using (var memoryStream = new MemoryStream())
            {
                await archivo.CopyToAsync(memoryStream);
                var contenido = memoryStream.ToArray();
                await File.WriteAllBytesAsync(ruta, contenido);
            }

            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            return Path.Combine(urlActual, contenedor, nomArchivo).Replace("\\", "/");
        }
    }
}
