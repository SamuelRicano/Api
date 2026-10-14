using SmartClaims.Api.Data.Interfaces;
using SmartClaims.Api.Models;

namespace SmartClaims.Api.Data
{
    public class FakeCotizacionesRepository : ICotizacionesRepository
    {
        private readonly List<Cotizacion> _cotizaciones = new List<Cotizacion>
        {
            new Cotizacion
            {
                Numero = 9001,
                NombreCliente = "Cliente Fake 1",
                Prima = 10000
            },
            new Cotizacion
            {
                Numero = 9002,
                NombreCliente = "Cliente Fake 2",
                Prima = 15000
            }
        };

        public Task<List<Cotizacion>> ObtenerCotizaciones()
        {
            return Task.FromResult(_cotizaciones);
        }

        public Task<Cotizacion> ObtenerCotizacion(int numero)
        {
            var cotizacion = _cotizaciones
                .FirstOrDefault(x => x.Numero == numero);

            return Task.FromResult(cotizacion);
        }

        public Task<Cotizacion> CrearCotizacion(Cotizacion cotizacion)
        {
            _cotizaciones.Add(cotizacion);

            return Task.FromResult(cotizacion);
        }

        public Task<bool> ActualizarCotizacion(Cotizacion cotizacion)
        {
            var existente = _cotizaciones
                .FirstOrDefault(x => x.Numero == cotizacion.Numero);

            if (existente == null)
            {
                return Task.FromResult(false);
            }

            existente.NombreCliente = cotizacion.NombreCliente;
            existente.Prima = cotizacion.Prima;

            return Task.FromResult(true);
        }

        public Task<bool> EliminarCotizacion(int numero)
        {
            var cotizacion = _cotizaciones
                .FirstOrDefault(x => x.Numero == numero);

            if (cotizacion == null)
            {
                return Task.FromResult(false);
            }

            _cotizaciones.Remove(cotizacion);

            return Task.FromResult(true);
        }

        public Task<bool> ExisteCotizacion(int numero)
        {
            var existe = _cotizaciones
                .Any(x => x.Numero == numero);

            return Task.FromResult(existe);
        }
    }
}
