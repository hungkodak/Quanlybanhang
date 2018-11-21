using Quanlybanhang.Scripts.Source.DBServices;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class WareHouseComponent : IDataComponent
    {
        public bool CreateOrUpdateWareHouse(TransactionDetail transactionDetail)
        {
            if(WareHouseServices.CreateOrUpdateWareHouse(transactionDetail))
            {
                return true;
            }
            return false;
        }

        public override IList GetDataByPage(int pagesize, int page)
        {
            if (page == 1)
            {
                return WareHouseServices.GetProductInWareHouseList(0, pagesize);
            }
            return WareHouseServices.GetProductInWareHouseList((page - 1) * pagesize, pagesize);
        }

        public TransactionDetail GetProductFromWareHouseAsTransactionDetail(string productId, ProductSize size)
        {           
            return WareHouseServices.GetProductFromWareHouseAsTransactionDetail(productId,size);
        }

        public override int GetTotalPage(int pagesize)
        {
            int count = WareHouseServices.GetCountTotalProductInWareHouse();

            if (count != 0)
            {
                return (count / pagesize) + 1;
            }

            return 0;
        }
    }
}