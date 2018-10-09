<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Quanlybanhang.InternalPage.Product.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form>
    <br />
    <div class="form-row">
        <div class="form-group col-md-6">            
            <label>Product ID (containt 4 letter and Unique)</label>
            <asp:TextBox ID="txtProductID" runat="server" class="form-control" placeholder="Enter Product ID"></asp:TextBox>  
        </div>
        <div class="form-group col-md-6">            
            <label>Product Name</label>
            <asp:TextBox ID="txtProductName" runat="server" class="form-control" placeholder="Enter Product Name"></asp:TextBox>  
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-6">            
            <label>Import Price</label>
            <asp:TextBox ID="txtImportPrice" runat="server" class="form-control" placeholder="0"></asp:TextBox>  
        </div>
        <div class="form-group col-md-6">            
            <label>Export Price</label>
            <asp:TextBox ID="txtExportPrice" runat="server" class="form-control" placeholder="0"></asp:TextBox>  
        </div>
    </div>        
    <div class="form-row">
        <asp:Button ID="btnAdded" runat="server" class="btn btn-primary" 
            Text="Added" onclick="btnAdded_Click"/>
    </div>          
</form>

<table class="table">
    <asp:Repeater ID="ProductRepeater" runat="server" onitemcommand="ProductRepeater_OnItemCommand">
        <HeaderTemplate>
          <thead >
            <tr>
              <th scope="col">ID</th>
              <th scope="col">Name</th>
              <th scope="col">Import Price</th>
              <th scope="col">Export Price</th>
            </tr>
          </thead>
        </HeaderTemplate>        
        <ItemTemplate>  
            <tr>
              <th scope="row"><asp:Label ID="lblIdRpt" runat="server" Text='<%#Eval("ID") %>' /></th>
              <td><asp:TextBox ID="txtNameRpt" runat="server" Text='<%#Eval("Name") %>' Enabled="false"></asp:TextBox></td>
              <td><asp:TextBox ID="txtImportPriceRpt" runat="server" Text='<%#Eval("ImportPrice") %>' Enabled="false"></asp:TextBox></td>
              <td><asp:TextBox ID="txtExportPriceRpt" runat="server" Text='<%#Eval("ExportPrice") %>' Enabled="false"></asp:TextBox></td>
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
