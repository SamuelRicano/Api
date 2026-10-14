using System.ComponentModel.DataAnnotations;

namespace SmartClaims.Api.DTOs
{
    public class ActualizarCotizacionDto
    {
        [Required]
        [StringLength(100)]
        public string NombreCliente { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Prima { get; set; }
    }
}
