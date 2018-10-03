using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Defination
{    
    public enum AgencyRole
    {        
        Import = 0,
        Export = 1
    }

    [Serializable]
    public class AgencyRoleContract
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}