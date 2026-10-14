namespace SmartClaims.Api.Exceptions
{
    public class CotizacionDuplicadaException : Exception
    {
        public int Numero { get; }

        public CotizacionDuplicadaException(int numero)
            : base("La cotización ya existe.")
        {
            Numero = numero;
        }
    }
}
