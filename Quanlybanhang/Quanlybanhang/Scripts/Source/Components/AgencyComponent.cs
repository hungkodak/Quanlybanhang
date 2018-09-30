using Quanlybanhang.Scripts.Source.DBServices;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class AgencyComponent
    {
        public static bool CreateAgency(string name, AgencyRole role)
        {
            AgencyContract agency = new AgencyContract()
            {
                AgencyName = name,
                Role = role
            };
            if(AgencyServices.CreateAgency(agency))
            {
                return true;
            }
            return false;
        }

        public static List<AgencyContract> GetAllAgency()
        {
            return AgencyServices.GetAgencyList();
        }
    }
}