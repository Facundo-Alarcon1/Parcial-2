using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ConcesionarioWEBFORM1111.Model;
using ConcesionarioWEBFORM1111.DataBase;

namespace ConcesionarioWEBFORM1111.Controller
{
    public class AutoController
    {
        private readonly DataBaseConnection dbConnection;

        public AutoController()
        {
            dbConnection = new DataBaseConnection();
        }

        // Método para agregar un nuevo auto (solo Gerente puede agregar)
        public bool AgregarAuto(Auto auto, Empleados empleado)
        {
            if (empleado.Puesto != "Gerente")
            {
                Console.WriteLine("Solo un Gerente puede agregar autos.");
                return false;
            }

            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    // Verifica si el ID_empleado existe en la tabla Empleados
                    string queryVerificarEmpleado = "SELECT COUNT(*) FROM Empleados WHERE ID_empleado = @ID_empleado";

                    SqlCommand commandVerificarEmpleado = new SqlCommand(queryVerificarEmpleado, connection);
                               commandVerificarEmpleado.Parameters.AddWithValue("@ID_empleado", auto.ID_empleado);

                    int empleadoExiste = (int)commandVerificarEmpleado.ExecuteScalar();
                    if (empleadoExiste == 0)
                    {
                        Console.WriteLine($"El ID_empleado {auto.ID_empleado} no existe en la tabla Empleados.");
                        return false;
                    }

                    // Insertamos el auto en la tabla Auto
                    string queryAuto = "INSERT INTO Auto (Marca, Modelo, Color, Patente, Anio, Estado, ID_empleado, Precio) " +
                                       "VALUES (@Marca, @Modelo, @Color, @Patente, @Anio, 'disponible', @ID_empleado, @Precio); " +
                                       "SELECT SCOPE_IDENTITY();"; // Obtenemos el ID del nuevo auto insertado

                    SqlCommand commandAuto = new SqlCommand(queryAuto, connection);
                    commandAuto.Parameters.AddWithValue("@Marca", auto.Marca);
                    commandAuto.Parameters.AddWithValue("@Modelo", auto.Modelo);
                    commandAuto.Parameters.AddWithValue("@Color", auto.Color);
                    commandAuto.Parameters.AddWithValue("@Patente", auto.Patente);
                    commandAuto.Parameters.AddWithValue("@Anio", auto.Anio);
                    commandAuto.Parameters.AddWithValue("@ID_empleado", auto.ID_empleado);
                    commandAuto.Parameters.AddWithValue("@Precio", auto.Precio);

                    // Obtenemos el ID del nuevo auto
                    int idNuevoAuto = Convert.ToInt32(commandAuto.ExecuteScalar());

                    if (idNuevoAuto > 0)
                    {
                        // Generar el comprobante de la adición del auto
                        string queryComprobante = "INSERT INTO Comprobante (Tipo, FechaHora, ID_auto, ID_empleado, Estado, Observaciones, Precio) " +
                                                  "VALUES ('compra', GETDATE(), @ID_auto, @ID_empleado, 'disponible', @Observaciones, @Precio)";
                        SqlCommand commandComprobante = new SqlCommand(queryComprobante, connection);
                        commandComprobante.Parameters.AddWithValue("@ID_auto", idNuevoAuto); // ID del nuevo auto
                        commandComprobante.Parameters.AddWithValue("@ID_empleado", auto.ID_empleado); // ID del empleado
                        commandComprobante.Parameters.AddWithValue("@Observaciones", "Auto ingresado al inventario");
                        commandComprobante.Parameters.AddWithValue("@Precio", auto.Precio); // Precio del auto

                        int rowsAffectedComprobante = commandComprobante.ExecuteNonQuery();

                        if (rowsAffectedComprobante > 0)
                        {
                            Console.WriteLine("El auto se agregó correctamente y se generó el comprobante.");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("No se pudo generar el comprobante.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se pudo agregar el auto.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al agregar el auto: {ex.Message}");
                }
            }

            return false;
        }


