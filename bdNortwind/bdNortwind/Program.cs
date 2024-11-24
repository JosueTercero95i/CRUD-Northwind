using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace progrmaBD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            string connectionString = "Server=DESKTOP-TLN3SFR;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True";

 
            Select(connectionString);
            Insert(connectionString);
            Update(connectionString);
            Delete(connectionString);
            LoadDataset(connectionString);
        }

        //-----------------------------------
        static void Select(string connectionString)
        {
            string query = "SELECT ProductID, ProductName, UnitPrice FROM Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    Console.WriteLine("Intentando conectar a la base de datos...");
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos Northwind.");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ProductID\tProductName\t\tUnitPrice");

                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ProductID"]}\t{reader["ProductName"],-20}\t{reader["UnitPrice"]:C}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en SELECT: {ex.Message}");
                }
            }
        }

        //-----------------------------------------
        static void Insert(string connectionString)
        {
            string query = "INSERT INTO Products (ProductName, UnitPrice, UnitsInStock) VALUES (@ProductName, @UnitPrice, @UnitsInStock)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Agregar parámetros
                command.Parameters.AddWithValue("@ProductName", "New Product");
                command.Parameters.AddWithValue("@UnitPrice", 15.00);
                command.Parameters.AddWithValue("@UnitsInStock", 50);

                try
                {
                    Console.WriteLine("Intentando insertar un nuevo producto...");
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos Northwind.");

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} fila(s) insertada(s).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en INSERT: {ex.Message}");
                }
            }
        }

        //-----------------------------------------
        static void Update(string connectionString)
        {
            string query = "UPDATE Products SET ProductName = @NewProductName, UnitPrice = @NewUnitPrice WHERE ProductID = @ProductID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Agregar parámetros
                command.Parameters.AddWithValue("@NewProductName", "Updated Product");
                command.Parameters.AddWithValue("@NewUnitPrice", 20.00);
                command.Parameters.AddWithValue("@ProductID", 1); // Cambiar según el producto a actualizar

                try
                {
                    Console.WriteLine("Intentando actualizar un producto...");
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos Northwind.");

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} fila(s) actualizada(s).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en UPDATE: {ex.Message}");
                }
            }
        }

        //-----------------------------------------
        static void Delete(string connectionString)
        {
            string query = "DELETE FROM Products WHERE ProductID = @ProductID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Agregar parámetros
                command.Parameters.AddWithValue("@ProductID", 3); // Cambiar según el producto a eliminar

                try
                {
                    Console.WriteLine("Intentando eliminar un producto...");
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos Northwind.");

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} fila(s) eliminada(s).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en DELETE: {ex.Message}");
                }
            }
        }

        //-----------------------------------------------------------------------
        static void LoadDataset(string connectionString)
        {
            string query = "SELECT * FROM Products; SELECT * FROM Categories;";

            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


                    adapter.Fill(dataSet);

                    // Obtener tablas
                    DataTable productsTable = dataSet.Tables[0]; 
                    DataTable categoriesTable = dataSet.Tables[1]; 

                    // Imprimir datos de la tabla Products
                    Console.WriteLine("\nTabla Products:");
                    foreach (DataRow row in productsTable.Rows)
                    {
                        Console.WriteLine($"ID: {row["ProductID"]}, Nombre: {row["ProductName"]}");
                    }

                    // Imprimir datos de la tabla Categories
                    Console.WriteLine("\nTabla Categories:");
                    foreach (DataRow row in categoriesTable.Rows)
                    {
                        Console.WriteLine($"ID: {row["CategoryID"]}, Nombre: {row["CategoryName"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en DATASET: {ex.Message}");
                }
            }
        }
    }
}
