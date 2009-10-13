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

    <asp:Menu ID="Menu1" runat="server" DataSourceID="SiteMapDataSource1" 
            BackColor="#F7F6F3" DynamicHorizontalOffset="2" Font-Names="Verdana" 
            Font-Size="0.8em" ForeColor="#7C6F57" 
            onmenuitemdatabound="Menu1_MenuItemDataBound" StaticSubMenuIndent="10px">
            <StaticSelectedStyle BackColor="#5D7B9D" />
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
            <DynamicMenuStyle BackColor="#F7F6F3" />
            <DynamicSelectedStyle BackColor="#5D7B9D" />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
        </asp:Menu>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
      
    <div>
    <asp:ImageButton runat="server" ID="m" onclick="m_Click" style="width: 14px" />
        &gt;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

