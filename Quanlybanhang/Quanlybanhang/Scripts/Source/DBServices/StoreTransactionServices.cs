using MySql.Data.MySqlClient;
using ProtoBuf;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Quanlybanhang.Scripts.Source.Utils;
using System.IO;

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
                StoreTransactionContract transactionContract = GetTransaction(transaction.ID);
                _conObj.Open();
                string sql;                
                byte[] data = SerializerHelper.Serialize(transaction.TransactionDetail);
                MySqlParameter transaction_data = new MySqlParameter("?transaction_data", MySqlDbType.Blob, data.Length);
                transaction_data.Value = data;
                if (transactionContract != null)
                {
                    sql = "UPDATE store_transaction SET name = '" + transaction.Name + "', type = " + (int)transaction.Type + ", transaction_data = ?transaction_data, shippingfee = " + transaction.Shippingfee + ", discount= " + transaction.Discount + ", lastupdate = '" + DateTime.UtcNow.ToUnixTime() + "' WHERE ID ='" + transaction.ID + "'";
                }
                else
                {
                    sql = "INSERT INTO store_transaction(ID, name, type, transaction_data, shippingfee, discount, lastupdate) VALUES('" + transaction.ID + "','" + transaction.Name + "', " + (int)transaction.Type + ", ?transaction_data, " + transaction.Shippingfee + "," + transaction.Discount + ", '" + DateTime.UtcNow.ToUnixTime() + "')";
                }

                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                cmd.Parameters.Add(transaction_data);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("StoreTransactionServices:CreateOrUpdateTransaction", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public StoreTransactionContract GetTransaction(string transactionId)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM store_transaction where ID='" + transactionId + "'";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<ProductContract> productList = new List<ProductContract>();

                if (reader.HasRows)
                {
                    reader.Read();
                    IList<TransactionDetail> transactionDetails= SerializerHelper.Deserialize<IList<TransactionDetail>>((byte[])reader[3]);
                    
                    StoreTransactionContract transaction = new StoreTransactionContract()
                    {
                        ID = reader[0].ToString(),
                        Name = reader[1].ToString(),
                        Type = (AgencyRole)Int32.Parse(reader[2].ToString()),
                        TransactionDetail = transactionDetails,
                        Shippingfee = Int32.Parse(reader[4].ToString()),
                        Discount = float.Parse(reader[5].ToString())
                    };
                    transaction.Total = transaction.CalculateToTal();
                    return transaction;
                }
                return null;
            }
            catch (MySqlException ex)
            {
                throw new Exception("StoreTransactionServices:GetTransaction", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public IList<StoreTransactionContract> GetTransactionDuringPeriod(DateTime from, DateTime to, AgencyRole? type = null)
        {
            try
            {
                _conObj.Open();
                string sql = "";
                if (type == null)
                {
                    sql = "SELECT * FROM store_transaction where lastupdate >= " + from.ToUnixTime() + " and  lastupdate <= " + to.ToUnixTime();
                }
                else
                {
                    sql = "SELECT * FROM store_transaction where lastupdate >= " + from.ToUnixTime() + " and  lastupdate <= " + to.ToUnixTime() + " and type="+(int)type;
                }
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<StoreTransactionContract> storeTransactions = new List<StoreTransactionContract>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IList<TransactionDetail> transactionDetails = SerializerHelper.Deserialize<IList<TransactionDetail>>((byte[])reader[3]);
                        StoreTransactionContract storeTransaction = new StoreTransactionContract()
                        {
                            ID = reader[0].ToString(),
                            Name = reader[1].ToString(),
                            Type = (AgencyRole)reader[2],
                            TransactionDetail = transactionDetails,
                            Shippingfee = Int32.Parse(reader[4].ToString()),
                            Discount = float.Parse(reader[5].ToString())
                        };
                        storeTransaction.Total = storeTransaction.CalculateToTal();
                        storeTransactions.Add(storeTransaction);                        
                    }
                }
                return storeTransactions;
            }
            catch (MySqlException ex)
            {
                throw new Exception("StoreTransactionServices:GetTransactionDuringPeriod", ex);
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
        public string ID { get; set; }

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

        [ProtoMember(7)]
        public long Total { get; set; }

        public StoreTransactionContract()
        {
            TransactionDetail = new List<TransactionDetail>();
        }
    }

    [Serializable]
    [ProtoContract]
    public class TransactionDetail
    {
        [ProtoMember(1)]
        public ProductContract Product { get; set; }

        [ProtoMember(2)]
        public ProductSize Size { get; set; }

        [ProtoMember(3)]
        public int Quantity { get; set; }        
    }

    public static class StoreTransactionExtension
    {
        public static long CalculateToTal(this StoreTransactionContract storeTransactionContract)
        {
            long total = 0;
            foreach(var item in storeTransactionContract.TransactionDetail)
            {
                if(storeTransactionContract.Type == AgencyRole.Import)
                {
                    total += item.Product.ImportPrice * item.Quantity;
                }else
                {
                    total += item.Product.ExportPrice * item.Quantity;
                }                
            }
            if(storeTransactionContract.Type == AgencyRole.Export)
            {
                total -= (long)(total * storeTransactionContract.Discount);
                total += storeTransactionContract.Shippingfee;
            }
            return total;
        }

        public static List<StoreTransactionContract> GetTransactionList(this IList<StoreTransactionContract> transactions, int offset, int numrow)
        {
            if (transactions.Count > 0)
            {
                return transactions.Skip(offset).Take(numrow).ToList();
            }
            return new List<StoreTransactionContract>();
        }
    }

    public static class TransactionDetailExtension
    {
        public static void AddOrUpdateTransaction(this IList<TransactionDetail> transactionDetails, TransactionDetail item, bool replace = false)
        {
            bool isFound = false;
            TransactionDetail deleteItem = null;
            if(transactionDetails.Count>0)
            {
                foreach(var transaction in transactionDetails)
                {
                    if(transaction.Product.ID == item.Product.ID && transaction.Size == item.Size)
                    {
                        if (replace)
                        {
                            transaction.Quantity = item.Quantity;
                            deleteItem = transaction;
                        }
                        else
                        {
                            transaction.Quantity += item.Quantity;
                        }
                        isFound = true;
                        break;
                    }
                }
            }

            if (!isFound)
            {
                transactionDetails.Add(item);
            }else
            {
                if(deleteItem != null && deleteItem.Quantity == 0)
                {
                    transactionDetails.Remove(deleteItem);
                }
            }
        }

        public static TransactionDetail FindTransaction(this IList<TransactionDetail> transactionDetails, string productId, ProductSize size)
        {   
            if (transactionDetails.Count > 0)
            {
                foreach (var transaction in transactionDetails)
                {
                    if (transaction.Product.ID == productId && transaction.Size == size)
                    {
                        return transaction;
                    }
                }
            }
            return null;
        }

        public static List<TransactionDetail> GetTransactionDetailList(this IList<TransactionDetail> transactionDetails, int offset, int numrow)
        {
            if(transactionDetails.Count>0)
            {
                return transactionDetails.Skip(offset).Take(numrow).ToList();
            }
            return new List<TransactionDetail>();
        }
    }
}