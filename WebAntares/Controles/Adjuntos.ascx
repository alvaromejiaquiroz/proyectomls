<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Adjuntos.ascx.cs" Inherits="Controles_Adjuntos" %>

<asp:UpdatePanel ID="UpdateAdjuntos" runat="server">
    <ContentTemplate>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Archivo"></asp:Label>
        </div>
        <div>
            <iframe id="iUploadFrame" runat="server" name="iUploadFrame" src="../Html/MantPreventivoUpload.htm" height="100px" scrolling="no" frameborder="0"></iframe>
        </div>
        <asp:Button id="btnUpload" runat="server" style="visibility:hidden;" 
                onclick="btnUpload_Click" />
        <div>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
        <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" 
            OnRowDeleting="gvFiles_RowDeleting" OnRowDataBound="gvFiles_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IdAdjunto" HeaderText="Id" ReadOnly="True" Visible="false" />
                <asp:BoundField DataField="FileName" HeaderText="Archivo" Visible="true" />
                <asp:BoundField DataField="Date" HeaderText="Fecha" />
                <asp:BoundField DataField="size" HeaderText="Tamaño" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <div id="noprint1">
                            <asp:ImageButton ID="imgAdjunto" runat="server" CausesValidation="False" CommandName="Delete"
                                ImageUrl="~/Images/delete.gif" Text="Delete" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>       
    </ContentTemplate>
</asp:UpdatePanel>