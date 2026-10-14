using SmartClaims.Api.Data.Interfaces;
using SmartClaims.Api.Exceptions;
using SmartClaims.Api.Models;
using SmartClaims.Api.Services.Interfaces;

namespace SmartClaims.Api.Services
{
    public class CotizacionesService: ICotizacionesService
    {
        private readonly ICotizacionesRepository _cotizacionesRepository;

        public CotizacionesService(ICotizacionesRepository cotizacionesRepository)
        {
            _cotizacionesRepository = cotizacionesRepository;
        }

        public async Task<List<Cotizacion>> ObtenerCotizaciones()
        {
            return await _cotizacionesRepository.ObtenerCotizaciones();
        }
        public async Task<Cotizacion> ObtenerCotizacion(int numero)
        {
            return await  _cotizacionesRepository.ObtenerCotizacion(numero);
        }
        public async Task< Cotizacion > CrearCotizacion(Cotizacion cotizacion)
        {
            var existe = await _cotizacionesRepository.ExisteCotizacion(cotizacion.Numero);

            if (existe)
            {
                throw new CotizacionDuplicadaException(cotizacion.Numero);
            }

            return await _cotizacionesRepository.CrearCotizacion(cotizacion);

        }
        public async Task<bool> ActualizarCotizacion(Cotizacion cotizacion)
        {
            return await _cotizacionesRepository.ActualizarCotizacion(cotizacion);
        }
        public async Task<bool> EliminarCotizacion(int numero)
        {
            return await _cotizacionesRepository.EliminarCotizacion(numero);
        }
        public async Task<bool> ExisteCotizacion(int numero)
        {
            return await _cotizacionesRepository.ExisteCotizacion(numero);
        }

    }
}