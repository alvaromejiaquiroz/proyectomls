<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaCapacitacion.aspx.cs" Inherits="Solicitudes_ListaCapacitacion" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        AutoGenerateColumns="False" onrowdeleting="GridView1_RowDeleting" 
        onrowediting="GridView1_RowEditing">
        <Columns>
        <asp:BoundField DataField="ID" HeaderText="Id" Visible="false" />
        <asp:BoundField DataField="ID_solicitud" HeaderText="IdSolicitud" Visible="false" />
        <asp:BoundField DataField="FechaInicio" HeaderText="Inicio"  />
        <asp:BoundField DataField="FechaFin" HeaderText="Fin" />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
        <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
        <asp:BoundField DataField="Duracion" HeaderText="Duracion" />
        <asp:BoundField DataField="AreaEstudio" HeaderText="Area Estudio" />
        <asp:BoundField DataField="Instructor" HeaderText="Instructor" />
        
            <asp:CommandField 
            ButtonType="Image" 
            DeleteImageUrl="~/images/delete.gif" 
            EditImageUrl="~/images/b_edit.gif" 
            ShowCancelButton="False" 
            ShowDeleteButton="True" 
            ShowEditButton="True" />

        </Columns>
    </asp:GridView>
</asp:Content>

