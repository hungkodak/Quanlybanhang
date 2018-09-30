using Quanlybanhang.Scripts.Source.Components;
using Quanlybanhang.Scripts.Source.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quanlybanhang
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageHelper.SetPlaceHolder(PlaceHolderMessage);
            //if (!UserComponent.IsUserLogin(this.Page))
            //{
            //    Response.Redirect("~/login.aspx");
            //}
        }
    }
}