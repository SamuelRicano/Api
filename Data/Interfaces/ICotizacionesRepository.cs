using SmartClaims.Api.Models;

namespace SmartClaims.Api.Data.Interfaces
{
    public interface ICotizacionesRepository
    {
        Task<List<Cotizacion>> ObtenerCotizaciones();

        Task<Cotizacion> ObtenerCotizacion(int numero);

        Task<Cotizacion> CrearCotizacion(Cotizacion cotizacion);

        Task<bool> ActualizarCotizacion(Cotizacion cotizacion);

        Task<bool> EliminarCotizacion(int numero);

        Task<bool> ExisteCotizacion(int numero);
    }
}
