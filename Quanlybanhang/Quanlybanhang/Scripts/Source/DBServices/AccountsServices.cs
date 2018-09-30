using MySql.Data.MySqlClient;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.DBServices
{
    public class AccountsServices
    {
        static string _conStr = ConfigurationManager.ConnectionStrings["construsermanagement"].ConnectionString;
        static MySqlConnection _conObj = new MySqlConnection(_conStr);
        static public UserContract GetUser(string username, string password)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM usermanagement.accounts where username = '" + username + "' and password = MD5('" + password + "') limit 1";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                UserContract userContract = new UserContract();
                
                if (reader.HasRows)
                {
                    reader.Read();
                    userContract.UserName = reader[0].ToString();
                    userContract.Name = reader[1].ToString();
                    userContract.Role = (AdminRole)reader[3];

                }                
                return userContract;
            }
            catch (MySqlException ex)
            {
                throw new Exception("AccountsQuery:GetUser", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }
    }

    public class UserContract
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public AdminRole Role { get; set; }

        public UserContract()
        {
            Role = AdminRole.None;
        }
    }
}