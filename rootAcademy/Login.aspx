<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="rootAcademy.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet"/>
    <style type="text/css">
	.login-form {
		width: 340px;
    	margin: 50px auto;
	}
    .login-form form {
    	margin-bottom: 15px;
        background: #f7f7f7;
        box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        padding: 30px;
    }
    .login-form h2 {
        margin: 0 0 15px;
    }
    .form-control, .btn {
        min-height: 38px;
        border-radius: 2px;
    }
    .btn {        
        font-size: 15px;
        font-weight: bold;
    }
</style>
</head>
<body>
    <script src="~/Scripts/jquery-3.3.1.min.js"></script>
    <script src="~/Scripts/bootstrap.min.js"></script>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet"/>
    <div class="login-form">
    <asp:Image ID="Image1" runat="server" Height="69px" ImageUrl="~/Content/rootAcademy.png" Width="334px" />
    <form id="form1" action="Test.aspx" method="post" runat="server">
        <h2 class="text-center">Log in</h2>       
        <div class="form-group">
            <div class="input-group glyph-group">
                <div class="input-group-addon"><i class="glyphicon glyphicon-user"></i></div>
                <asp:TextBox ID="username" name="username" runat="server" CssClass="form-control" placeholder="Usuario" required="required"></asp:TextBox>
        </div>
        <div class="form-group">
            <div class="input-group glyph-group">
                <div class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></div>
                <asp:TextBox ID="password" name="password" runat="server" CssClass="form-control" placeholder="Contraseña" required="required" type="password"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" class="btn btn-primary btn-block" OnClick="btnIngresar_Click"/>
        </div>
    </form>
        <p class="text-center">
        <asp:HyperLink ID="btnRegistrarse" runat="server">Registrarse
        </asp:HyperLink>
        </p>
    </div>
</body>
</html>
