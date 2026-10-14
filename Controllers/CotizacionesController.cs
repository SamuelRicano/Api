using Microsoft.AspNetCore.Mvc;
using SmartClaims.Api.DTOs;
using SmartClaims.Api.Exceptions;
using SmartClaims.Api.Models;
using SmartClaims.Api.Services;
using SmartClaims.Api.Services.Interfaces;

namespace SmartClaims.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CotizacionesController : ControllerBase
    {
        private readonly ICotizacionesService _cotizacionesService;

        public CotizacionesController(ICotizacionesService cotizacionesService)
        {
            _cotizacionesService = cotizacionesService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCotizaciones()
        {
            var cotizaciones = await _cotizacionesService.ObtenerCotizaciones();

            return Ok(cotizaciones);
        }

        [HttpGet("{numero}")]
        public async Task<IActionResult> ObtenerCotizacion(int numero)
        {
            var cotizacion = await  _cotizacionesService.ObtenerCotizacion(numero);

            if (cotizacion == null)
            {
                return NotFound();
            }

            return Ok(cotizacion);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCotizacion(CrearCotizacionDto dto)
        {

            var cotizacion = new Cotizacion
            {
                Numero = dto.Numero,
                NombreCliente = dto.NombreCliente,
                Prima = dto.Prima
            };

            var nuevaCotizacion = await _cotizacionesService.CrearCotizacion(cotizacion);

            return CreatedAtAction(
                nameof(ObtenerCotizacion),
                new { numero = nuevaCotizacion.Numero },
                nuevaCotizacion
            );

        }

        [HttpPut("{numero}")]
        public async Task<IActionResult> ActualizarCotizacion(int numero, ActualizarCotizacionDto dto)
        {
            var cotizacion = new Cotizacion
            {
                Numero = numero,
                NombreCliente = dto.NombreCliente,
                Prima = dto.Prima
            };

            var actualizado =await _cotizacionesService.ActualizarCotizacion(cotizacion);

            if (!actualizado)
            {
                return NotFound();
            }

            return Ok(cotizacion);
        }

        [HttpGet("error-prueba")]
        public IActionResult ErrorPrueba()
        {
            throw new Exception("Este es un error de prueba.");
        }

        [HttpDelete("{numero}")]
        public async Task<IActionResult> EliminarCotizacion(int numero)
        {
            var eliminado =await _cotizacionesService.EliminarCotizacion(numero);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}