<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu01Edit.aspx.cs" Inherits="WebHTML.Page.Menu01Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../CSS/CommonCss.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <p>父级选项：<asp:DropDownList runat="server" ID="dp_Tree01">
            <asp:ListItem>sdsd</asp:ListItem>
            <asp:ListItem>as</asp:ListItem>
                </asp:DropDownList> <asp:Button runat="server" ID="btn_Delete" Text="删除" OnClick="btn_Delete_Click" /></p>
        <p>子集名称：<asp:TextBox runat="server" ID="txt_Title" /><asp:Button runat="server" ID="btn_Insert" Text="添加" OnClick="btn_Insert_Click" /></p>
    </form>
</body>
</html>
