<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Warehouse.aspx.cs" Inherits="Quanlybanhang.InternalPage.Warehouse.Warehouse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="table">
    <asp:Repeater ID="WareHouseRepeater" runat="server">
        <HeaderTemplate>
          <thead >
            <tr>
              <th scope="col">ID</th>
              <th scope="col">Product ID</th>
              <th scope="col">Product Name</th>
              <th scope="col">Size</th>
              <th scope="col">Quantity</th>
            </tr>
          </thead>
        </HeaderTemplate>        
        <ItemTemplate>  
            <tr>
              <th scope="row"><asp:Label ID="lblIdRpt" runat="server" Text='<%#Eval("ID") %>' /></th>
              <td><asp:Label ID="txtProductIdRpt" runat="server" Text='<%#Eval("ProductId") %>' ></asp:Label></td>
              <td><asp:Label ID="txtProductNameRpt" runat="server" Text='<%#Eval("ProductName") %>' ></asp:Label></td>
              <td><asp:Label ID="txtSizeRpt" runat="server" Text='<%#Eval("Size") %>' ></asp:Label></td>
              <td><asp:Label ID="txtQuantityRpt" runat="server" Text='<%#Eval("Quantity") %>' ></asp:Label></td>              
            </tr>
        </ItemTemplate>        
    </asp:Repeater>
</table>
    <asp:PlaceHolder ID="PagingPlaceHolder" runat="server"></asp:PlaceHolder>
</asp:Content>
