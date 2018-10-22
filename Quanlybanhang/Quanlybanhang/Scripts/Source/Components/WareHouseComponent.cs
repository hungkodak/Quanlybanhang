using Quanlybanhang.Scripts.Source.DBServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class WareHouseComponent
    {
        public bool CreateOrUpdateWareHouse(TransactionDetail transactionDetail)
        {
            if(WareHouseServices.CreateOrUpdateWareHouse(transactionDetail))
            {
                return true;
            }
            return false;
        }
    }
}