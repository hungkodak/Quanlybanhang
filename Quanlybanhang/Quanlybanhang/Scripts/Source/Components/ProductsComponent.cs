using Quanlybanhang.Scripts.Source.DBServices;
using Quanlybanhang.Scripts.Source.Defination;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class ProductsComponent : IDataComponent
    {             

        public bool CreateProduct(string id, string name, int exportprice, int importprice)
        {
            ProductContract product = new ProductContract()
            {
                ID = id,
                Name = name,
                ExportPrice = exportprice,
                ImportPrice = importprice
            };
            if(ProductsServices.CreateProduct(product))
            {
                return true;
            }
            return false;
        }

        public bool IsProductExist(string productId)
        {
            ProductContract product = ProductsServices.GetProduct(productId);
                        
            if (product != null)
            {
                return true;
            }
            return false;
        }

        public ProductContract GetProduct(string productId)
        {
            ProductContract product = ProductsServices.GetProduct(productId);

            if (product != null)
            {
                return product;
            }
            return null;
        }

        public bool UpdateProduct(string id, string name, int exportprice, int importprice)
        {
            ProductContract product = new ProductContract()
            {
                ID = id,
                Name = name,
                ExportPrice = exportprice,
                ImportPrice = importprice
            };
            if (ProductsServices.UpdateProduct(product))
            {
                return true;
            }
            return false;
        }

        public override IList GetDataByPage(int pagesize, int page)
        {
            if (page == 1)
            {
                return ProductsServices.GetProductList(0, pagesize);
            }
            return ProductsServices.GetProductList((page - 1) * pagesize, pagesize);
        }

        public override int GetTotalPage(int pagesize)
        {
            int count = ProductsServices.GetCountTotalProduct();

            if (count != 0)
            {
                return (count / pagesize) + 1;
            }

            return 0;
        }
    }
}