        // Método para actualizar solo el precio de un auto
        public bool ActualizarPrecioAuto(int idAuto, decimal nuevoPrecio)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Auto SET Precio = @Precio WHERE ID_auto = @ID_auto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_auto", idAuto);
                    command.Parameters.AddWithValue("@Precio", nuevoPrecio);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("El precio del auto se actualizó correctamente.");
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al actualizar el precio del auto: {ex.Message}");
                }
            }

            return false;
        }
        public bool ActualizarMarcaAuto(int idAuto, string nuevaMarca)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Auto SET Marca = @Marca WHERE ID_auto = @ID_auto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_auto", idAuto);
                    command.Parameters.AddWithValue("@Marca", nuevaMarca);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("La marca del auto se actualizó correctamente.");
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al actualizar marca del auto: {ex.Message}");
                }
            }

            return false;
        }

        public bool ActualizarModeloAuto(int idAuto, string nuevoModelo)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Auto SET Modelo = @Modelo WHERE ID_auto = @ID_auto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_auto", idAuto);
                    command.Parameters.AddWithValue("@Modelo", nuevoModelo);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("El modelo del auto se actualizó correctamente.");
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al actualizar el modelo del auto: {ex.Message}");
                }
            }


            return false;
        }

        public bool ActualizarColorAuto(int idAuto, string nuevoColor)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Auto SET Color = @Color WHERE ID_auto = @ID_auto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_auto", idAuto);
                    command.Parameters.AddWithValue("@Color", nuevoColor);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("El modelo del auto se actualizó correctamente.");
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al actualizar el modelo del auto: {ex.Message}");
                }
            }

            return false;
        }

        
        public bool ActualizarPatenteAuto(int idAuto, string nuevaPatente)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Auto SET Patente = @Patente WHERE ID_auto = @ID_auto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_auto", idAuto);
                    command.Parameters.AddWithValue("@Patente", nuevaPatente);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("La patente del auto se actualizó correctamente.");
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al actualizar la patente del auto: {ex.Message}");
                }
            }
            return false;
        }

        public bool ActualizarAnioAuto(int idAuto, int nuevoAnio)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Auto SET Anio = @Anio WHERE ID_auto = @ID_auto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_auto", idAuto);
                    command.Parameters.AddWithValue("@Anio", nuevoAnio);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("El año del auto se actualizó correctamente.");
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al actualizar el año del auto: {ex.Message}");
                }
            }
            return false;
        }

        public bool EliminarAuto(int idAuto)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    // Verificar si el auto existe
                    string queryVerificarAuto = "SELECT COUNT(*) FROM Auto WHERE ID_auto = @ID_auto";
                    SqlCommand commandVerificarAuto = new SqlCommand(queryVerificarAuto, connection);
                    commandVerificarAuto.Parameters.AddWithValue("@ID_auto", idAuto);
                    int autoExiste = (int)commandVerificarAuto.ExecuteScalar();
                    if (autoExiste == 0)
                    {
                        Console.WriteLine($"El auto con ID {idAuto} no existe.");
                        return false;
                    }

                    // Eliminar comprobantes relacionados
                    string queryEliminarComprobantes = "DELETE FROM Comprobante WHERE ID_auto = @ID_auto";
                    SqlCommand commandEliminarComprobantes = new SqlCommand(queryEliminarComprobantes, connection);
                    commandEliminarComprobantes.Parameters.AddWithValue("@ID_auto", idAuto);
                    commandEliminarComprobantes.ExecuteNonQuery();

                    // Eliminar el auto
                    string queryEliminarAuto = "DELETE FROM Auto WHERE ID_auto = @ID_auto";
                    SqlCommand commandEliminarAuto = new SqlCommand(queryEliminarAuto, connection);
                    commandEliminarAuto.Parameters.AddWithValue("@ID_auto", idAuto);
                    int rowsAffected = commandEliminarAuto.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("El auto se eliminó correctamente.");
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al eliminar el auto: {ex.Message}");
                }
            }
            return false;
        }



        // Método para buscar un auto por ID o palabras clave y mostrar todos los resultados disponibles
        public List<Auto> BuscarAuto(string criterio)
        {
            List<Auto> autos = new List<Auto>();

            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Auto WHERE Estado = 'disponible' AND " +
                                   "(CAST(ID_auto AS NVARCHAR) LIKE @Criterio OR Marca LIKE @Criterio OR Modelo LIKE @Criterio " +
                                   "OR Color LIKE @Criterio OR Patente LIKE @Criterio OR CAST(Anio AS NVARCHAR) LIKE @Criterio)";
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Criterio", $"{criterio}%");
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        autos.Add(new Auto
                        {
                            ID_auto = reader.GetInt32(0),
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Color = reader.GetString(3),
                            Patente = reader.GetString(4),
                            Anio = reader.GetInt32(5),
                            Estado = reader.GetString(6),
                            ID_empleado = reader.GetInt32(7),
                            Precio = reader.GetDecimal(8)
                        });
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al buscar el auto: {ex.Message}");
                }
            }

            return autos;
        }

        // Método para vender un auto (marcar como vendido y generar comprobante)
        public bool VenderAuto(int idAuto, int idEmpleado, string observaciones)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Marcar el auto como vendido
                        string queryAuto = "UPDATE Auto SET Estado = 'vendido' WHERE ID_auto = @ID_auto";
                        SqlCommand commandAuto = new SqlCommand(queryAuto, connection, transaction);
                        commandAuto.Parameters.AddWithValue("@ID_auto", idAuto);
                        int rowsAffectedAuto = commandAuto.ExecuteNonQuery();

                        if (rowsAffectedAuto == 0)
                        {
                            throw new Exception("No se pudo actualizar el estado del auto.");
                        }

                        // Generar el comprobante
                        string queryComprobante = "INSERT INTO Comprobante (Tipo, FechaHora, ID_auto, ID_empleado, Estado, Observaciones, Precio) " +
                                                   "VALUES ('venta', GETDATE(), @ID_auto, @ID_empleado, 'vendido', @Observaciones, " +
                                                   "(SELECT Precio FROM Auto WHERE ID_auto = @ID_auto))";
                        
                        SqlCommand commandComprobante = new SqlCommand(queryComprobante, connection, transaction);
                        commandComprobante.Parameters.AddWithValue("@ID_auto", idAuto);
                        commandComprobante.Parameters.AddWithValue("@ID_empleado", idEmpleado);
                        commandComprobante.Parameters.AddWithValue("@Observaciones", observaciones);

                        int rowsAffectedComprobante = commandComprobante.ExecuteNonQuery();
                        if (rowsAffectedComprobante == 0)
                        {
                            throw new Exception("No se pudo generar el comprobante.");
                        }

                        // Confirmar la transacción
                        transaction.Commit();
                        Console.WriteLine("El auto se vendió correctamente y se generó el comprobante.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Error al vender el auto: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        // Método para obtener todos los autos
        public List<Auto> ObtenerTodosLosAutos()
        {
            List<Auto> autos = new List<Auto>();
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Auto";
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        autos.Add(new Auto
                        {
                            ID_auto = reader.GetInt32(0),
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Color = reader.GetString(3),
                            Patente = reader.GetString(4),
                            Anio = reader.GetInt32(5),
                            Estado = reader.GetString(6),
                            ID_empleado = reader.GetInt32(7),
                            Precio = reader.GetDecimal(8)
                        });
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al obtener los autos: {ex.Message}");
                }
            }

            return autos;
        }

        // Método para buscar autos por estado
        public List<Auto> BuscarAutosPorEstado(string estado)
        {
            List<Auto> autos = new List<Auto>();
            using (SqlConnection connection = new SqlConnection(dbConnection.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT ID_auto, Marca, Modelo, Color, Patente, Anio, Estado, ID_empleado, Precio FROM Auto WHERE Estado = @Estado ORDER BY ID_Auto DESC";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Estado", estado);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        autos.Add(new Auto
                        {
                            ID_auto = reader.GetInt32(0),
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Color = reader.GetString(3),
                            Patente = reader.GetString(4),
                            Anio = reader.GetInt32(5),
                            Estado = reader.GetString(6),
                            ID_empleado = reader.GetInt32(7),
                            Precio = reader.GetDecimal(8)
                        });
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al buscar autos por estado: {ex.Message}");
                }
            }

            return autos;
        }
    }
}
