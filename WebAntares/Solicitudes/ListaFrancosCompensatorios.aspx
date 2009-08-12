﻿<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="ListaFrancosCompensatorios.aspx.cs"
    Inherits="Solicitudes_ListaFrancosCompensatorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <table width="700px" style="border-style: solid; border-color: #000000; border-width: 1px">
        <tr class="header_custom">
            <td align="center">
                Solicitudes de francos compensatorios
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting"
                    OnRowEditing="GridView1_RowEditing">
                    <Columns>
                        <asp:BoundField DataField="iDSolicitud" HeaderText="Solicitud" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha de inicio" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FechaFin" HeaderText="Fecha de fin" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Duracion" HeaderText="Duración" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ConElConsentimiento" HeaderText="Con el consentimiento de" HeaderStyle-HorizontalAlign="Center" />
                        <asp:CommandField HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            ButtonType="Image" EditImageUrl="~/images/b_edit.gif" EditText="Editar" ShowCancelButton="False"
                            ShowEditButton="True" />
                        <asp:CommandField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            ButtonType="Image" DeleteImageUrl="~/images/delete.gif" DeleteText="Eliminar"
                            ShowCancelButton="False" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
    </table>
</asp:Content>
