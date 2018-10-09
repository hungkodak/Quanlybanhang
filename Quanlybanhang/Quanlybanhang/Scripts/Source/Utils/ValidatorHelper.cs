using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Quanlybanhang.Utils
{
    public class ValidatorHelper
    {
        //-----------------------------------------
        public static Boolean isBlank(string a)
        {
            if (a.Equals("") || a.Equals(null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //-----------------------------------------
        public static Boolean isNumber(string a)
        {
            foreach (Char c in a)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;

        }

        //-----------------------------------------
        public static Boolean isNumberfloat(string a)
        {
            try
            {
                float.Parse(a);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //-----------------------------------------
        public static Boolean isAboveZero(string a)
        {
            if (float.Parse(a) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //-----------------------------------------
        public static Boolean checkName(string a)
        {
            if (isBlank(a))
            {                
                return false;
            }
            else
            {
                return true;
            }
        }

        //-----------------------------------------
        public static Boolean checkQuantity(string a)
        {
            if (isNumber(a) && !isBlank(a))
            {
                if (isAboveZero(a))
                {
                    return true;
                }
                else
                {                    
                    return false;
                }
            }
            else
            {                
                return false;
            }
        }

        //-----------------------------------------
        public static Boolean checkQuantityfloat(string a)
        {
            if (isNumberfloat(a) && !isBlank(a))
            {
                if (isAboveZero(a))
                {
                    return true;
                }
                else
                {                    
                    return false;
                }
            }
            else
            {                
                return false;
            }
        }

        //---------------------------------------
        public static void showSucessMessage(PlaceHolder plh, string message)
        {
            string script = "<style type='text/css'>";
            script += "#messagesucess{top: 0; left: 0}";
            script += "</style>";
            script += "<div id='messagesucess' class='alert alert-success'>";
            script += "<a class='close' data-dismiss='alert' >&times;</a>";
            script += message;
            script += "</div>";

            plh.Controls.Add(new LiteralControl(script));

            //System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('"+message+"')</SCRIPT>");
        }

        //--------------------------------------
        public static void showErrorMessage(PlaceHolder plh, string message)
        {
            string script = "<style type='text/css'>";
            script += "#messageerror{top: 0; left: 0}";
            script += "</style>";
            script += "<div id='messageerror' class='alert alert-danger'>";
            script += "<a class='close' data-dismiss='alert' >&times;</a>";
            script += message;
            script += "</div>";
           

            plh.Controls.Add(new LiteralControl(script));
            //System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('" + message + "')</SCRIPT>");
        }
    }
}