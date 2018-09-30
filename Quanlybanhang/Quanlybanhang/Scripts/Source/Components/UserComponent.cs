using Quanlybanhang.Scripts.Source.DBServices;
using Quanlybanhang.Scripts.Source.Defination;
using System.Web.UI;

namespace Quanlybanhang.Scripts.Source.Components
{
    public class UserComponent
    {
        public static bool IsUserLogin(Page page)
        {
            if (page.Session["username"] == null)
            {
                return false;
            }
            return true;
        }

        public static bool Login(Page page, string username, string password)
        {
            UserContract user = AccountsServices.GetUser(username, password);
            if(user.Role != AdminRole.None)
            {
                page.Session["username"] = user.UserName;
                page.Session["name"] = user.Name;
                page.Session["role"] = user.Role;
                return true;
            }
            return false;
        }
    }
}