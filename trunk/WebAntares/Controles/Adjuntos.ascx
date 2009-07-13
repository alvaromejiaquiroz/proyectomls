<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Adjuntos.ascx.cs" Inherits="Controles_Adjuntos" %>
    <table>
    <tr>
    <td>
<asp:Label ID="Label1" runat="server" Text="Archivo"></asp:Label>
    </td>
    <td>
<asp:FileUpload ID="File1" runat="server" Height="25px"/>
    </td>
    <td>
<asp:Button ID="cmdUpload" runat="server" onclick="cmdUpload_Click" Height="25px" 
    Text="Agregar" />
    </td>
    
    </tr>
    </table>
<p>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
   </p>

 <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False"
        OnRowDeleting="GridView1_RowDeleting">
        <Columns>
            <asp:BoundField DataField="IdAdjunto" HeaderText="Id" ReadOnly="True"  Visible="false"/>
            <asp:BoundField DataField="FileName" HeaderText="Archivo" Visible="true" />
            <asp:BoundField DataField="Date" HeaderText="Fecha" />
            <asp:BoundField DataField="size" HeaderText="Tamaño" />
            <asp:TemplateField>
                <ItemTemplate>
                    <div id="noprint1">
                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" CommandName="Delete"
                            ImageUrl="~/Images/delete.gif" Text="Delete" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
