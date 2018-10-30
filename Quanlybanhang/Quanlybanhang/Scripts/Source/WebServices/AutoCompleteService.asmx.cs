using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Quanlybanhang.Scripts.Source.WebServices
{
    /// <summary>
    /// Summary description for AutoCompleteService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteService : System.Web.Services.WebService
    {
        [WebMethod]
        public string[] GetCompletionAgencyImportList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }

            if (prefixText.Equals("xyz"))
            {
                return new string[0];
            }

            var random = new Random();
            var items = new List<string>(count);
            for (var i = 0; i < count; i++)
            {
                var c1 = (char)random.Next(65, 90);
                var c2 = (char)random.Next(97, 122);
                var c3 = (char)random.Next(97, 122);

                items.Add(prefixText + c1 + c2 + c3);
            }

            return items.ToArray();
        }
    }
}
