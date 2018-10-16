using MySql.Data.MySqlClient;
using ProtoBuf;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Quanlybanhang.Scripts.Source.Utils;

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
                if (transaction.ID >= 0)
                {
                    sql = "UPDATE store_transaction SET name = '" + transaction.Name + "', type = " + transaction.Type + ", transaction_data = " + transaction.TransactionDetail + ", shippingfee = " + transaction.Shippingfee + ", discount= " + transaction.Discount + ", lastupdate = '" + DateTime.UtcNow.ToUnixTime() + "' WHERE ID ='" + transaction.ID + "'";
                }
                else
                {
                    sql = "INSERT INTO store_transaction(name, type, transaction_data, shippingfee, discount, lastupdate) VALUES('" + transaction.Name + "', " + transaction.Type + ", " + transaction.TransactionDetail + ", " + transaction.Shippingfee + "," + transaction.Discount + ", '" + DateTime.UtcNow.ToUnixTime() + "')";
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
        }
    }

    [Serializable]
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
        public IList<TransactionDetail> TransactionDetail { get; set; }

        [ProtoMember(5)]
        public int Shippingfee { get; set; }

        [ProtoMember(6)]
        public float Discount { get; set; }
    }

    [ProtoContract]
    public class TransactionDetail
    {
        [ProtoMember(1)]
        public ProductContract Product { get; set; }

        [ProtoMember(2)]
        public uint Quantity { get; set; }
    }
}