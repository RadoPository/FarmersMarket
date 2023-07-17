using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace FarmersMarket
{
    internal class SaleRepository
    {
        private SQLiteConnection conn;
        private ProductRepository productRepo;

        public SaleRepository(string connectionString, ProductRepository productRepo)
        {
            this.productRepo = productRepo;

            conn = new SQLiteConnection(connectionString);
            conn.Open();

            string createTableQuery = @"CREATE TABLE IF NOT EXISTS [Sale] (
                        [SaleId] INTEGER NOT NULL PRIMARY KEY,
                        [CustomerId] INTEGER NOT NULL,
                        [ProductId] INTEGER NOT NULL,
                        [Quantity] REAL NULL
                    )";

            using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void AddSale(Sale sale)
        {
            // Begin a transaction
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    // Insert sale record
                    string insertQuery = "INSERT INTO Sale (CustomerId, ProductId, Quantity) VALUES (@customerId, @productId, @quantity)";
                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@customerId", sale.CustomerId);
                        cmd.Parameters.AddWithValue("@productId", sale.ProductId);
                        cmd.Parameters.AddWithValue("@quantity", sale.Quantity);
                        cmd.ExecuteNonQuery();
                    }

                    // Update product quantity
                    Product product = productRepo.GetProduct(sale.ProductId);
                    product.Amount -= sale.Quantity;
                    productRepo.UpdateProduct(product);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // If anything goes wrong, roll back the transaction
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
