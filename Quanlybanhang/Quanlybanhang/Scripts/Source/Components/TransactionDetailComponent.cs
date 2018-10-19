using Quanlybanhang.Scripts.Source.DBServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class TransactionDetailComponent : IDataComponent
    {
        public StoreTransactionContract Transaction { get; set; }

        public override IList GetDataByPage(int pagesize, int page)
        {
            if(Transaction == null)
            {
                return Array.Empty<TransactionDetail>();
            }
            if (page == 1)
            {
                return Transaction.TransactionDetail.GetTransactionDetailList(0, pagesize);
            }
            return Transaction.TransactionDetail.GetTransactionDetailList((page - 1) * pagesize, pagesize);            
        }

        public override int GetTotalPage(int pagesize)
        {
            if(Transaction!=null && Transaction.TransactionDetail!=null)
            {
                int count = Transaction.TransactionDetail.Count;

                if (count != 0)
                {
                    return (count / pagesize) + 1;
                }
            }
            return 0;
        }
    }
}