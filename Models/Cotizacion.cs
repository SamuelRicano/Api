using System.ComponentModel.DataAnnotations;

namespace SmartClaims.Api.Models
{
    public class Cotizacion
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int Numero { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCliente { get; set; }

        [Range(0.01,double.MaxValue)]
        public decimal Prima { get; set; }
    }
}
