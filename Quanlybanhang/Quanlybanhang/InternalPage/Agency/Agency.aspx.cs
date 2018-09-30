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

namespace Quanlybanhang.InternalPage.Agency
{
    public partial class Agency : System.Web.UI.Page
    {
        protected List<ListItem> _agencyRoleList = new List<ListItem>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                int[] itemValues = (int[])Enum.GetValues(typeof(AgencyRole));
                string[] itemNames = Enum.GetNames(typeof(AgencyRole));

                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    ListItem item = new ListItem(itemNames[i], itemValues[i].ToString());
                    _agencyRoleList.Add(item);                   
                }
                agencyType.Items.AddRange(_agencyRoleList.ToArray());
                FetchData();
            }
            
        }

        private void FetchData()
        {
            AgencyRepeater.DataSource = AgencyComponent.GetAllAgency();
            AgencyRepeater.DataBind();
        }

        protected void AgencyRepeater_OnItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {            
            DropDownList agencyDropDownList = (DropDownList)e.Item.FindControl("agencyTypeRpt");
            if (agencyDropDownList != null)
            {
                agencyDropDownList.Items.AddRange(_agencyRoleList.ToArray());
                agencyDropDownList.SelectedValue = ((int)((AgencyContract)e.Item.DataItem).Role).ToString();
            }            
        }

        protected void AgencyRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            TextBox txtAgencyNameRpt = (TextBox)e.Item.FindControl("txtAgencyNameRpt");
            DropDownList agencyTypeRpt = (DropDownList)e.Item.FindControl("agencyTypeRpt");
            Button btnEdit = (Button)e.Item.FindControl("btnEdit");
            Button btnUpdate = (Button)e.Item.FindControl("btnUpdate");
            Button btnCancel = (Button)e.Item.FindControl("btnCancel");



            switch (e.CommandName)
            {
                case "edit":
                    txtAgencyNameRpt.Enabled = true;
                    agencyTypeRpt.Enabled = true;
                    btnEdit.Visible = false;
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;

                    //if (accountsql.deleteAccount(Convert.ToInt32(l.Value.ToString())))
                    //{
                    //    validate.showSucessMessage(PlaceHolder1, "Bạn đã xóa thành công");
                    //    FetchData();
                    //    PlaceHolder1.Controls.Clear();
                    //    CreatePagingControl();
                    //}
                    //else
                    //{
                    //    validate.showErrorMessage(PlaceHolder1, "Đã có lỗi xảy ra vui lòng liên hệ người viết chương trình");
                    //}
                    break;
                case "cancel":
                    txtAgencyNameRpt.Enabled = false;
                    agencyTypeRpt.Enabled = false;
                    btnEdit.Visible = true;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    break;
                case "update":
                    break;
            }
        }

        protected void btnAdded_Click(object sender, EventArgs e)
        {
            if (!ValidatorHelper.isBlank(txtAgencyName.Text.ToString()))
            {
                if (AgencyComponent.CreateAgency(txtAgencyName.Text.ToString(), (AgencyRole)Int32.Parse(agencyType.SelectedValue)))
                {
                    MessageHelper.ShowSucessMessage("Create Agency " + txtAgencyName.Text + "sucessfull");
                }
                else
                {
                    MessageHelper.ShowErrorMessage("Create Agency Error");
                }
            }
            else
            {
                MessageHelper.ShowErrorMessage("Agency Name is empty");
            }
        }
    }
}