using Quanlybanhang.Scripts.Source.DBServices;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class AgencyComponent: IDataComponent
    {             

        public bool CreateAgency(string name, AgencyRole role)
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

        public bool UpdateAgency(int id, string name, AgencyRole role)
        {
            AgencyContract agency = new AgencyContract()
            {
                ID = id,
                AgencyName = name,
                Role = role
            };
            if (AgencyServices.UpdateAgency(agency))
            {
                return true;
            }
            return false;
        }

        public override IList GetDataByPage(int pagesize, int page)
        {
            if (page == 1)
            {
                return AgencyServices.GetAgencyList(0, pagesize);
            }
            return AgencyServices.GetAgencyList((page - 1) * pagesize, pagesize);
        }

        public override int GetTotalPage(int pagesize)
        {
            int count = AgencyServices.GetCountTotalAgency();

            if (count != 0)
            {
                return (count / pagesize) + 1;
            }

            return 0;
        }
    }
}