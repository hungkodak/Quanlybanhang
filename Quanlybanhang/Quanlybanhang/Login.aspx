<%@ Page Title="Signin" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Quanlybanhang.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <script src="Scripts/jquery-3.3.1.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/signin.css" rel="stylesheet" type="text/css" />   
</head>
<body class="text-center">    
    <div>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>        
    </div>
    <form class="form-signin" runat="server">
      <img class="mb-4" src="Images/logo.jpg" alt="" width="256" height="256">
      <h1 class="h3 mb-3 font-weight-normal">Please sign in</h1>
      <label for="txtusername" class="sr-only">Username</label>
      <asp:TextBox ID="txtusername" class="form-control" runat="server" placeholder="User Name" required autofocus></asp:TextBox>
      <label for="inputPassword" class="sr-only">Password</label>
      <asp:TextBox ID="txtpassword" class="form-control" TextMode="Password" runat="server" placeholder="Password" required></asp:TextBox>
      <asp:Button ID="btnlogin" runat="server" class="btn btn-lg btn-primary btn-block" 
                Text="Sign In" onclick="btnlogin_Click" 
                 />
      <p class="mt-5 mb-3 text-muted">&copy; 2018 by Hungkodak</p>
    </form>

</body>
</html>
