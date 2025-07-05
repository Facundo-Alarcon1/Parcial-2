using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConcesionarioWEBFORM1111.DataBase;
using ConcesionarioWEBFORM1111.Model;

namespace ConcesionarioWEBFORM1111.Controller
{
    public class LoginController
    {
        private readonly DataBaseConnection dbConnection;

        public LoginController()
        {
            dbConnection = new DataBaseConnection();
        }

        public (string Puesto, int IdEmpleado, string NombreUsuario, string Contraseña) ValidarCredenciales(string nombreUsuario, string contraseña)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                connection.Open();
                string query = "SELECT Puesto, ID_empleado, NombreUsuario, Contraseña FROM Empleados WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                command.Parameters.AddWithValue("@Contraseña", contraseña);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (
                            reader["Puesto"].ToString(),
                            Convert.ToInt32(reader["ID_empleado"]),
                            reader["NombreUsuario"].ToString(),
                            reader["Contraseña"].ToString()
                        );
                    }
                }
            }

            return (null, 0, null, null);
        }

        public bool PuedeVenderAutos(string nombreUsuario, string contraseña)
        {
            var (puesto, _, _, _) = ValidarCredenciales(nombreUsuario, contraseña);
            return puesto == "Empleado" || puesto == "Gerente";
        }

        public bool PuedeAgregarAutos(string nombreUsuario, string contraseña)
        {
            var (puesto, _, _, _) = ValidarCredenciales(nombreUsuario, contraseña);
            return puesto == "Gerente";
        }

        public List<Empleados> ObtenerTodosLosEmpleados()
        {
            var empleados = new List<Empleados>();
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ID_empleado, Nombre, Apellido FROM Empleados";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            empleados.Add(new Empleados
                            {
                                ID_empleado = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2)
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al obtener empleados: {ex.Message}");
                }
            }
            return empleados;
        }
    }
}
