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
    public partial class Agency : Page
    {
        protected AgencyComponent _agencyComponent = new AgencyComponent();
        protected List<AgencyRoleContract> _agencyRoleList
        {
            get
            {
                if(ViewState["_agencyRoleList"] == null)
                {
                    List<AgencyRoleContract> list = new List<AgencyRoleContract>();
                    int[] itemValues = (int[])Enum.GetValues(typeof(AgencyRole));
                    string[] itemNames = Enum.GetNames(typeof(AgencyRole));
                    for (int i = 0; i <= itemNames.Length - 1; i++)
                    {
                        AgencyRoleContract item = new AgencyRoleContract()
                        {
                            Name =  itemNames[i],
                            Value = itemValues[i]
                        };
                        list.Add(item);
                    }
                    ViewState["_agencyRoleList"] = list;
                }
                return ((List<AgencyRoleContract>)ViewState["_agencyRoleList"]);
            }
        }
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
            _pagingHelper = new PagingHelper(PagingPlaceHolder, _agencyComponent, AgencyRepeater, CurrentPage, SetCurrentPage);
            if (!IsPostBack)
            {
                agencyType.DataSource = _agencyRoleList;
                agencyType.DataTextField = "Name";
                agencyType.DataValueField = "Value";
                agencyType.DataBind();
                _pagingHelper.FetchData();
            }
            _pagingHelper.CreatePagingControl();

        }

        protected void AgencyRepeater_OnItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {            
            DropDownList agencyDropDownList = (DropDownList)e.Item.FindControl("agencyTypeRpt");
            if (agencyDropDownList != null)
            {
                agencyDropDownList.DataSource = _agencyRoleList;
                agencyDropDownList.DataTextField = "Name";
                agencyDropDownList.DataValueField = "Value";
                agencyDropDownList.DataBind();
                agencyDropDownList.SelectedIndex = (int)((AgencyContract)e.Item.DataItem).Role;
            }            
        }

        protected void AgencyRepeater_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lbAgencyID = (Label)e.Item.FindControl("lblIdRpt");
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
                    break;
                case "cancel":
                    txtAgencyNameRpt.Enabled = false;
                    agencyTypeRpt.Enabled = false;
                    btnEdit.Visible = true;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    break;
                case "update":
                    if(_agencyComponent.UpdateAgency(Int32.Parse(lbAgencyID.Text), txtAgencyNameRpt.Text, (AgencyRole)Enum.Parse(typeof(AgencyRole),agencyTypeRpt.SelectedValue)))
                    {
                        txtAgencyNameRpt.Enabled = false;
                        agencyTypeRpt.Enabled = false;
                        btnEdit.Visible = true;
                        btnUpdate.Visible = false;
                        btnCancel.Visible = false;                        
                        _pagingHelper.FetchData();
                        _pagingHelper.CreatePagingControl();
                        MessageHelper.ShowSucessMessage("Update Agency " + txtAgencyName.Text + "sucessfull");
                    }
                    else
                    {
                        MessageHelper.ShowErrorMessage("Update Agency Error");
                    }                    
                    break;
            }
        }

        protected void btnAdded_Click(object sender, EventArgs e)
        {
            if (!ValidatorHelper.isBlank(txtAgencyName.Text.ToString()))
            {
                if (_agencyComponent.CreateAgency(txtAgencyName.Text.ToString(), (AgencyRole)Int32.Parse(agencyType.SelectedValue)))
                {
                    MessageHelper.ShowSucessMessage("Create Agency " + txtAgencyName.Text + "sucessfull");
                    _pagingHelper.FetchData();
                    _pagingHelper.CreatePagingControl();
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