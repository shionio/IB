<%@ Page Title="" EnableEventValidation="false" ValidateRequest="false" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="DetalleNotificacion.aspx.cs" Inherits="IBBAV.pages.NotificacionViajes.DetalleNotificacion" %>
<%@ Register Src="~/UserControls/PreguntasDesafio.ascx" TagPrefix="uc1" TagName="PreguntasDesafio" %>
<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.js") %>"></script>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.theme.css") %>" />

    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/ui-lightness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/smoothness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.css") %>" />
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/locale/bootstrap-table-es-VE.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/accounting.min.js") %>"></script>
    <style>
        .gi-2x{font-size: 1em;}
        .gi-3x{font-size: 2em;}
        .gi-4x{font-size: 3em;}
        .gi-5x{font-size: 4em;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="well">
            En esta sección podrá consultar, agregar y eliminar destinos en su notificación de viaje
		   </br>
		   Recuerde:
		   </br>
		   * Cada Notificación podrá tener un máximo de tres (3) destinos distintos.
		   </br>
		   * Podrá realizar operaciones desde el exterior, por un periodo máximo de seis (6) meses.
            </br>
            Luego de trasncurrido este periodo, deberá realizar una nueva notificación.
            </br>
           * Para eliminar una notificación, debe eliminar todos los destinos agregados.
        </div>
        <asp:Panel ID="panelValidacion" runat="server">
            <uc1:PreguntasDesafio runat="server" ID="PreguntasDesafio" TipoPreguntas="PreguntasAfiliado" TipoRepeater="Table" MaskedInputs="true" />
            <center>
                <asp:Button id="btnAceptarPreguntas" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClick="btnAceptarPreguntas_Click" />
                <asp:Button id="btnCancelarPreguntas" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelarPreguntas_Click" />
            </center>
        </asp:Panel>
        <asp:Panel ID="panelDatos" runat="server" Visible="false">
            <table id="tabla" class="table-striped" data-height="299" data-cache="false">
                <thead>
                    <tr>
                       <th class="col-md-1" data-field="País" data-sortable="true" data-align="center" >País Destino</th>
                        <th class="col-md-2" data-field="FechaInicio" data-sortable="true" data-align="center">Fecha de Salida</th>
                        <th class="col-md-2" data-field="FechaFin" data-sortable="true" data-align="center">Fecha de Retorno</th>
                        <th class="col-md-1" data-formatter="operateFormatter" data-events="operateEvents" data-align="center"></th>
                    </tr>
                </thead>
            </table>
            <center>
                <asp:LinkButton ID="LinkButton1"  runat="server" CssClass="btn btn-danger" OnClick="btnNew_Click"    >
                    <i aria-hidden="true" class="glyphicon glyphicon-plus"></i>Agregar
                </asp:LinkButton>
                <asp:LinkButton ID="btnSalir"  runat="server" CssClass="btn btn-danger" OnClick="btnSalir_Click"     >
                    Salir
                </asp:LinkButton>
        
            </center>

             <script>


                $(function () {


                    loadData();
                    $(window).resize(function () {
                        $('#tabla').bootstrapTable('resetView');
                    });



                    function loadData() {
                        $('#tabla').bootstrapTable({
                            query: [{
                                sesion: '<%= base.SessionId %>'
                            }],
                            method: "post",
                            url: '<%= ResolveUrl("~/ws/Data.asmx/DetallesNotificacion") %>',
                        });
                    }

                    function updateData() {

                        $('#tabla').bootstrapTable('refresh', {
                            query: [{
                                sesion: '<%= base.SessionId %>'
                            }],
                            method: "post",
                            url: '<%= ResolveUrl("~/ws/Data.asmx/DetallesNotificacion") %>'

                        });
                    }
                });
                function dateFormatter(value) {
                    return moment(value).format("DD/MM/YYYY");

                }
                function operateFormatter(value, row, index) {
                    return [
                        //'<a class="modify ml10" href="javascript:void(0)" title="Modificar">',
                        //    '<i class="glyphicon glyphicon-pencil gi-2x"></i>',
                        //'</a>&nbsp;',
                        '<a class="remove ml10" href="javascript:void(0)" title="Eliminar">',
                            '<i class="glyphicon glyphicon-remove gi-2x"></i>',
                        '</a>'
                    ].join('');
                }
                window.operateEvents = {
                    'click .modify': function (e, value, row, index) {
                        if (confirm("¿Desea Modificar este Destino?")) {
                            delete row["__type"];
                            $("#<%= hData.ClientID%>").val(JSON.stringify(row));
                            <%= ClientScript.GetPostBackEventReference(btnModificar,null) %>;
                            return;
                        }
                    },
                    'click .remove': function (e, value, row, index) {
                        if (confirm("¿Desea Eliminar este Destino?")) {
                            delete row["__type"];
                            $("#<%= hData.ClientID%>").val(JSON.stringify(row));
                            <%= ClientScript.GetPostBackEventReference(btnEliminar,null) %>;
                            return;
                        }
                    }
                };

            </script>

        </asp:Panel>

        <asp:HiddenField ID="SesionField" runat="server" />
        <asp:HiddenField ID="hData" runat="server" />
        <asp:Button ID="btnModificar" runat="server" Visible="false" OnClick="btnModificar_Click" />
        <asp:Button ID="btnEliminar" runat="server" Visible="false" OnClick="btnEliminar_Click" />

    </div>
    
</asp:Content>