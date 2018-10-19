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
    <asp:Repeater ID="TransactionRepeater" runat="server"> <%--onitemcommand="ImportRepeater_OnItemCommand">--%>
        <HeaderTemplate>
          <thead >
            <tr>
              <th scope="col">ID</th>
              <th scope="col">Name</th>
              <%--<th scope="col">Import Price</th>
              <th scope="col">Export Price</th>--%>
            </tr>
          </thead>
        </HeaderTemplate>        
        <ItemTemplate>  
            <tr>
              <th scope="row"><asp:Label ID="lblIdRpt" runat="server" Text='<%#Eval("ID") %>' /></th>
              <td><asp:TextBox ID="txtNameRpt" runat="server" Text='<%#Eval("Name") %>' Enabled="false"></asp:TextBox></td>
              <%--<td><asp:TextBox ID="txtImportPriceRpt" runat="server" Text='<%#Eval("Product.ImportPrice") %>' Enabled="false"></asp:TextBox></td>
              <td><asp:TextBox ID="txtQuantityRpt" runat="server" Text='<%#Eval("Quantity") %>' Enabled="false"></asp:TextBox></td>--%>
              <td><asp:Button ID="btnEdit" class="btn btn-primary" CommandName="edit" Visible="true" runat="server" Text="Edit"></asp:Button>
                  <asp:Button ID="btnUpdate" class="btn btn-primary" CommandName="update" Visible="false" runat="server" Text="Update"></asp:Button>
                  <asp:Button ID="btnCancel" class="btn btn-primary" CommandName="cancel" Visible="false" runat="server" Text="Cancel"></asp:Button>
              </td>
            </tr>
        </ItemTemplate>        
    </asp:Repeater>
</table>
    <asp:PlaceHolder ID="PagingPlaceHolder" runat="server"></asp:PlaceHolder>
</asp:Content>
