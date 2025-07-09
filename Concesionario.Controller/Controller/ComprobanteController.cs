using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConcesionarioWEBFORM1111.Model;
using ConcesionarioWEBFORM1111.DataBase;
using System.ComponentModel;

namespace ConcesionarioWEBFORM1111.Controller
{
    public class ComprobanteController
    {
        private readonly DataBaseConnection dbConnection;

        public ComprobanteController()
        {
            dbConnection = new DataBaseConnection();
        }

        // Método para obtener todos los comprobantes
        public List<Comprobante> ObtenerTodosLosComprobantes()
        {
            List<Comprobante> comprobantes = new List<Comprobante>();
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Comprobante ORDER BY FechaHora DESC";
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comprobantes.Add(MapearComprobante(reader));
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al obtener todos los comprobantes: {ex.Message}");
                }
            }

            return comprobantes;
        }



        // Funcion-Metodo para usar en ObtenerTodosLosComprobantes
        private Comprobante MapearComprobante(SqlDataReader reader)
        {
            return new Comprobante
            {
                ID_comprobante = reader.GetInt32(0),
                Tipo = reader.GetString(1),
                FechaHora = reader.GetDateTime(2),
                ID_auto = reader.GetInt32(3),
                ID_empleado = reader.GetInt32(4),
                Estado = reader.IsDBNull(5) ? null : reader.GetString(5),
                Observaciones = reader.IsDBNull(6) ? null : reader.GetString(6),
                Precio = reader.GetDecimal(7)
            };
        }
    }
}
