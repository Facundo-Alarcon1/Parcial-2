using System;
using System.Collections.Generic;
using System.Text;
using ConcesionarioWEBFORM1111.DataBase;
using ConcesionarioWEBFORM1111.Model;
using System.Data.SqlClient;


namespace ConcesionarioWEBFORM1111.Controller
{
    

    public class ServicioController
    {
        private readonly DataBaseConnection dbConnection;

        public ServicioController()
        {
            dbConnection = new DataBaseConnection();
        }

        // Método para agregar un servicio
        public bool AgregarServicio(Servicio servicio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO Servicios (Descripcion, Fecha, Estado, ID_empleado) " +
                                   "VALUES (@Descripcion, @Fecha, @Estado, @ID_empleado)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Descripcion", servicio.Descripcion);
                    cmd.Parameters.AddWithValue("@Fecha", servicio.Fecha);
                    cmd.Parameters.AddWithValue("@Estado", "En proceso"); // Siempre se agrega como "En proceso"
                    cmd.Parameters.AddWithValue("@ID_empleado", servicio.ID_empleado);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar servicio: " + ex.Message);
                return false;
            }
        }

        // Método para marcar un servicio como realizado
        public bool MarcarComoRealizado(int idServicio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Servicios SET Estado = 'Realizado' WHERE ID_servicio = @ID_servicio";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID_servicio", idServicio);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al marcar como realizado: " + ex.Message);
                return false;
            }
        }

        // Método para eliminar un servicio
        public bool EliminarServicio(int idServicio)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM Servicios WHERE ID_servicio = @ID_servicio";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID_servicio", idServicio);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0; // Si se elimina al menos un servicio, devolvemos true
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar servicio: " + ex.Message);
                return false;
            }
        }

        // Método para obtener todos los servicios
        public List<Servicio> ObtenerTodosLosServicios()
        {
            List<Servicio> lista = new List<Servicio>();

            try
            {
                using (SqlConnection conn = new SqlConnection(dbConnection.connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    S.ID_servicio, 
                    S.Descripcion, 
                    S.Fecha, 
                    S.Estado, 
                    S.ID_empleado, 
                    E.Nombre + ' ' + E.Apellido AS NombreCompleto
                FROM Servicios S
                INNER JOIN Empleados E ON S.ID_empleado = E.ID_empleado
                ORDER BY S.Fecha DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Servicio servicio = new Servicio()
                            {
                                ID_servicio = reader.GetInt32(0),
                                Descripcion = reader.GetString(1),
                                Fecha = reader.GetDateTime(2),
                                Estado = reader.GetString(3),
                                ID_empleado = reader.GetInt32(4),
                                NombreEmpleado = reader.GetString(5)  // Esto es lo nuevo
                            };
                            lista.Add(servicio);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener servicios: " + ex.Message);
            }

            return lista;
        }

    }

}
