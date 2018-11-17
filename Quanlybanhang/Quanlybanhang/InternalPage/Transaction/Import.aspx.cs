using Quanlybanhang.Scripts.Source.Components;
using Quanlybanhang.Scripts.Source.DBServices;
using Quanlybanhang.Scripts.Source.Defination;
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
        protected PagingHelper _pagingHelper;
        protected TransactionDetailComponent _transactionDetailComponent = new TransactionDetailComponent();
        protected WareHouseComponent _wareHouseComponent = new WareHouseComponent();

        protected List<ProductSizeContract> _productSizeList
        {
            get
            {
                if (ViewState["_productSizeList"] == null)
                {
                    List<ProductSizeContract> list = new List<ProductSizeContract>();
                    int[] itemValues = (int[])Enum.GetValues(typeof(ProductSize));
                    string[] itemNames = Enum.GetNames(typeof(ProductSize));
                    for (int i = 0; i <= itemNames.Length - 1; i++)
                    {
                        ProductSizeContract item = new ProductSizeContract()
                        {
                            Name = itemNames[i],
                            Value = itemValues[i]
                        };
                        list.Add(item);
                    }
                    ViewState["_productSizeList"] = list;
                }
                return ((List<ProductSizeContract>)ViewState["_productSizeList"]);
            }
        }

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

        protected StoreTransactionContract _currentTransaction
        {
            get
            {
                if (ViewState["_currentTransaction"] == null)
                {
                    StoreTransactionContract transaction = new StoreTransactionContract();
                    transaction.ID = Guid.NewGuid().ToString();
                    transaction.Name = "";

                    ViewState["_currentTransaction"] = transaction;
                }
                return ((StoreTransactionContract)ViewState["_currentTransaction"]);
            }
            set
            {
                ViewState["_currentTransaction"] = value;
            }           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lbTransactionID.Text = "Transaction ID:" + _currentTransaction.ID;
            _transactionDetailComponent.Transaction = _currentTransaction;
            _pagingHelper = new PagingHelper(PagingPlaceHolder, _transactionDetailComponent, ImportRepeater, CurrentPage, SetCurrentPage);
            if (!IsPostBack)
            {
                productSize.DataSource = _productSizeList;
                productSize.DataTextField = "Name";
                productSize.DataValueField = "Value";
                productSize.DataBind();
                if (Request.QueryString["id"] != null)
                {
                    _currentTransaction = _storeTransactionComponent.GetTransaction(Request.QueryString["id"]);
                    lbTransactionID.Text = "Transaction ID:" + _currentTransaction.ID;
                    _transactionDetailComponent.Transaction = _currentTransaction;
                    txtAgencyName.Text = _currentTransaction.Name;
                }
                _pagingHelper.FetchData();
            }
            _pagingHelper.CreatePagingControl();
            lbTotal.Text = _currentTransaction.CalculateToTal().ToString();
        }

        protected void btnAdded_Click(object sender, EventArgs e)
        {
            if(ValidatorHelper.isBlank(txtAgencyName.Text))
            {
                MessageHelper.ShowErrorMessage("Agency Name could not be blank.");
                return;
            }

            if (ValidatorHelper.isBlank(txtProductID.Text) || txtProductID.Text.Length != 4)
            {
                MessageHelper.ShowErrorMessage("Product ID must have 4 unique character.");
                return;
            }

            if (!_productComponent.IsProductExist(txtProductID.Text))
            {
                MessageHelper.ShowErrorMessage("Product ID isn't correct.");
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
                Size = (ProductSize)Enum.Parse(typeof(ProductSize), productSize.SelectedValue),
                Quantity = Int32.Parse(txtQuantity.Text)
            };            
            _wareHouseComponent.CreateOrUpdateWareHouse(item);
            _currentTransaction.Name = txtAgencyName.Text;
            _currentTransaction.TransactionDetail.AddOrUpdateTransaction(item);
            if(_storeTransactionComponent.CreateOrUpdateTransaction(_currentTransaction))
            {
                MessageHelper.ShowSucessMessage("Added Item success.");
                _pagingHelper.FetchData();
                _pagingHelper.CreatePagingControl();
                lbTotal.Text = _currentTransaction.CalculateToTal().ToString();
            }
            else
            {
                MessageHelper.ShowErrorMessage("Added Item failed.");
            }
        }
        

        protected void ImportRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lbID = (Label)e.Item.FindControl("lblIdRpt");
            Label txtNameRpt = (Label)e.Item.FindControl("txtNameRpt");
            Label txtImportPriceRpt = (Label)e.Item.FindControl("txtImportPriceRpt");
            TextBox txtQuantityRpt = (TextBox)e.Item.FindControl("txtQuantityRpt");
            Label txtsize = (Label)e.Item.FindControl("sizeRpt");
            Button btnEdit = (Button)e.Item.FindControl("btnEdit");
            Button btnUpdate = (Button)e.Item.FindControl("btnUpdate");
            Button btnCancel = (Button)e.Item.FindControl("btnCancel");

            switch (e.CommandName)
            {
                case "edit":
                    //txtNameRpt.Enabled = true;
                    //txtImportPriceRpt.Enabled = true;
                    txtQuantityRpt.Enabled = true;
                    btnEdit.Visible = false;
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;
                    break;
                case "cancel":
                    //txtNameRpt.Enabled = false;
                    //txtImportPriceRpt.Enabled = false;
                    txtQuantityRpt.Enabled = false;
                    btnEdit.Visible = true;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    break;
                case "update":

                    int quantity = 0;
                    if(!Int32.TryParse(txtQuantityRpt.Text, out quantity))
                    {
                        MessageHelper.ShowErrorMessage("Quantity must be a number and above zero");
                    }
                    else
                    {
                        TransactionDetail item = new TransactionDetail()
                        {
                            Product = _productComponent.GetProduct(lbID.Text),
                            Size = (ProductSize)Enum.Parse(typeof(ProductSize), txtsize.Text),
                            Quantity = quantity
                        };

                        TransactionDetail currentItem = _currentTransaction.TransactionDetail.FindTransaction(item.Product.ID, item.Size);
                        int delta = (int)item.Quantity - (int)currentItem.Quantity;
                        int newQuantity = (int)currentItem.Quantity + delta;
                        
                        if (newQuantity < 0)
                        {
                            MessageHelper.ShowErrorMessage("Don't have enough quantity to change");
                            return;
                        }
                        item.Quantity = delta;
                        currentItem.Quantity = newQuantity;

                        _wareHouseComponent.CreateOrUpdateWareHouse(item);
                        _currentTransaction.TransactionDetail.AddOrUpdateTransaction(currentItem, true);
                        if (_storeTransactionComponent.CreateOrUpdateTransaction(_currentTransaction))
                        {
                            MessageHelper.ShowSucessMessage("Update Item success.");
                            //txtNameRpt.Enabled = false;
                            //txtImportPriceRpt.Enabled = false;
                            txtQuantityRpt.Enabled = false;
                            btnEdit.Visible = true;
                            btnUpdate.Visible = false;
                            btnCancel.Visible = false;
                            _pagingHelper.FetchData();
                            _pagingHelper.CreatePagingControl();
                            lbTotal.Text = _currentTransaction.CalculateToTal().ToString();
                        }
                        else
                        {
                            MessageHelper.ShowErrorMessage("Added Item failed.");
                        }
                    }                    
                    break;
            }
        }

        protected void TxtProductID_TextChanged(object sender, EventArgs e)
        {
            ProductContract product = _productComponent.GetProduct(txtProductID.Text);
            if (product != null)
            {
                txtProductName.Text = product.Name;
            }else
            {
                txtProductName.Text = "";
            }
        }
    }
}