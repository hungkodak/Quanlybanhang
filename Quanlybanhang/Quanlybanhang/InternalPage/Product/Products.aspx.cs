using Quanlybanhang.Scripts.Source.Components;
using Quanlybanhang.Scripts.Source.Utils;
using Quanlybanhang.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quanlybanhang.InternalPage.Product
{
    public partial class Products : System.Web.UI.Page
    {
        protected ProductsComponent _productComponent = new ProductsComponent();
        protected PagingHelper _pagingHelper;
        public int CurrentPage
        {
            get
            {
                object obj = ViewState["CurrentPage"];
                return (obj == null) ? 1 : (int)obj;
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        public void SetCurrentPage(int page)
        {
            CurrentPage = page;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _pagingHelper = new PagingHelper(PagingPlaceHolder, _productComponent, ProductRepeater, CurrentPage, SetCurrentPage);
            if (!IsPostBack)
            {
                _pagingHelper.FetchData();
            }
            _pagingHelper.CreatePagingControl();
        }

        protected void btnAdded_Click(object sender, EventArgs e)
        {
            if(ValidatorHelper.isBlank(txtProductID.Text) || txtProductID.Text.Length != 4)
            {
                MessageHelper.ShowErrorMessage("Product ID must have 4 unique character.");
                return;
            }
            if(ValidatorHelper.isBlank(txtProductName.Text))
            {
                MessageHelper.ShowErrorMessage("Product Name can not be blank.");
                return;
            }

            if (!ValidatorHelper.checkQuantity(txtImportPrice.Text))
            {
                MessageHelper.ShowErrorMessage("Import Price must be a number and above zero.");
                return;
            }

            if (!ValidatorHelper.checkQuantity(txtExportPrice.Text))
            {
                MessageHelper.ShowErrorMessage("Export Price must be a number and above zero.");
                return;
            }

            if (_productComponent.CreateProduct(txtProductID.Text, txtProductName.Text, Int32.Parse(txtExportPrice.Text), Int32.Parse(txtImportPrice.Text)))
            {
                MessageHelper.ShowSucessMessage("Create Product " + txtProductID.Text + "sucessfull");
                _pagingHelper.FetchData();
                _pagingHelper.CreatePagingControl();
            }
            else
            {
                MessageHelper.ShowErrorMessage("Create Agency Error");
            }
        }        

        protected void ProductRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lbID = (Label)e.Item.FindControl("lblIdRpt");
            TextBox txtNameRpt = (TextBox)e.Item.FindControl("txtNameRpt");
            TextBox txtImportPriceRpt = (TextBox)e.Item.FindControl("txtImportPriceRpt");
            TextBox txtExportPriceRpt = (TextBox)e.Item.FindControl("txtExportPriceRpt");            
            Button btnEdit = (Button)e.Item.FindControl("btnEdit");
            Button btnUpdate = (Button)e.Item.FindControl("btnUpdate");
            Button btnCancel = (Button)e.Item.FindControl("btnCancel");

            switch (e.CommandName)
            {
                case "edit":
                    txtNameRpt.Enabled = true;
                    txtImportPriceRpt.Enabled = true;
                    txtExportPriceRpt.Enabled = true;
                    btnEdit.Visible = false;
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;
                    break;
                case "cancel":
                    txtNameRpt.Enabled = false;
                    txtImportPriceRpt.Enabled = false;
                    txtExportPriceRpt.Enabled = false;
                    btnEdit.Visible = true;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    break;
                case "update":
                    if (_productComponent.UpdateProduct(lbID.Text, txtNameRpt.Text, Int32.Parse(txtExportPriceRpt.Text), Int32.Parse(txtImportPriceRpt.Text)))
                    {
                        txtNameRpt.Enabled = false;
                        txtImportPriceRpt.Enabled = false;
                        txtExportPriceRpt.Enabled = false;
                        btnEdit.Visible = true;
                        btnUpdate.Visible = false;
                        btnCancel.Visible = false;
                        _pagingHelper.FetchData();
                        _pagingHelper.CreatePagingControl();
                        MessageHelper.ShowSucessMessage("Update Product " + lbID.Text + " sucessfull");
                    }
                    else
                    {
                        MessageHelper.ShowErrorMessage("Update Product Error");
                    }
                    break;
            }
        }
    }
}