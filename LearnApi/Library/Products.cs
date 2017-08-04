using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Products
    {
        public List<Product> GetProduct(int row)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connect = new SqlConnection(Config.ConnectionString))
            {
                connect.Open();
                string sql = @"SELECT TOP 50 ProductID, Name, ProductNumber, Color, Weight FROM (SELECT ProductID, Name, ProductNumber, Color, Weight, ROW_NUMBER() OVER(ORDER BY ProductID) as row FROM Production.Product) AS data WHERE data.row > @row ";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.Parameters.AddWithValue("@row", row);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductID")))
                        {
                            product.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                        }
                        product.Name = reader["Name"].ToString();
                        product.ProductNumber = reader["ProductNumber"].ToString();
                        product.Color = reader["Color"].ToString();
                        product.Weight = reader["Weight"].ToString();

                        products.Add(product);
                    }
                }
                return products;
            }
        }
        public Product GetProductByID(int id)
        {
            using (SqlConnection connect = new SqlConnection(Config.ConnectionString))
            {
                Product product = new Product();
                connect.Open();
                string sql = @"SELECT ProductID, Name, ProductNumber, Color, Weight FROM Production.Product WHERE ProductID = @id ";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        if (!reader.IsDBNull(reader.GetOrdinal("ProductID")))
                        {
                            product.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                        }
                        product.Name = reader["Name"].ToString();
                        product.ProductNumber = reader["ProductNumber"].ToString();
                        product.Color = reader["Color"].ToString();
                        product.Weight = reader["Weight"].ToString();
                    }
                }
                return product;
            }
        }
    }

    public class Product
    {
        public int ProductID;
        public string Name;
        public string ProductNumber;
        public string Color;
        public string Weight;
    }
}
