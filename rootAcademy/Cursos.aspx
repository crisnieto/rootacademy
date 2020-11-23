<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="rootAcademy.Cursos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="styles/mydatagrid.css"/>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet"/>
    <style type="text/css">
	.search-group {
		display: inline;
	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="search-group">
            <asp:TextBox ID="TextBox1" runat="server" Width="283px"></asp:TextBox>
            <asp:Button ID="Buscar" runat="server" class="btn" OnClick="Buscar_Click" Text="Buscar" Width="127px" />
        </div>
        <asp:GridView ID="GridView1" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager"
           HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        </asp:GridView>
    </form>
</body>
</html>
