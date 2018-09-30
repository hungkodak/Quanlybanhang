using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Quanlybanhang.Scripts.Source.Defination;
using Quanlybanhang.Scripts.Source.Utils;

namespace Quanlybanhang.Scripts.Source.DBServices
{
    public class AgencyServices
    {
        static string _conStr = ConfigurationManager.ConnectionStrings["constrstore"].ConnectionString;
        static MySqlConnection _conObj = new MySqlConnection(_conStr);

        static public bool CreateAgency(AgencyContract agency)
        {
            try
            {
                _conObj.Open();                
                string sql = "INSERT INTO agency(name, type, lastupdate) VALUES('" + agency.AgencyName + "', " + (int)agency.Role + ", '" + DateTime.UtcNow.ToMySQLDateTimeString() + "')";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("AgencyServices:CreateAgency", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public List<AgencyContract> GetAgencyList(bool isLimit,)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM agency order by lastupdate";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<AgencyContract> agencyList = new List<AgencyContract>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AgencyContract agency = new AgencyContract()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            AgencyName = reader[1].ToString(),
                            Role = (AgencyRole)reader[2]
                        };
                        agencyList.Add(agency);
                    }
                }
                return agencyList;
            }
            catch (MySqlException ex)
            {
                throw new Exception("AgencyServices:GetAgencyList", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public List<AgencyContract> GetAgencyListByRole(AgencyRole role)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM agency where type = " + (int)role + "order by lastupdate";
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                MySqlDataReader reader = cmd.ExecuteReader();
                List<AgencyContract> agencyList = new List<AgencyContract>();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        AgencyContract agency = new AgencyContract()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            AgencyName = reader[1].ToString(),
                            Role = (AgencyRole)reader[2]
                    };
                        agencyList.Add(agency);
                    }
                }
                return agencyList;
            }
            catch (MySqlException ex)
            {
                throw new Exception("AgencyServices:GetAgencyList", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }
    }

    public class AgencyContract
    {
        public int ID { get; set; }
        public string AgencyName { get; set; }        
        public AgencyRole Role { get; set; }

        public AgencyContract()
        {
            Role = AgencyRole.Import;
        }
    }
}