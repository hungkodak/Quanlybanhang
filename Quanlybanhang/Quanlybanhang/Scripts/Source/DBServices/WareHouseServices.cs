using MySql.Data.MySqlClient;
using Quanlybanhang.Scripts.Source.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.DBServices
{
    public class WareHouseServices
    {
        static string _conStr = ConfigurationManager.ConnectionStrings["constrstore"].ConnectionString;
        static MySqlConnection _conObj = new MySqlConnection(_conStr);
        static public bool CreateOrUpdateWareHouse(TransactionDetail transactionDetail)
        {
            if(transactionDetail == null)
            {
                return false;
            }
            TransactionDetail existTransactionDetail = GetProductFromWareHouse(transactionDetail.Product.ID);
            if (existTransactionDetail == null)
            {
                if (!CreateInternal(transactionDetail))
                {
                    return false;
                }
            }
            else
            {
                existTransactionDetail.Quantity += transactionDetail.Quantity;
                if (!UpdateInternal(transactionDetail))
                {
                    return false;
                }
            }
            return true;
        }

        static private bool CreateInternal(TransactionDetail transactionDetail)
        {
            try
            {
                _conObj.Open();                

                string sql = "INSERT INTO warehouse(productId, quantity, lastupdate) VALUES('" + transactionDetail.Product.ID + "'," + transactionDetail.Quantity + ", '" + DateTime.UtcNow.ToUnixTime() + "')";
                
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);                
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("WareHouseServices:CreateInternal", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static private bool UpdateInternal(TransactionDetail transactionDetail)
        {
            try
            {
                _conObj.Open();                

                string sql = "UPDATE warehouse set quantity='"+ transactionDetail.Quantity + "', lastupdate = '" + DateTime.UtcNow.ToUnixTime() + "' WHERE productid ='" + transactionDetail.Product.ID + "'";                            

                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("WareHouseServices:CreateInternal", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public TransactionDetail GetProductFromWareHouse(string productId)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM warehouse where productId='" + productId + "'";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                

                if (reader.HasRows)
                {
                    reader.Read();
                    ProductContract product = ProductsServices.GetProduct(reader[0].ToString());
                    if (product == null)
                    {
                        product = new ProductContract()
                        {
                            ID = reader[0].ToString(),
                            Name = "Not found in product",
                            ExportPrice = 0,
                            ImportPrice = 0
                        };
                    }
                    TransactionDetail transactionDetail = new TransactionDetail()
                    {
                        Product = product,
                        Quantity = UInt32.Parse(reader[2].ToString())
                    };
                    return transactionDetail;
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
    }
}