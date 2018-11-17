using Quanlybanhang.Scripts.Source.DBServices;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class StoreTransactionComponent : IDataComponent
    {
        private IList<StoreTransactionContract> _storeTransactions;

        public bool CreateOrUpdateTransaction(StoreTransactionContract transaction)
        {            
            if (StoreTransactionServices.CreateOrUpdateTransaction(transaction))
            {
                return true;
            }
            return false;
        }

        public override IList GetDataByPage(int pagesize, int page)
        {
            if (_storeTransactions == null)
            {
                return Array.Empty<TransactionDetail>();
            }
            if (page == 1)
            {
                return _storeTransactions.GetTransactionList(0, pagesize);
            }
            return _storeTransactions.GetTransactionList((page - 1) * pagesize, pagesize);
        }

        public override int GetTotalPage(int pagesize)
        {
            if (_storeTransactions != null)
            {
                int count = _storeTransactions.Count;

                if (count != 0)
                {
                    return (count / pagesize) + 1;
                }
            }
            return 0;
        }

        public IList<StoreTransactionContract> GetTransactionDuringPeriod(DateTime from, DateTime to, AgencyRole? type = null)
        {
            _storeTransactions =  StoreTransactionServices.GetTransactionDuringPeriod(from, to, type);
            return _storeTransactions;
        }

        public StoreTransactionContract GetTransaction(string transactionId)
        {
            return StoreTransactionServices.GetTransaction(transactionId);
        }
    }
}