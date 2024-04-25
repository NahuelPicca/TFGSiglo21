using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace PeliculasAPI.Utilidades
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nomPropiedad = bindingContext.ModelName;
            var valor = bindingContext.ValueProvider.GetValue(nomPropiedad);
            if (valor == ValueProviderResult.None)
                return Task.CompletedTask;

            try
            {
                var valDeserealizado = JsonConvert.DeserializeObject<T>(valor.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valDeserealizado);
            }
            catch (Exception)
            {
                bindingContext.ModelState.TryAddModelError(nomPropiedad, "El valor dado no es del tipo adecuado");
            }
            return Task.CompletedTask;
        }
    }
}
