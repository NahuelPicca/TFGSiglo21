﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/seguimiento")]
    [ApiController]
    public class SeguimientoPedidoController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<SeguimientoPedidoController> logger;
        private readonly IMapper mapper;

        public SeguimientoPedidoController(ILogger<SeguimientoPedidoController> logger,
                                           AppDbContext context,
                                           IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        async Task<List<SeguimientoPedido>> GetListSeguimiento()
        {
            return await context.SeguimientoPedido
                                .Include(r => r.Pedido)
                                .Include(r => r.ComprobanteEntre)
                                .Include(r => r.Localizaciones)
                                .ToListAsync();
        }

        async Task<SeguimientoPedido> GetSeguimieto(int id)
        {
            var lista = await GetListSeguimiento();
            return lista.FirstOrDefault(t => t.Id == id);
        }

        [HttpGet()]
        public async Task<ActionResult<List<SeguimientoPedidoDto>>> GetListSeguimientoPedDto()
        {
            logger.LogInformation("Lista de seguimientos");
            var listSeguimiento = await GetListSeguimiento();
            return mapper.Map<List<SeguimientoPedidoDto>>(listSeguimiento);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SeguimientoPedidoDto>> GetSeguimientoPedDto(int id)
        {
            logger.LogInformation("Obtiene un Seguimiento");
            var seguimiento = await GetSeguimieto(id);
            if (seguimiento != null)
                return mapper.Map<SeguimientoPedidoDto>(seguimiento);
            return NotFound($"No se encontró el Seguimiento con el Id: {id}");
        }


        [HttpPost()]
        public async Task<ActionResult> PostSeguimientoDto([FromBody] CUSeguimientoPedidoDto create)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var seg = mapper.Map<SeguimientoPedido>(create);
                HidrataPropFaltante(create, seg);
                context.Add(seg);
                var change = await context.SaveChangesAsync();
                await context.Database.CommitTransactionAsync();
                return Ok(change);
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }

        void HidrataPropFaltante(CUSeguimientoPedidoDto create
                               , SeguimientoPedido seguimiento)
        {
            if (seguimiento.Id > 0)
                seguimiento.FechaModificacion = DateTime.Now;

            foreach (var localiz in seguimiento.Localizaciones)
                localiz.Seguimiento = seguimiento;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CUSeguimientoPedidoDto create)
        {
            try
            {
                var seguimiento = await GetSeguimieto(id);
                if (seguimiento != null)
                {
                    var seg = mapper.Map<CUSeguimientoPedidoDto, SeguimientoPedido>(create, seguimiento);
                    HidrataPropFaltante(create, seg);
                    var aa = await context.SaveChangesAsync();
                    return Ok(aa);
                }
                return NotFound($"No se encontró el Seguimiento con el Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        void RemoveObject(SeguimientoPedido seg)
        {
            //Limpia los renglones y el encabezado del contexto.
            context.SeguimientoPedido.Remove(seg);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSeguimiento(int id)
        {
            try
            {
                await context.Database.BeginTransactionAsync();
                var seg = await GetSeguimieto(id);
                if (seg != null)
                {
                    var mensaje = $"Se eliminó OK el Seguimiento " +
                                  $"Id {seg.Id}" +
                                  $"con sus renglones localizaciones correspondientes.";
                    RemoveObject(seg);
                    context.SaveChanges();
                    await context.Database.CommitTransactionAsync();
                    return Ok(mensaje);
                }
                return NotFound($"No se encontró el Seguimiento con el Id: {id}");
            }
            catch (Exception ex)
            {
                await context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
