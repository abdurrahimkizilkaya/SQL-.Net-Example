using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Case
{
    class DatabaseHandler
    {
        // Database connection string
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDBFilename=|DataDirectory|\\Database.mdf;Integrated Security=True";
        public DatabaseHandler()
        {
            TestConnectionToDatabase();
        }

        /// <summary>
        /// Test connection to specified database given with path
        /// </summary>
        private void TestConnectionToDatabase()
        {
            StringBuilder errorMessages = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                }
            }
        }

        /// <summary>
        /// Gets all sales history
        /// </summary>
        public void GetSalesHistory()
        {
            string sql = "SELECT * FROM inventory_sales";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Product ID: {0} Store ID: {1} Date: {2} Sales Quantity: {3} Stock: {4}",
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetDateTime(2),
                                reader.GetInt32(3),
                                reader.GetInt32(4));
                        }
                        Console.WriteLine("\n\n");
                    }
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Adds new history record to inventory_sales database
        /// </summary>
        public void AddNewHistoryRecord(int productId, int storeId, DateTime date, int salesQuantity, int stock)
        {
            Console.WriteLine("Sales history records after new addition is below:");
            string query = "INSERT INTO inventory_sales(ProductID, StoreID, Date, SalesQuantity, Stock) VALUES(@productId, @storeId, @date, @salesQuantity, @stock)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productId", productId);
                    command.Parameters.AddWithValue("@storeId", storeId);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@salesQuantity", salesQuantity);
                    command.Parameters.AddWithValue("@stock", stock);
                    Console.WriteLine(command.ExecuteNonQuery() + " row(s) inserted");
                }
                connection.Close();
            }
            GetSalesHistory();
        }

        /// <summary>
        /// Deletes a history record from inventory_sales database
        /// </summary>
        public void DeleteSalesHistoryRecord(DateTime rowToDelete)
        {
            Console.WriteLine("Sales history records after deletion is below:");
            string query = "DELETE FROM inventory_sales WHERE Date = @date;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date", rowToDelete);
                    Console.WriteLine(command.ExecuteNonQuery() + " row(s) deleted");
                }
                connection.Close();
            }
            GetSalesHistory();
        }

        /// <summary>
        /// Updates a history record on inventory_sales database
        /// </summary>
        public void UpdateSalesHistoryRecord(DateTime rowToUpdate)
        {
            Console.WriteLine("Sales history records after update is below:");
            string query = "UPDATE inventory_sales SET ProductID = N'3' WHERE Date = @date";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date", rowToUpdate);
                    Console.WriteLine(command.ExecuteNonQuery() + " row(s) updated");
                }
                connection.Close();
            }
            GetSalesHistory();
        }

        /// <summary>
        /// Gets profit for given store
        /// </summary>
        /// <param name="storeName"></param>
        public void GetProfitForAGivenStore(string storeName)
        {
            string query = "SELECT SUM(SalesQuantity * (Price - Cost)) FROM stores, products, inventory_sales where stores.Name = '"+storeName+"' AND stores.Id = inventory_sales.StoreID AND products.Id = inventory_sales.ProductID GROUP BY stores.Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Profit for {0} store is {1}", storeName, reader.GetInt32(0));
                        }
                        Console.WriteLine("\n");
                    }
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Gets the most profitable store
        /// </summary>
        public void GetMostProfitableStore()
        {
            string query = "SELECT TOP 1 stores.Name FROM stores, products, inventory_sales where stores.Id = inventory_sales.StoreID AND products.Id = inventory_sales.ProductID GROUP BY stores.Name ORDER BY SUM(SalesQuantity * (Price - Cost)) DESC";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Most profitable store is {0}", reader.GetString(0));
                        }
                        Console.WriteLine("\n");
                    }
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Gets the best seller product by sales quantity
        /// </summary>
        public void GetBestSellerbySalesQuantity()
        {
            string query = "SELECT TOP 1 Name FROM products as p INNER JOIN inventory_sales as sales on p.Id = sales.ProductID GROUP BY Name ORDER BY sum(SalesQuantity) DESC";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Best seller product by sales quantity is {0}", reader.GetString(0));
                        }
                        Console.WriteLine("\n");
                    }
                }
                connection.Close();
            }
        }
    }
}
