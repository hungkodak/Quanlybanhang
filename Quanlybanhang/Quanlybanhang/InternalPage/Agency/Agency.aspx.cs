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
        protected AgencyComponent _agencyComponent = new AgencyComponent();
        protected List<ListItem> _agencyRoleList = new List<ListItem>();
        protected PagingHelper _pagingHelper;
        protected void Page_Load(object sender, EventArgs e)
        {
            _pagingHelper = new PagingHelper(this, PagingPlaceHolder, _agencyComponent, AgencyRepeater);
            if (!IsPostBack)
            {
                int[] itemValues = (int[])Enum.GetValues(typeof(AgencyRole));
                string[] itemNames = Enum.GetNames(typeof(AgencyRole));

                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    ListItem item = new ListItem(itemNames[i], itemValues[i].ToString());
                    _agencyRoleList.Add(item);
                }
                Session["_agencyRoleList"] = _agencyRoleList;
                agencyType.DataSource = _agencyRoleList;
                agencyType.DataBind();
                _pagingHelper.FetchData();
                this.ViewState["s"];

                //Session["_pagingHelper"] = _pagingHelper;
            }
            else
            {
                _agencyRoleList = (List<ListItem>)Session["_agencyRoleList"];
                //_pagingHelper = (PagingHelper)Session["_pagingHelper"];
                //_pagingHelper.FetchData();
                //_pagingHelper.CreatePagingControl();
            }
            
            
            _pagingHelper.CreatePagingControl();

        }

        protected void AgencyRepeater_OnItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {            
            DropDownList agencyDropDownList = (DropDownList)e.Item.FindControl("agencyTypeRpt");
            if (agencyDropDownList != null)
            {
                agencyDropDownList.DataSource = _agencyRoleList;
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