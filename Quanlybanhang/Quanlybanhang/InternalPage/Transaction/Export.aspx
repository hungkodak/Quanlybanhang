<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="Quanlybanhang.InternalPage.Transaction.Export" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form>
    <br />
    <div class="form-group">
        <label><asp:Label ID="lbTransactionID" runat="server" Text="TransactionID:"></asp:Label></label>
    </div>
    <div class="form-row">
        <div class="form-group col-md-2">            
            <label>Agency Name</label>
            <asp:TextBox ID="txtAgencyName" runat="server" class="form-control" placeholder="Enter Agency Name"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender
                runat="server" 
                BehaviorID="AutoCompleteEx"
                ID="autoComplete1" 
                TargetControlID="txtAgencyName"
                ServicePath="~/Scripts/Source/WebServices/AutoCompleteService.asmx" 
                ServiceMethod="GetCompletionAgencyExportList"
                MinimumPrefixLength="1" 
                CompletionInterval="100"
                EnableCaching="true"
                CompletionSetCount="5"                                
                ShowOnlyCurrentWordInCompletionListItem="true" />
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-2">            
            <label>Product ID</label>
            <asp:TextBox ID="txtProductID" runat="server" class="form-control" placeholder="Enter Product ID" AutoPostBack="True" OnTextChanged ="TxtProductID_TextChanged"></asp:TextBox> 
            <ajaxToolkit:AutoCompleteExtender
                runat="server" 
                BehaviorID="AutoCompleteEx"
                ID="AutoCompleteExtender1" 
                TargetControlID="txtProductID"
                ServicePath="~/Scripts/Source/WebServices/AutoCompleteService.asmx" 
                ServiceMethod="GetCompletionProductIdList"                
                MinimumPrefixLength="1" 
                CompletionInterval="100"
                EnableCaching="true"
                CompletionSetCount="5"                                
                ShowOnlyCurrentWordInCompletionListItem="true" />
        </div>
        <div class="form-group col-md-2">            
            <label>Product Name</label>
            <asp:TextBox ID="txtProductName" runat="server" class="form-control" placeholder="" Enabled="false"></asp:TextBox>  
        </div>
        <div class="form-group col-md-2">            
            <label>Quantity</label>
            <asp:TextBox ID="txtQuantity" runat="server" class="form-control" placeholder="1">1</asp:TextBox>  
        </div>
        <div class="form-group col-md-2">
            <label>Size</label>                                         
            <asp:DropDownList ID="productSize" class="form-control" runat="server"></asp:DropDownList>    
        </div>
        <div class="form-group col-md-2">
        <label>Action</label>
        <asp:Button ID="btnAdded" runat="server" class="form-control btn btn-primary" 
            Text="Added" onclick="btnAdded_Click"/>
        </div>          
    </div>      
    
</form>

<table class="table">
    <asp:Repeater ID="ExportRepeater" runat="server" onitemcommand="ExportRepeater_OnItemCommand">
        <HeaderTemplate>
          <thead >
            <tr>
              <th scope="col">ID</th>
              <th scope="col">Name</th>
              <th scope="col">Export Price</th>
              <th scope="col">Size</th>
              <th scope="col">Quantity</th>
            </tr>
          </thead>
        </HeaderTemplate>        
        <ItemTemplate>  
            <tr>
              <th scope="row"><asp:Label ID="lblIdRpt" runat="server" Text='<%#Eval("Product.ID") %>' /></th>
              <td><asp:Label ID="txtNameRpt" runat="server" Text='<%#Eval("Product.Name") %>' ></asp:Label></td>
              <td><asp:Label ID="txtExportPriceRpt" runat="server" Text='<%#Eval("Product.ExportPrice") %>' ></asp:Label></td>
              <td><asp:Label ID="sizeRpt" runat="server" Text='<%#Eval("Size") %>' ></asp:Label></td>
              <td><asp:TextBox ID="txtQuantityRpt" runat="server" Text='<%#Eval("Quantity") %>' Enabled="false"></asp:TextBox></td>
              <td><asp:Button ID="btnEdit" class="btn btn-primary" CommandName="edit" Visible="true" runat="server" Text="Edit"></asp:Button>
                  <asp:Button ID="btnUpdate" class="btn btn-primary" CommandName="update" Visible="false" runat="server" Text="Update"></asp:Button>
                  <asp:Button ID="btnCancel" class="btn btn-primary" CommandName="cancel" Visible="false" runat="server" Text="Cancel"></asp:Button>
              </td>
            </tr>
        </ItemTemplate>        
    </asp:Repeater>
    <tr>
        <td colspan="4">Total:</td>
        <td><asp:Label ID="lbTotal" runat="server" Text='0' Enabled="false"></asp:Label></td>
    </tr>
</table>
    <asp:PlaceHolder ID="PagingPlaceHolder" runat="server"></asp:PlaceHolder>
</asp:Content>
