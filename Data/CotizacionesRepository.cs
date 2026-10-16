//using Microsoft.Data.SqlClient;
using Npgsql;
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

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();


                string sql = @"SELECT ""Numero"", ""NombreCliente"", ""Prima"" FROM ""Cotizaciones""";

                //string sql = @"
                //    SELECT Numero, NombreCliente, Prima
                //    FROM Cotizaciones";

                using (NpgsqlCommand  command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader  reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cotizacion = new Cotizacion
                            {
                                Numero = Convert.ToInt32(reader["numero"]),
                                NombreCliente = reader["nombrecliente"].ToString(),
                                Prima = Convert.ToDecimal(reader["prima"])
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

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            SELECT Numero, NombreCliente, Prima
            FROM Cotizaciones
            WHERE Numero = @Numero";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", numero);

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
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
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"
                    INSERT INTO ""Cotizaciones""
                    (
                        ""Numero"",
                        ""NombreCliente"",
                        ""Prima""
                    )
                    VALUES
                    (
                        @Numero,
                        @NombreCliente,
                        @Prima
                    )";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Numero", cotizacion.Numero);
                        command.Parameters.AddWithValue("@NombreCliente", cotizacion.NombreCliente);
                        command.Parameters.AddWithValue("@Prima", cotizacion.Prima);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return cotizacion;
            }
            catch (PostgresException ex)
            {

                Console.WriteLine($"Error PostgreSQL: {ex.MessageText}");
                Console.WriteLine($"Código SQL: {ex.SqlState}");
                Console.WriteLine($"Detalle: {ex.Detail}");
                Console.WriteLine($"Restricción: {ex.ConstraintName}");

                throw;
            }

       

        }
        public async Task<bool> ActualizarCotizacion(Cotizacion cotizacion)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            UPDATE Cotizaciones
            SET
                NombreCliente = @NombreCliente,
                Prima = @Prima
            WHERE Numero = @Numero";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
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
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
            DELETE FROM Cotizaciones
            WHERE Numero = @Numero";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", numero);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();

                    return filasAfectadas > 0;
                }
            }
        }
        public async Task<bool> ExisteCotizacion(int numero)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                //    string sql = @"
                //SELECT COUNT(1)
                //FROM Cotizaciones
                //WHERE Numero = @Numero";
                string sql = @"
                    SELECT COUNT(1)
                    FROM ""Cotizaciones""
                    WHERE ""Numero"" = @Numero";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
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
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
            }
        }
    }
}