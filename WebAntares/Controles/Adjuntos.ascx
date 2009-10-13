<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Adjuntos.ascx.cs" Inherits="Controles_Adjuntos" %>

<asp:UpdatePanel ID="UpdateAdjuntos" runat="server">
    <ContentTemplate>
        <table width="600px" class="text_custom">
            <tr>
                <td colspan="2" style="height: 20px">
                </td>
            </tr>
            <tr>
                <td valign="top" style="padding-left: 20px">
                    Archivo adjunto
                </td>
                <td>
                    <div>
                        <iframe id="iUploadFrame" runat="server" name="iUploadFrame" height="100px" scrolling="no" frameborder="0"></iframe>
                    </div>
                    <asp:Button id="btnUpload" runat="server" style="visibility:hidden;" 
                            onclick="btnUpload_Click" CausesValidation="false" />
                    <div>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" 
                        OnRowDeleting="gvFiles_RowDeleting" OnRowDataBound="gvFiles_RowDataBound" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="IdAdjunto" HeaderText="Id" ReadOnly="True" Visible="false" />
                            <asp:BoundField DataField="FileName" HeaderText="Archivo" Visible="true" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Date" HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="size" HeaderText="Tamaño" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAdjunto" runat="server" CausesValidation="False" CommandName="Delete"
                                        ImageUrl="~/Images/basuravacio.gif" Text="Delete" ToolTip="Eliminar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>