using MySql.Data.MySqlClient;
using ProtoBuf;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.DBServices
{
    public class StoreTransactionServices
    {
        static string _conStr = ConfigurationManager.ConnectionStrings["constrstore"].ConnectionString;
        static MySqlConnection _conObj = new MySqlConnection(_conStr);
        static public bool CreateOrUpdateTransaction(StoreTransactionContract transaction)
        {
            try
            {
                _conObj.Open();
                string sql;
                if (transaction.ID>=0)
                {
                    sql = "INSERT INTO store_transaction(name, type, transaction_data, lastupdate) VALUES('" + transaction.Name + "', '" + product.Name + "', " + product.ExportPrice + ", " + product.ImportPrice + ", '" + DateTime.UtcNow.ToMySQLDateTimeString() + "')";
                }
                
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
            return true;
        }
    }

    [ProtoContract]
    public class StoreTransactionContract
    {
        [ProtoMember(1)]
        public int ID { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public AgencyRole Type { get; set; }

        [ProtoMember(4)]
        public ProductContract[] Products { get; set; }

        public StoreTransactionContract()
        {

        }
    }
}