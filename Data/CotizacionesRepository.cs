using Microsoft.Data.SqlClient;
using SmartClaims.Api.Data.Interfaces;
using SmartClaims.Api.Models;

namespace SmartClaims.Api.Data
{
    public class CotizacionesRepository : ICotizacionesRepository
    {
        private readonly string _connectionString;

        public CotizacionesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SmartClaims");
        }

        public async Task <List<Cotizacion>> ObtenerCotizaciones()
        {
            var cotizaciones = new List<Cotizacion>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
                    SELECT Numero, NombreCliente, Prima
                    FROM Cotizaciones";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cotizacion = new Cotizacion
                            {
                                Numero = Convert.ToInt32(reader["Numero"]),
                                NombreCliente = reader["NombreCliente"].ToString(),
                                Prima = Convert.ToDecimal(reader["Prima"])
                            };

                            cotizaciones.Add(cotizacion);
                        }
                    }
                }
            }

            return cotizaciones;
        }

        public async Task<Cotizacion> ObtenerCotizacion(int numero)
        {
            Cotizacion cotizacion = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            SELECT Numero, NombreCliente, Prima
            FROM Cotizaciones
            WHERE Numero = @Numero";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", numero);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cotizacion = new Cotizacion
                            {
                                Numero = Convert.ToInt32(reader["Numero"]),
                                NombreCliente = reader["NombreCliente"].ToString(),
                                Prima = Convert.ToDecimal(reader["Prima"])
                            };
                        }
                    }
                }
            }

            return cotizacion;
        }

        public async Task<Cotizacion> CrearCotizacion(Cotizacion cotizacion)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            INSERT INTO Cotizaciones
            (
                Numero,
                NombreCliente,
                Prima
            )
            VALUES
            (
                @Numero,
                @NombreCliente,
                @Prima
            )";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", cotizacion.Numero);
                    command.Parameters.AddWithValue("@NombreCliente", cotizacion.NombreCliente);
                    command.Parameters.AddWithValue("@Prima", cotizacion.Prima);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return cotizacion;

        }
        public async Task<bool> ActualizarCotizacion(Cotizacion cotizacion)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            UPDATE Cotizaciones
            SET
                NombreCliente = @NombreCliente,
                Prima = @Prima
            WHERE Numero = @Numero";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", cotizacion.Numero);
                    command.Parameters.AddWithValue("@NombreCliente", cotizacion.NombreCliente);
                    command.Parameters.AddWithValue("@Prima", cotizacion.Prima);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();

                    return filasAfectadas > 0;
                }
            }
        }
        public async Task<bool> EliminarCotizacion(int numero)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            DELETE FROM Cotizaciones
            WHERE Numero = @Numero";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", numero);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();

                    return filasAfectadas > 0;
                }
            }
        }
        public async Task<bool> ExisteCotizacion(int numero)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            SELECT COUNT(1)
            FROM Cotizaciones
            WHERE Numero = @Numero";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", numero);
                    var resultado = await command.ExecuteScalarAsync();

                    int cantidad = Convert.ToInt32(resultado);

                    return cantidad > 0;
                }
            }
        }

        public void ProbarConexion()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
            }
        }
    }
}