﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace PeliculasAPI.Filtros
{
    public class FiltroDeExcepcion : ExceptionFilterAttribute
    {
        readonly ILogger<FiltroDeExcepcion> logger;
        
        public FiltroDeExcepcion(ILogger<FiltroDeExcepcion> logger) {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}
