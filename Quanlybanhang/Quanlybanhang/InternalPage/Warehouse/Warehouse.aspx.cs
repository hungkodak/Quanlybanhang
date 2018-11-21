using Quanlybanhang.Scripts.Source.Components;
using Quanlybanhang.Scripts.Source.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quanlybanhang.InternalPage.Warehouse
{
    public partial class Warehouse : System.Web.UI.Page
    {
        protected WareHouseComponent _wareHouseComponent = new WareHouseComponent();
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
            _pagingHelper = new PagingHelper(PagingPlaceHolder, _wareHouseComponent, WareHouseRepeater, CurrentPage, SetCurrentPage);
            if (!IsPostBack)
            {
                _pagingHelper.FetchData();
            }
            _pagingHelper.CreatePagingControl();
        }
    }
}