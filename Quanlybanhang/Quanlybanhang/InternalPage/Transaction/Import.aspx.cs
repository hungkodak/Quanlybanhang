using Quanlybanhang.Scripts.Source.Components;
using Quanlybanhang.Scripts.Source.DBServices;
using Quanlybanhang.Scripts.Source.Utils;
using Quanlybanhang.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quanlybanhang.InternalPage.Transaction
{
    public partial class Import : System.Web.UI.Page
    {
        protected ProductsComponent _productComponent = new ProductsComponent();
        protected StoreTransactionComponent _storeTransactionComponent = new StoreTransactionComponent();

        protected StoreTransactionContract _currentTransaction
        {
            get
            {
                if (ViewState["_currentTransaction"] == null)
                {
                    StoreTransactionContract transaction = new StoreTransactionContract();
                    transaction.ID = Guid.NewGuid().ToString(); ;                   
                    
                    ViewState["_currentTransaction"] = transaction;
                }
                return ((StoreTransactionContract)ViewState["_currentTransaction"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdded_Click(object sender, EventArgs e)
        {
            if (ValidatorHelper.isBlank(txtProductID.Text) || txtProductID.Text.Length != 4)
            {
                MessageHelper.ShowErrorMessage("Product ID must have 4 unique character.");
                return;
            }

            if (_productComponent.IsProductExist(txtProductID.Text))
            {
                MessageHelper.ShowErrorMessage("Product ID must be unique.");
                return;
            }

            if (!ValidatorHelper.isNumber(txtQuantity.Text))
            {
                MessageHelper.ShowErrorMessage("Quantity must be a number.");
                return;
            }

            if (!ValidatorHelper.isAboveZero(txtQuantity.Text))
            {
                MessageHelper.ShowErrorMessage("Quantity must be higher than zero.");
                return;
            }
            TransactionDetail item = new TransactionDetail()
            {
                Product = _productComponent.GetProduct(txtProductID.Text),
                Quantity = UInt32.Parse(txtQuantity.Text)
            };
            _currentTransaction.TransactionDetail.Add(item);
            if(_storeTransactionComponent.CreateOrUpdateTransaction(_currentTransaction))
            {

            }
        }
    }
}