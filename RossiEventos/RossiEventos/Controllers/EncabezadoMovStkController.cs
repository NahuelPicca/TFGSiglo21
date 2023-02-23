using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;
using System.Runtime.Intrinsics.Arm;

namespace RossiEventos.Controllers
{
    [Route("api/encabezado")]
    [ApiController]
    public class EncabezadoMovStkController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<EncabezadoMovStk> logger;
        private readonly IMapper mapper;

        public EncabezadoMovStkController(ILogger<EncabezadoMovStk> logger,
                                          AppDbContext context,
                                          IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        async Task<EncabezadoMovStk> GetMovimiento(int id)
        {
            return await context.EncabezadoMovStk
                                .Include("Renglones")
                                .Include("Renglones.Saldo")
                                .FirstOrDefaultAsync(u => u.Id == id);
        }

        void HidrataPropFaltante(CreateUpdateEncabezadoMovStkDto create
                               , EncabezadoMovStk mov)
        {
            if (mov.Id > 0)
                mov.FechaModificacion = DateTime.Now;

            foreach (var reng in mov.Renglones)
            {
                reng.Producto = context.Producto
                                       .FirstOrDefault(p => p.Id == reng.ProductoId);
                var saldo = context.SaldoUbicacion
                                   .FirstOrDefault(p => p.Id == reng.SaldoUbiId);
                DefineCantidad(create, reng, saldo);
                reng.Saldo = saldo;
                reng.Encabezado = mov;
            }
        }

        void DefineCantidad(CreateUpdateEncabezadoMovStkDto create
                          , RenglonMovStk reng
                          , SaldoUbicacion saldo)
        {
            if (create.TipoMovimiento == TipoComprobante.Ingreso)
                saldo.Cantidad += reng.Cantidad;
            else if (create.TipoMovimiento == TipoComprobante.Egreso)
                saldo.Cantidad -= reng.Cantidad;
            else if (create.TipoMovimiento == TipoComprobante.Ajuste)
                saldo.Cantidad = reng.Cantidad;
        }

        void RestableceCantidad(EncabezadoMovStk enc)
        {
            //Solamente se restablece las cantidades si es un Ing o Egr.
            foreach (var renglon in enc.Renglones)
            {
                if (enc.TipoMovimiento == TipoComprobante.Ingreso)
                    renglon.Saldo.Cantidad -= renglon.Cantidad;
                else if (enc.TipoMovimiento == TipoComprobante.Egreso)
                    renglon.Saldo.Cantidad += renglon.Cantidad;
            }
        }

        void RemoveObject(EncabezadoMovStk encab)
        {
            //Limpia los renglones y el encabezado del contexto.
            foreach (var reg in encab.Renglones)
                context.RenglonMov.Remove(reg);
            context.EncabezadoMovStk.Remove(encab);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMov(int id)
        {
            try
            {
                context.Database.BeginTransactionAsync();
                var encab = await GetMovimiento(id);
                if (encab != null)
                {
                    var mensaje = $"Se eliminó OK el Movimiento " +
                                  $"Id {encab.Id}" +
                                  $"Tipo Movimiento {encab.TipoMovimiento} {encab.ComprobanteRelacionado}" +
                                  $"con sus renglones correspondientes.";
                    RestableceCantidad(encab);
                    RemoveObject(encab);
                    context.SaveChanges();
                    context.Database.CommitTransactionAsync();
                    return Ok(mensaje);
                }
                return NotFound($"No se encontró el Movimiento con el Id: {id}");
            }
            catch (Exception ex)
            {
                context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<List<EncabezadoMovStkDto>>> GetListMovDto()
        {
            logger.LogInformation("Lista de movimientos");
            var listEncab = await context.EncabezadoMovStk
                                         .Include("Renglones")
                                         .ToListAsync();
            return mapper.Map<List<EncabezadoMovStkDto>>(listEncab);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EncabezadoMovStkDto>> GetMovDto(int id)
        {
            logger.LogInformation("Obtiene un Movimiento");
            var encMov = await GetMovimiento(id);
            if (encMov != null)
                return mapper.Map<EncabezadoMovStkDto>(encMov);
            return NotFound($"No se encontró el Movimiento con el Id: {id}");
        }

        [HttpPost()]
        public async Task<ActionResult> PostMovimentoDto([FromBody] CreateUpdateEncabezadoMovStkDto create)
        {
            try
            {
                context.Database.BeginTransactionAsync();
                var mov = mapper.Map<EncabezadoMovStk>(create);
                HidrataPropFaltante(create, mov);
                context.Add(mov);
                var cambios = await context.SaveChangesAsync();
                context.Database.CommitTransactionAsync();
                return Ok(cambios);
            }
            catch (Exception ex)
            {
                context.Database.RollbackTransactionAsync();
                return BadRequest(ex.InnerException.Message);
            }
        }

        //Por ahora lo comento, porque me parece que este evento no debería ir..
        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Put(int id, [FromBody] CreateUpdateEncabezadoMovStkDto create)
        //{
        //    try
        //    {
        //        var movDb = context.EncabezadoMovStk
        //                           .FirstOrDefault(c => c.Id == id);
        //        if (movDb != null)
        //        {
        //            var mov = mapper.Map<CreateUpdateEncabezadoMovStkDto, EncabezadoMovStk>(create, movDb);
        //            HidrataPropFaltante(create, mov);
        //            var aa = await context.SaveChangesAsync();
        //            return Ok(aa);
        //        }
        //        return NotFound($"No se encontró el Movimiento con el Id: {id}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.InnerException.Message);
        //    }
        //}
    }
}
