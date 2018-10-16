<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="Quanlybanhang.InternalPage.Transaction.Import" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<form>
    <br />
    <div class="form-row">
        <div class="form-group col-md-3">            
            <label>Product ID (containt 4 letter and Unique)</label>
            <asp:TextBox ID="txtProductID" runat="server" class="form-control" placeholder="Enter Product ID"></asp:TextBox>  
        </div>
        <div class="form-group col-md-6">            
            <label>Product Name</label>
            <asp:TextBox ID="txtProductName" runat="server" class="form-control" placeholder="Enter Product Name"></asp:TextBox>  
        </div>
        <div class="form-group col-md-3">            
            <label>Quantity</label>
            <asp:TextBox ID="txtQuantity" runat="server" class="form-control" placeholder="1">1</asp:TextBox>  
        </div>
    </div>      
    <div class="form-row">
        <asp:Button ID="btnAdded" runat="server" class="btn btn-primary" 
            Text="Added" onclick="btnAdded_Click"/>
    </div>          
</form>

<%--<table class="table">
    <asp:Repeater ID="ImportRepeater" runat="server" onitemcommand="ImportRepeater_OnItemCommand">
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
              <th scope="row"><asp:Label ID="lblIdRpt" runat="server" Text='<%#Eval("TransactionDetail.Product.ID") %>' /></th>
              <td><asp:TextBox ID="txtNameRpt" runat="server" Text='<%#Eval("TransactionDetail.Product.Name") %>' Enabled="false"></asp:TextBox></td>
              <td><asp:TextBox ID="txtImportPriceRpt" runat="server" Text='<%#Eval("TransactionDetail.Product.ImportPrice") %>' Enabled="false"></asp:TextBox></td>
              <td><asp:TextBox ID="txtExportPriceRpt" runat="server" Text='<%#Eval("TransactionDetail.Product.ExportPrice") %>' Enabled="false"></asp:TextBox></td>
              <td><asp:Button ID="btnEdit" class="btn btn-primary" CommandName="edit" Visible="true" runat="server" Text="Edit"></asp:Button>
                  <asp:Button ID="btnUpdate" class="btn btn-primary" CommandName="update" Visible="false" runat="server" Text="Update"></asp:Button>
                  <asp:Button ID="btnCancel" class="btn btn-primary" CommandName="cancel" Visible="false" runat="server" Text="Cancel"></asp:Button>
              </td>
            </tr>
        </ItemTemplate>        
    </asp:Repeater>
</table>
    <asp:PlaceHolder ID="PagingPlaceHolder" runat="server"></asp:PlaceHolder>--%>
</asp:Content>
