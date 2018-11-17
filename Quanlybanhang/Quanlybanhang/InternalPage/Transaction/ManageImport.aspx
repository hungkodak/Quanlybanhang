<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageImport.aspx.cs" Inherits="Quanlybanhang.InternalPage.Transaction.ManageImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="form-row">
        <div class="form-group col-md-4">            
            <label>From Date</label>
            <asp:Calendar ID="calendarFrom"  runat="server"></asp:Calendar>            
        </div>
        <div class="form-group col-md-4">            
            <label>To Date</label>
            <asp:Calendar ID="calendarTo"  runat="server"></asp:Calendar>            
        </div>
        <div class="form-group col-md-2">
        <label>Action</label>
        <asp:Button ID="btnSearch" runat="server" class="form-control btn btn-primary" 
            Text="Search" onclick="btnSearch_Click"/>
        </div>          
    </div>   
<table class="table">
    <asp:Repeater ID="TransactionRepeater" runat="server" onitemcommand="Transaction_OnItemCommand">
        <HeaderTemplate>
          <thead >
            <tr>
              <th scope="col">ID</th>
              <th scope="col">Name</th>
              <th scope="col">Shippingfee</th>
              <th scope="col">Discount</th>
              <th scope="col">Total</th>
            </tr>
          </thead>
        </HeaderTemplate>        
        <ItemTemplate>  
            <tr>
              <th scope="row"><asp:Label ID="lblIdRpt" runat="server" Text='<%#Eval("ID") %>' /></th>
              <td><asp:Label ID="txtNameRpt" runat="server" Text='<%#Eval("Name") %>' ></asp:Label></td>
              <td><asp:Label ID="txtShippingfeeRpt" runat="server" Text='<%#Eval("Shippingfee") %>' ></asp:Label></td>
              <td><asp:Label ID="txtDiscountRpt" runat="server" Text='<%#Eval("Discount") %>' ></asp:Label></td>
              <td><asp:Label ID="Label1" runat="server" Text='<%#Eval("Total") %>' ></asp:Label></td>
              <td><asp:Button ID="btnEdit" class="btn btn-primary" CommandName="edit" Visible="true" runat="server" Text="Edit"></asp:Button>                  
              </td>
            </tr>
        </ItemTemplate>        
    </asp:Repeater>
</table>
    <asp:PlaceHolder ID="PagingPlaceHolder" runat="server"></asp:PlaceHolder>
</asp:Content>
