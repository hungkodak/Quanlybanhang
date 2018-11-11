using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quanlybanhang.InternalPage.Transaction
{
    public partial class ManageImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                calendarFrom.SelectedDate = calendarTo.SelectedDate.Add(new TimeSpan(-24, 0, 0));
                calendarTo.SelectedDate = DateTime.UtcNow;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime date = calendarFrom.SelectedDate;
            DateTime date2 = calendarFrom.TodaysDate;

            //if (ValidatorHelper.isBlank(txtProductID.Text) || txtProductID.Text.Length != 4)
            //{
            //    MessageHelper.ShowErrorMessage("Product ID must have 4 unique character.");
            //    return;
            //}

            //if (!_productComponent.IsProductExist(txtProductID.Text))
            //{
            //    MessageHelper.ShowErrorMessage("Product ID isn't correct.");
            //    return;
            //}

            //if (!ValidatorHelper.isNumber(txtQuantity.Text))
            //{
            //    MessageHelper.ShowErrorMessage("Quantity must be a number.");
            //    return;
            //}

            //if (!ValidatorHelper.isAboveZero(txtQuantity.Text))
            //{
            //    MessageHelper.ShowErrorMessage("Quantity must be higher than zero.");
            //    return;
            //}
            //TransactionDetail item = new TransactionDetail()
            //{
            //    Product = _productComponent.GetProduct(txtProductID.Text),
            //    Quantity = UInt32.Parse(txtQuantity.Text)
            //};
            //_currentTransaction.TransactionDetail.AddOrUpdateTransaction(item);
            //if (_storeTransactionComponent.CreateOrUpdateTransaction(_currentTransaction))
            //{
            //    MessageHelper.ShowSucessMessage("Added Item success.");
            //    _pagingHelper.FetchData();
            //    _pagingHelper.CreatePagingControl();
            //}
            //else
            //{
            //    MessageHelper.ShowErrorMessage("Added Item failed.");
            //}
        }
    }
}