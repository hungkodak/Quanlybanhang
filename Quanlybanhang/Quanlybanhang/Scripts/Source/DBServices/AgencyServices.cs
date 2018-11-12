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
                string sql = "INSERT INTO agency(name, type, lastupdate) VALUES('" + agency.AgencyName + "', " + (int)agency.Role + ", '" + DateTime.UtcNow.ToUnixTime() + "')";
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

        static public bool UpdateAgency(AgencyContract agency)
        {
            try
            {
                _conObj.Open();
                string sql = "UPDATE agency SET name = '" + agency.AgencyName + "', type = " + (int)agency.Role + ", lastupdate = '" + DateTime.UtcNow.ToUnixTime() + "' WHERE ID =" + agency.ID;
                MySqlCommand cmd = new MySqlCommand(sql, _conObj);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception("AgencyServices:UpdateAgency", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public List<AgencyContract> GetAgencyList(int offset, int numrow)
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT * FROM agency limit " + offset + "," + numrow;
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

        static public int GetCountTotalAgency()
        {
            try
            {
                _conObj.Open();
                string sql = "SELECT Count(*) FROM agency";
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
                throw new Exception("AgencyServices:GetAgencyListByRole", ex);
            }
            finally
            {
                _conObj.Close();
            }
        }

        static public List<AgencyContract> SearchAgencyByName(string name, AgencyRole? role, int limit=1)
        {
            try
            {
                _conObj.Open();
                string sql = "";
                if (role == null)
                {
                    sql = "SELECT * FROM agency where name like '%" + name + "%' limit "+limit;
                }
                else
                {
                    sql = "SELECT * FROM agency where name like '%" + name + "%' and type = " + (int)role + " limit " + limit;
                }
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
                throw new Exception("AgencyServices:SearchAgencyByName", ex);
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