using SmartClaims.Api.Models;

namespace SmartClaims.Api.Services.Interfaces
{
    public interface ICotizacionesService
    {
        Task<List<Cotizacion>> ObtenerCotizaciones();

        Task<Cotizacion> ObtenerCotizacion(int numero);

        Task<Cotizacion> CrearCotizacion(Cotizacion cotizacion);

        Task<bool> ActualizarCotizacion(Cotizacion cotizacion);

        Task<bool> EliminarCotizacion(int numero);

        Task<bool> ExisteCotizacion(int numero);
    }
}
