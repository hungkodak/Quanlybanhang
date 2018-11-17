using Quanlybanhang.Scripts.Source.Components;
using Quanlybanhang.Scripts.Source.Utils;
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
        protected PagingHelper _pagingHelper;
        protected StoreTransactionComponent _storeTransactionComponent = new StoreTransactionComponent();
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
            _pagingHelper = new PagingHelper(PagingPlaceHolder, _storeTransactionComponent, TransactionRepeater, CurrentPage, SetCurrentPage);
            if (!IsPostBack)
            {
                calendarTo.SelectedDate = DateTime.UtcNow;
                calendarFrom.SelectedDate = calendarTo.SelectedDate.Add(new TimeSpan(-24, 0, 0));
                _storeTransactionComponent.GetTransactionDuringPeriod(calendarFrom.SelectedDate, calendarTo.SelectedDate, Scripts.Source.Defination.AgencyRole.Import);
                _pagingHelper.FetchData();
            }
            _pagingHelper.CreatePagingControl();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            _storeTransactionComponent.GetTransactionDuringPeriod(calendarFrom.SelectedDate, calendarTo.SelectedDate, Scripts.Source.Defination.AgencyRole.Import);
            _pagingHelper.FetchData();
            _pagingHelper.CreatePagingControl();
        }

        protected void Transaction_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lbID = (Label)e.Item.FindControl("lblIdRpt");            

            if(e.CommandName.Equals("edit"))
            {
                Response.Redirect("Import.aspx?id=" + lbID.Text.ToString());
            }            
        }
    }
}