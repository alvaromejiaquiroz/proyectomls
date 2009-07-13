<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true"
    CodeFile="MantRendicion.aspx.cs" Inherits="Solicitudes_MantRendicion" EnableEventValidation="false" %>

<%@ Register Src="../Controles/SolDetalle.ascx" TagName="SolDetalle" TagPrefix="uc1" %>
<%@ Register Src="../Controles/Tareas.ascx" TagName="Tareas" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageContainer" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/flexigrid/flexigrid.css">

    <script type="text/javascript" src="../lib/jquery/jquery.js"></script>

    <script src="flexigrid.js" type="text/javascript"></script>

    <script>
        //        $(document).ready(function() {

        //            //            $.post("../tareas/tareas.aspx",
        //            //            { id: "2", time: "2pm" },
        //            //            function(data) {
        //            //                $("#divtareas").html(data);
        //            //                $('.flexme2').flexigrid();

        //            //            });

        //            //seleccionado
        //            $("#selected").click(function() {
        //                var grid = $(".flexme2");
        //                alert($('.trSelected', grid).length);
        //                $('.trSelected', grid).each(function() {
        //                    alert($('div:eq(1)', $(this)).text());
        //                    alert($(this).children()[0].innerText);
        //                })
        //            })
        //        })





        function makeFlexGrid() {
            $("#flex1").flexigrid
			(
			{
			    url: 'tareas.ashx', datatype: "json",
			    colModel: [
				{ display: 'Id', name: 'Id', width: 40, sortable: true, align: 'center' },
				{ display: 'Inicio', name: 'tipo', width: 180, sortable: true, align: 'left' },
				{ display: 'Fin', name: 'inicio', width: 120, sortable: true, align: 'left' },
				{ display: 'Lugar', name: 'lugar', width: 130, sortable: true, align: 'left', hide: true },
				{ display: 'Tarea', name: 'tarea', width: 80, sortable: true, align: 'right' }
				],
			    buttons: [
				{ name: 'Agregar', bclass: 'add', onpress: test },
				{ name: 'Borrar', bclass: 'delete', onpress: test },
				{ separator: true }
			xccc	],
			    searchitems: [
				{ display: 'Id', name: 'id' },
				{ display: 'TipoSolicitud', name: 'TipoSolicitud', isdefault: true }
				],

			    usepager: true,
			    title: 'Tareas',
			    useRp: true,
			    rp: 15,
			    showTableToggleBtn: true,
			    width: 700,
			    height: 200
			}
			);

        }
        function rowdelete(idtarea) {
            alert("borrar tarea" + idtarea);
        }
        function rowSelected(idtarea) {
            alert("borrar tarea" + idtarea);
        }
        function test(com, grid) {
            if (com == 'Delete') {
                confirm('Delete ' + $('.trSelected', grid).length + ' items?')
            }
            else if (com == 'Add') {
                alert('Add New Item');
            }
        }
    </script>

    <div id="divSolicitud">
        <uc1:SolDetalle ID="SolDetalle1" runat="server" />
    </div>
    <div id="divtareas">
        <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="False" CssClass="flexme2"
            EmptyDataText="No hay registros de datos para mostrar." OnSelectedIndexChanged="gvTareas_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="IdAlumno" />
                <asp:BoundField DataField="Inicio" HeaderText="Inicio" />
                <asp:BoundField DataField="Fin" HeaderText="Fin" />
                <asp:BoundField DataField="Sitio" HeaderText="Sitio" />
                <asp:BoundField DataField="tarea" HeaderText="Tarea" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <div id="noprint">
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                ImageUrl="~/Images/b_edit.gif" Text="Ver" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <button id="selected">
            Seleccionados</button>
        <table id="tareasHoras">
            <tr>
                <td>
                    IdTarea:
                </td>
                <td>
                    <asp:TextBox ID="txtIdTarea" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Tarea:
                </td>
                <td>
                    <asp:TextBox ID="txtTarea" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Realizada:
                </td>
                <td>
                    <asp:DropDownList ID="cboEstado" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Tiempo:
                </td>
                <td>
                    <asp:TextBox ID="txtTiempo" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="divtraslados">
    </div>
    <div id="divgastos">
    </div>
    <div id="divpersonal">
    </div>
    <div id="divadjuntos">
    </div>
</asp:Content>
