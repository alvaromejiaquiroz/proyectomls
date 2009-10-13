<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tareas.ascx.cs" Inherits="Controles_Tareas" %>
 
<asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="False" 
    onrowdeleting="gvTareas_RowDeleting">    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="IdAlumno" />
        <asp:BoundField DataField="Inicio" HeaderText="Inicio" />
        <asp:BoundField DataField="Fin" HeaderText="Fin" />
        <asp:BoundField DataField="Sitio" HeaderText="Sitio" />
        <asp:BoundField DataField="tarea" HeaderText="Tarea" />
        <asp:TemplateField>
            <ItemTemplate>
                <div id="noprint">
                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                        ImageUrl="~/Images/basuravacio.gif" Text="Delete" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

