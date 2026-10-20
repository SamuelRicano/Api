using AutoMapper;
using SmartClaims.Api.Models;
using SmartClaims.Api.DTOs;

namespace SmartClaims.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 1. Configuración para CREAR: 
            // Transforma de CrearCotizacionDto hacia el Modelo Cotizacion
            CreateMap<CrearCotizacionDto, Cotizacion>();

            // 2. Configuración para ACTUALIZAR:
            // Transforma de ActualizarCotizacionDto hacia el Modelo Cotizacion
            CreateMap<ActualizarCotizacionDto, Cotizacion>();

            // TIP: Si tus variables se llaman exactamente igual en el DTO 
            // y en el Modelo, no necesitas escribir nada más. ¡AutoMapper lo deduce!
        }

    }
}
