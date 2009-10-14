<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Solicitudes_test" %>

<%@ Register src="../Controles/Adjuntos.ascx" tagname="Adjuntos" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
<script language="javascript" type="text/javascript">
function MoveTab(num)
    {
        var container = $find('Tabs');
        container.set_activeTabIndex(num);
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

    <asp:Menu ID="Menu1" runat="server"  Orientation="Horizontal"
            BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" 
            Font-Size="0.8em" ForeColor="#990000" 
            StaticSubMenuIndent="10px">
            <StaticSelectedStyle BackColor="#FFCC66" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
            <DynamicMenuStyle BackColor="#FFFBD6" />
            <DynamicSelectedStyle BackColor="#FFCC66" />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticHoverStyle BackColor="#990000" ForeColor="White" />
        </asp:Menu>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
      
    <div>
    <asp:ImageButton runat="server" ID="m" onclick="m_Click" style="width: 14px" />
        &gt;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="false">
    <ContentTemplate>
    <cc1:TabContainer ID="Tabs" runat="server" Height="450px" >
                    <cc1:TabPanel ID="tpFalla" HeaderText="1" runat="server">
                        <ContentTemplate>
                          <input id="Button1" type="button" value="Next" onclick="MoveTab(1)" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel1" HeaderText="2" runat="server">
                        <ContentTemplate>
                          <input id="Button3" type="button" value="Next" onclick="MoveTab(1)" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" HeaderText="3" runat="server">
                        <ContentTemplate>
                          <input id="Button2" type="button" value="Next" onclick="MoveTab(1)" />
                        </ContentTemplate>
                    </cc1:TabPanel>
    </cc1:TabContainer>
   
    
    
    
    </ContentTemplate>
    </asp:UpdatePanel>
</form>
</body>
</html>

