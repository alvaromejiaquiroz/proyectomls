<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaTareasGenerales.aspx.cs" Inherits="Solicitudes_ListaTareasGenerales" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" 
        
        AutoGenerateColumns="False" onrowdeleting="GridView1_RowDeleting" 
        onrowediting="GridView1_RowEditing" HorizontalAlign="Left">
        <Columns>
        <asp:BoundField DataField="iDSolicitud" HeaderText="Solicitud" />
        <asp:BoundField DataField="FechaInicio" HeaderText="Inicio" />
        <asp:BoundField DataField="FechaFin" HeaderText="Fin" />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
        <asp:BoundField DataField="Duracion" HeaderText="Duración" />
        
        
        
            <asp:CommandField 
            ButtonType="Image" 
            DeleteImageUrl="~/images/delete.gif"  DeleteText="Eliminar" 
            EditImageUrl="~/images/b_edit.gif"  EditText="Editar"   
            ShowCancelButton="False" 
            ShowDeleteButton="True" 
            ShowEditButton="True" />

        </Columns>
    </asp:GridView>
</asp:Content>

