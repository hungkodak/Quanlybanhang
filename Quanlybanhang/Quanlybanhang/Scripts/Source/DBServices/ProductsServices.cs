using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using ProtoBuf;
using Quanlybanhang.Scripts.Source.Defination;
using Quanlybanhang.Scripts.Source.Utils;

namespace Quanlybanhang.Scripts.Source.DBServices
{
    public class ProductsServices
    {
        static string _conStr = ConfigurationManager.ConnectionStrings["constrstore"].ConnectionString;
        static MySqlConnection _conObj = new MySqlConnection(_conStr);

        static public bool CreateProduct(ProductContract product)
        {
            try
            {
                _conObj.Open();
                string sql = "INSERT INTO products(id, name, exportprice, importprice, lastupdate) VALUES('" + product.ID + "', '" + product.Name + "', " + product.ExportPrice + ", " + product.ImportPrice + ", '" + DateTime.UtcNow.ToUnixTime() + "')";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("ProductsServices:CreateProduct", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public bool UpdateProduct(ProductContract product)
        {
            try
            {
                _conObj.Open();
                string sql = "UPDATE products SET name = '" + product.Name + "', exportprice = " + product.ExportPrice + ", importprice = " + product.ImportPrice + ", lastupdate = '" + DateTime.UtcNow.ToUnixTime() + "' WHERE ID ='" + product.ID + "'";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("ProductsServices:UpdateProduct", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public List<ProductContract> GetProductList(int offset, int numrow)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM products limit " + offset + "," + numrow;
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<ProductContract> productList = new List<ProductContract>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductContract product = new ProductContract()
                        {
                            ID = reader[0].ToString(),
                            Name = reader[1].ToString(),
                            ExportPrice = Int32.Parse(reader[2].ToString()),
                            ImportPrice = Int32.Parse(reader[3].ToString())
                        };
                        productList.Add(product);
                    }
                }
                return productList;
            }
            catch (MySqlException ex)
            {
                throw new Exception("ProductsServices:GetProductList", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public ProductContract GetProduct(string productId)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM products where ID='" + productId + "'";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<ProductContract> productList = new List<ProductContract>();

                if (reader.HasRows)
                {
                    reader.Read();
                    ProductContract product = new ProductContract()
                    {
                        ID = reader[0].ToString(),
                        Name = reader[1].ToString(),
                        ExportPrice = Int32.Parse(reader[2].ToString()),
                        ImportPrice = Int32.Parse(reader[3].ToString())
                    };
                    return product;
                }
                return null;
            }
            catch (MySqlException ex)
            {
                throw new Exception("ProductsServices:GetProductList", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public int GetCountTotalProduct()
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT Count(*) FROM products";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    return Int32.Parse(reader[0].ToString());
                }

                return 0;
            }
            catch (MySqlException ex)
            {
                throw new Exception("AgencyServices:GetCountTotalAgency", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public List<ProductContract> SearchProductByProductId(string productId, int limit=1)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM products where ID like '%"+ productId +"%' limit " + limit;
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<ProductContract> productList = new List<ProductContract>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductContract product = new ProductContract()
                        {
                            ID = reader[0].ToString(),
                            Name = reader[1].ToString(),
                            ExportPrice = Int32.Parse(reader[2].ToString()),
                            ImportPrice = Int32.Parse(reader[3].ToString())
                        };
                        productList.Add(product);
                    }
                }
                return productList;
            }
            catch (MySqlException ex)
            {
                throw new Exception("ProductsServices:GetProductList", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }
    }

    [Serializable]
    [ProtoContract]
    public class ProductContract
    {
        [ProtoMember(1)]
        public string ID { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public int ExportPrice { get; set; }

        [ProtoMember(4)]
        public int ImportPrice { get; set; }

        public ProductContract()
        {
            
        }
    }
}