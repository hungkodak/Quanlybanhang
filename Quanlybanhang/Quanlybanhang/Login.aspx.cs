using Quanlybanhang.Scripts.Source.Components;
using Quanlybanhang.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Quanlybanhang
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (ValidatorHelper.checkName(PlaceHolder1, txtusername.Text.ToString()) && ValidatorHelper.checkName(PlaceHolder1, txtpassword.Text.ToString()))
            {
                if (UserComponent.Login(this, txtusername.Text.ToString(), txtpassword.Text.ToString()))
                {
                    Response.Redirect("default.aspx");
                }
                else
                {
                    ValidatorHelper.showErrorMessage(PlaceHolder1, "Tài khoản hoặc mật khẩu không chính xác");
                }
            }
        }
    }
}