<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="rootAcademy.Bitacora" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="styles/mydatagrid.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:GridView ID="GridView1" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager"
           HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
