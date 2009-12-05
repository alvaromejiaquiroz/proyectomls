<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
<div style="height:350px">
    <div id="divMensaje" runat="server" visible=false 
    style="width:auto; height:25px;width:auto;padding-left:30%;padding-top:50px ;padding-right:30%;">
    
    <table>
        <tr>
            <td align="center">
            <asp:Label ID="lblMensaje" runat="server"  Visible = "true"  Font-Bold="true" Font-Size="Small" ForeColor="Red"></asp:Label>
            </td>
            </tr>
     </table>
        
    </div>
</div>
</asp:Content>



