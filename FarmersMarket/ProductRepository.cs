using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SQLite;

namespace FarmersMarket
{
    internal class ProductRepository
    {
        private SQLiteConnection conn;

        public ProductRepository(string connectionString)
        {
            conn = new SQLiteConnection(connectionString);
            conn.Open();

            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [Product] (
                        [ProductId] INTEGER NOT NULL PRIMARY KEY,
                        [ProductName] NVARCHAR(2048) NULL,
                        [Amount] REAL NULL,
                        [PricePerKg] REAL NULL
                    )";

            using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            string selectQuery = "SELECT * FROM Product";
            using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["ProductName"].ToString(),
                            Amount = Convert.ToDouble(reader["Amount"]),
                            PricePerKg = Convert.ToDouble(reader["PricePerKg"])
                        });
                    }
                }
            }

            return products;
        }

        public void InsertProduct(Product product)
        {
            string insertQuery = $"INSERT INTO Product (ProductName, Amount, PricePerKg) VALUES (@productName, @amount, @pricePerKg)";
            using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@productName", product.ProductName);
                cmd.Parameters.AddWithValue("@amount", product.Amount);
                cmd.Parameters.AddWithValue("@pricePerKg", product.PricePerKg);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product)
        {
            string updateQuery = $"UPDATE Product SET ProductName = @productName, Amount = @amount, PricePerKg = @pricePerKg WHERE ProductId = @productId";
            using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@productName", product.ProductName);
                cmd.Parameters.AddWithValue("@amount", product.Amount);
                cmd.Parameters.AddWithValue("@pricePerKg", product.PricePerKg);
                cmd.Parameters.AddWithValue("@productId", product.ProductId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int productId)
        {
            string deleteQuery = $"DELETE FROM Product WHERE ProductId = @productId";
            using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
            {
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
