using Quanlybanhang.Scripts.Source.Components;
using Quanlybanhang.Scripts.Source.DBServices;
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
        static protected AgencyComponent _agencyComponent = new AgencyComponent();
        static protected ProductsComponent _productComponent = new ProductsComponent();
        [WebMethod]
        public string[] GetCompletionAgencyImportList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            IList<AgencyContract> listAgency = _agencyComponent.SearchAgencyByName(prefixText, Defination.AgencyRole.Import, count);
            if(listAgency == null || listAgency.Count == 0)
            {
                return new string[0];
            }

            var items = new List<string>(count);
            for (var i = 0; i < listAgency.Count; i++)
            {
                items.Add(listAgency[i].AgencyName);
            }

            return items.ToArray();
        }

        [WebMethod]
        public string[] GetCompletionAgencyExportList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            IList<AgencyContract> listAgency = _agencyComponent.SearchAgencyByName(prefixText, Defination.AgencyRole.Export, count);
            if (listAgency == null || listAgency.Count == 0)
            {
                return new string[0];
            }

            var items = new List<string>(count);
            for (var i = 0; i < listAgency.Count; i++)
            {
                items.Add(listAgency[i].AgencyName);
            }

            return items.ToArray();
        }

        [WebMethod]
        public string[] GetCompletionProductIdList(string prefixText, int count)
        {
            if (count == 0)
            {
                count = 10;
            }
            IList<ProductContract> listProduct = _productComponent.SearchProductByProductId(prefixText, count);
            if (listProduct == null || listProduct.Count == 0)
            {
                return new string[0];
            }

            var items = new List<string>(count);
            for (var i = 0; i < listProduct.Count; i++)
            {
                items.Add(listProduct[i].ID);
            }

            return items.ToArray();
        }
    }
}
