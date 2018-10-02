<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Agency.aspx.cs" Inherits="Quanlybanhang.InternalPage.Agency.Agency" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<form>
    <br />
    <div class="form-row">
        <div class="form-group col-sm-2">
            <label>Agency Name</label>                
        </div>
        <div class="form-group col-md-4">            
            <asp:TextBox ID="txtAgencyName" runat="server" class="form-control" placeholder="Enter Agency Name"></asp:TextBox>  
        </div>
        <div class="form-group col-sm-2">
            <label>Agency Type</label>            
        </div>
        <div class="form-group col-md-2">                        
            <asp:DropDownList ID="agencyType" class="form-control" runat="server"></asp:DropDownList>    
        </div>
        <div class="form-group col-md-2">
            <asp:Button ID="btnlogin" runat="server" class="btn btn-primary" 
                Text="Added" onclick="btnAdded_Click"/>
        </div>    
    </div>   
</form>

<table class="table">
    <asp:Repeater ID="AgencyRepeater" runat="server" OnItemDataBound="AgencyRepeater_OnItemDataBound" onitemcommand="AgencyRepeater_OnItemCommand">
        <HeaderTemplate>
          <thead >
            <tr>
              <th scope="col">ID</th>
              <th scope="col">Agency Name</th>
              <th scope="col">Role</th>
              <th scope="col">Action</th>
            </tr>
          </thead>
        </HeaderTemplate>        
        <ItemTemplate>  
            <tr>
              <th scope="row"><asp:Label ID="lblIdRpt" runat="server" Text='<%#Eval("ID") %>' /></th>
              <td><asp:TextBox ID="txtAgencyNameRpt" runat="server" Text='<%#Eval("AgencyName") %>' Enabled="false"></asp:TextBox></td>
              <td><asp:DropDownList ID="agencyTypeRpt" runat="server" Enabled="false" ></asp:DropDownList></td>
              <td><asp:Button ID="btnEdit" CommandName="edit" Visible="true" runat="server" Text="Edit"></asp:Button>
                  <asp:Button ID="btnUpdate" CommandName="update" Visible="false" runat="server" Text="Update"></asp:Button>
                  <asp:Button ID="btnCancel" CommandName="cancel" Visible="false" runat="server" Text="Cancel"></asp:Button>
              </td>
            </tr>
        </ItemTemplate>        
    </asp:Repeater>
</table>
    <asp:PlaceHolder ID="PagingPlaceHolder" runat="server"></asp:PlaceHolder>     
</asp:Content>
