﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RossiEventos.Dto;
using RossiEventos.Entidades;

namespace RossiEventos.Controllers
{
    [Route("api/calidad")]
    [ApiController]
    public class CalidadController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<CalidadController> logger;
        private readonly IMapper mapper;

        public CalidadController(ILogger<CalidadController> logger,
                                 AppDbContext context,
                                 IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCalidad(int id)
        {
            var calidad = await context.Calidad
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (calidad != null)
            {
                context.Calidad.Remove(calidad);
                var aa = context.SaveChanges();
                return Ok($"Se eliminó OK el tipo de CALIDAD " +
                          $"{calidad.Nombre}");
            }
            return NotFound($"No se encontró el tipo de CALIDAD con el Id: {id}");
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CalidadDto>> GetCalidadDto(int id)
        {
            logger.LogInformation("Obtiene un tipo de calidad");
            var calidad = await context.Calidad
                                       .FirstOrDefaultAsync(u => u.Id == id);
            if (calidad != null)
                return mapper.Map<CalidadDto>(calidad);
            return NotFound($"No se encontró el tipo de calidad con el Id: {id}");
        }

        [HttpGet()]
        public async Task<ActionResult<List<CalidadDto>>> GetListCalidadDto()
        {
            return await GetListaCalidad();
        }

        async Task<ActionResult<List<CalidadDto>>> GetListaCalidad()
        {
            logger.LogInformation("Lista de calidades");
            var list = await context.Calidad
                                    .ToListAsync();
            return mapper.Map<List<CalidadDto>>(list);
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<CalidadDto>>> GetListCalidadDtoTodos()
        {
            return await GetListaCalidad();
        }

        [HttpPost("crear")]
        public async Task<ActionResult> PostCalidadDto([FromBody] CUCalidadDto calidadDto)
        {
            try
            {
                var calidad = mapper.Map<Calidad>(calidadDto);
                context.Add(calidad);
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCalidadDto(int id, [FromBody] CUCalidadDto create)
        {
            try
            {
                var calidadDb = context.Calidad.FirstOrDefault(c => c.Id == id);
                var calidad = mapper.Map<CUCalidadDto, Calidad>(create, calidadDb);
                calidad.FechaModificacion = DateTime.Now;
                var aa = await context.SaveChangesAsync();
                return Ok(aa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
