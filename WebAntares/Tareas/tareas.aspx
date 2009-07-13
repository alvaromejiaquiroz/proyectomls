<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tareas.aspx.cs" Inherits="Tareas_tareas"
    EnableViewState="false" Theme="" %>

<html>
<head>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="False" CssClass="flexme2">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="IdAlumno" />
            <asp:BoundField DataField="Inicio" HeaderText="Inicio" />
            <asp:BoundField DataField="Fin" HeaderText="Fin" />
            <asp:BoundField DataField="Sitio" HeaderText="Sitio" />
            <asp:BoundField DataField="tarea" HeaderText="Tarea" />
            <asp:TemplateField>
                <ItemTemplate>
                    <div id="noprint">
                        <img src="../Images/delete.gif" title="Eliminar" onclick="rowdelete( ' <%# DataBinder.Eval(Container.DataItem, "Id") %>');" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
