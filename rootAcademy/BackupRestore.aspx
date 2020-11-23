<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackupRestore.aspx.cs" Inherits="rootAcademy.BackupRestore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="styles/mydatagrid.css"/>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet"/>

</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" CssClass="mydatagrid" AutoGenerateColumns="false" PagerStyle-CssClass="pager"
           HeaderStyle-CssClass="header" RowStyle-CssClass="rows" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">            <Columns>
                <asp:BoundField DataField="Text" HeaderText="File Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Restore" runat="server" CommandArgument = '<%# Eval("Value") %>' CommandName="Restore" class="btn btn-primary" Text="Restore" OnClick="Restore_Click"></asp:Button>    
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Backup" runat="server" OnClick="Backup_Click" class="btn btn-primary" Text="Backup" />
    </form>
</body>
</html>
