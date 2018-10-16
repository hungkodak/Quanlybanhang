using Quanlybanhang.Scripts.Source.DBServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class StoreTransactionComponent
    {
        public bool CreateOrUpdateTransaction(StoreTransactionContract transaction)
        {            
            if (StoreTransactionServices.CreateOrUpdateTransaction(transaction))
            {
                return true;
            }
            return false;
        }
    }
}