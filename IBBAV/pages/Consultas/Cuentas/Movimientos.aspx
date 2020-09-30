<%@ Page Title="" Language="C#" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="Movimientos.aspx.cs" Inherits="IBBAV.pages.Consultas.Cuentas.Movimientos" %>
<%@ Register Assembly="IBBAV" Namespace="IBBAV.UserControls.BAVCommons" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.js") %>"></script>
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/jquery-ui.theme.css") %>" />

    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/ui-lightness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/js/vendor/jquery-ui-1.11.1/themes/smoothness/jquery-ui.css") %>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.css") %>" />
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/locale/bootstrap-table-es-VE.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/js/accounting.min.js") %>"></script>
    <script type="text/javascript">
        var myData = [];

    </script>
    <style>
        .glyphicon-refresh-animate {
            -animation: spin .7s infinite linear;
            -webkit-animation: spin .7s infinite linear;
            -moz-animation: spin .7s infinite linear;
        }

        @-webkit-keyframes spin {
            from { transform: scale(1) rotate(0deg);}
            to { transform: scale(1) rotate(360deg);}
        }

        @-moz-keyframes spin {
            from { transform: scale(1) rotate(0deg);}
            to { transform: scale(1) rotate(360deg);}
        }
        
        @keyframes spin {
            from { transform: scale(1) rotate(0deg);}
            to { transform: scale(1) rotate(360deg);}
        }
        .iconconsultar {
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container-fluid">
        <div class="well">
            Obtén toda la información detallada de las cuentas, sin dirigirte a la Agencia
        </div>
        <div class="row">
            <div class="col-md-8">
                <div class="form-group">
                    <label for="cuenta" class="col-md-3 control-label">Seleccione una Cuenta:</label>
                    <div class="col-md-4">
                        <cc1:DropDownListCuentas ID="ddlCuenta" runat="server" OnSelectedIndexChanged="ddlCuenta_SelectedIndexChanged" CssClass="form-control input-sm"></cc1:DropDownListCuentas>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="well">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: center">Saldo Total:<br />
                            <asp:Label ID="lblSaldoTotal" runat="server" Width="152px" Text="0.00"></asp:Label></td>
                        <td style="text-align: center">Saldo Diferido:<br />
                            <asp:Label ID="lblSaldoDif" runat="server" Width="152px" Text="0.00"></asp:Label></td>
                        <td style="text-align: center">Saldo Bloqueado:<br />
                            <asp:Label ID="lblSaldoBloq" runat="server" Width="152px" Text="0.00"></asp:Label></td>
                        <td style="text-align: center">Saldo Disponible:<br />
                            <asp:Label ID="lblSaldoDisponible" runat="server" Width="152px" Text="0.00"></asp:Label></td>
						<!-- Agregado 12/09/2018 por Liliana Guerra para mostrar saldos en petro 
                                     Se incluye columna Petro-->
						<td style="text-align: center">Saldo Disponible en PETRO:<br />
                            <asp:Label ID="lblSaldoPetro" runat="server" Width="152px" Text="0.00"></asp:Label></td>
                    </tr>
                </table>
                </div>
                <asp:Literal ID="liScript" runat="server"></asp:Literal>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                
            </Triggers>
        </asp:UpdatePanel>
        
            <table style="width: 100%">
                <tr>
                    <td style="width: 25%">
                        <div class="checkbox">
                            <label>
                                <asp:RadioButton ID="rdoDia" runat="server" CssClass="rdoDia" GroupName="busqueda" OnCheckedChanged="rdoCheck_CheckedChanged" value="1" />
                                Del día
                            </label>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div class="checkbox">
                            <label>
                                <asp:RadioButton ID="rdoMes" runat="server" CssClass="rdoMes" GroupName="busqueda" OnCheckedChanged="rdoCheck_CheckedChanged"  value="2" />
                                Del mes
                            </label>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div class="checkbox">
                            <label>
                                <asp:RadioButton ID="rdoRango" runat="server" CssClass="rdoRango" type="radio" GroupName="busqueda" value="5" />
                                Rango
                            </label>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div class="input-group">
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar" aria-hidden=""></span>
                            </span>
                            <asp:TextBox ID="FD" runat="server" CssClass="form-control input-sm" aria-label="..." placeholder="Fecha desde"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <div class="checkbox">
                            <label>
                                <asp:RadioButton ID="rdoDiaAnt" runat="server" CssClass="rdoDiaAnt" GroupName="busqueda" OnCheckedChanged="rdoCheck_CheckedChanged"  value="3" />
                                Del día anterior
                            </label>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div class="checkbox">
                            <label>
                                <asp:RadioButton ID="rdoMesAnt" runat="server" CssClass="rdoMesAnt" GroupName="busqueda" OnCheckedChanged="rdoCheck_CheckedChanged"  value="4" />
                                Del mes anterior
                            </label>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div class="checkbox">
                            <label>
                                <asp:RadioButton ID="rdoUltimosMov" runat="server" CssClass="rdoUltimosMov" GroupName="busqueda" OnCheckedChanged="rdoCheck_CheckedChanged"  value="6" />

                                20 últimos movimientos
                            </label>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div class="input-group">
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar" aria-hidden=""></span>
                            </span>
                            <asp:TextBox ID="FH" runat="server" CssClass="form-control input-sm" aria-label="..." placeholder="Fecha hasta"></asp:TextBox>
                        </div>
                    </td>
                </tr>
            </table>
        <br />
        <center>   
            
                 
            <asp:LinkButton ID="btnConsultar" runat="server" CssClass="btn btn-danger btnConsultar" OnClientClick="return validar();" OnClick="btnConsultar_Click">
                <i aria-hidden="true" class="glyphicon glyphicon-refresh glyphicon-refresh-animate iconconsultar" ></i> Consultar
            </asp:LinkButton>
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="btnRegresar_Click" />
            <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-danger btnExportar" OnClick="btnExportar_Click">
                <i aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></i> Exportar
            </asp:LinkButton>          
        </center>
        <br/>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                <ContentTemplate>
                    <b>Criterio seleccionado: </b><asp:Literal ID="liCritero" runat="server"></asp:Literal>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <table id="tabla" class="table-striped" data-height="299" data-cache="false">
                <thead>
                    <tr>
                        <th class="col-md-2" data-field="SFechaProc" data-sortable="true" data-align="center" data-formatter="dateFormatter">Fecha Transacción</th>
                        <th class="col-md-2" data-field="SFechaEfect" data-sortable="true" data-align="center" data-formatter="dateFormatter">Fecha Efectiva</th>
                        <th class="col-md-2 view" data-field="SChqRef" data-sortable="true" data-align="center" data-formatter="operateFormatter" data-events="operateEvents">Referencia</th>
                        <th class="col-md-4" data-field="SDesctrans" data-sortable="true" data-align="left">Descripción</th>
                        <th class="col-md-2" data-field="SMonto" data-sortable="true" data-align="right" data-formatter="amountFormatter">Monto</th>
                    </tr>
                </thead>
            </table>
        </div>
        <script>
            var fd = $("#<%= FD.ClientID %>"),
                fh = $("#<%= FH.ClientID %>");
            function validar() {
                $(".alert").hide();
                $(".iconconsultar").show();
                if ($("#<%= ddlCuenta.ClientID %>").val() == 0) {
                    showAlert("Alerta", "Debe seleccionar una cuenta");
                    $(".iconconsultar").hide();
                    return false;
                }


                var rdo = $("input[type='radio']:checked");
                
                if (rdo.val() != "5") {
                    
                    
                }
                else {
                    

                    
                    if (fd.val() == "") {
                        showAlert("Alerta", "Debe seleccionar la fecha desde");
                        $(".iconconsultar").hide();
                        return false;
                    }
                    if (fh.val() == "") {
                        showAlert("Alerta", "Debe seleccionar la fecha hasta");
                        $(".iconconsultar").hide();
                        return false;
                    }

                }

                return true;
            }
            $(document).ready(function () {
                $('#tabla').bootstrapTable({
                    striped: true,
                    pagination: true,
                    pageSize: 20,
                    pageList: [10, 20, 50, 100, 200],
                    height: 250,
                    data : myData
                });

                
            });

            $(function () {

                $(window).resize(function () {
                    $('#tabla').bootstrapTable('resetView');
                });

                $("#<%= FD.ClientID %>, #<%= FH.ClientID %>").datepicker({
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    maxDate: "-0y",
                    minDate: "-1y",
                    //yearRange: "-1:-1",
                    //showButtonPanel: true,
                    prevText: "<<",
                    nextText: ">>",
                    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'], // For formatting
                    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
                    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                    monthNamesMin: ['En', 'Fe', 'Ma', 'Ab', 'My', 'Jn', 'Ju', 'Ag', 'Se', 'Oc', 'No', 'Di'],
                    firstDay: 1,
                    beforeShow: function () {
                        setTimeout(function () {
                            $('.ui-datepicker').css('z-index', 99999999999999);
                        }, 0);
                    },
                    onSelect: function (dateText) {
                        
                        $(".alert").hide();

                    }
                }).attr('readonly', 'readonly').css("cursor", "pointer");

                $("#<%= FD.ClientID %>, #<%= FH.ClientID %>").datepicker("option", "disabled", true).val("");


                $("input[type='radio']").on("click", function (event) {
                    var rdo = $(this);
                    //console.log(rdo.val());
                    $("#<%= FD.ClientID %>, #<%= FH.ClientID %>").datepicker("option", "disabled", rdo.val() != "5").val("");


                });
               


            });

                function operateFormatter(value, row, index) {
                    return [
                        '<a class="view ml10" href="javascript:void(0)" title="Ver detalle" >',
                            value,
                        '</a>'
                    ].join('');
                }
                function amountFormatter(value) {
                    
                    return accounting.formatMoney(value, "", 2, ".", ",");

                }
                function dateFormatter(value) {

                    return moment(value).format("DD/MM/YYYY");

                }
                window.operateEvents = {
                    'click .view': function (e, value, row, index) {

                        $(".modal-body").find("#ref").html(row.SChqRef);
                        $(".modal-body").find("#fectod").html(moment().format("DD/MM/YYYY hh:mm:ss a"));
                        $(".modal-body").find("#des").html(row.SDesctrans);
                        $(".modal-body").find("#fec").html(moment(row.SFechaProc).format("DD/MM/YYYY hh:mm:ss a"));
                        $(".modal-body").find("#mon").html(amountFormatter(row.SMonto));
                        $('#myModal').modal();

                    }

                };

                function loadTable() {
                    $("#tabla").bootstrapTable("load", myData);
                }
                //function updateTable() {
                //    $("#tabla").bootstrapTable().load(myData);

                //}
        </script>

        
    </div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                    <div class="pull-left">
                        <strong>Fecha:</strong> <span id="fectod"></span>
                    </div>
                    <div class="pull-right">
                        Referencia Nro.: <b id="ref"></b>
                    </div>
                    <br />
                    <br />
                    <br />
                    <table class="table">
                        <tr>
                            <td><b>Descripción</b></td>
                            <td style="text-align: right" id="des">Depósito</td>
                        </tr>
                        <tr>
                            <td><b>Fecha</b></td>
                            <td style="text-align: right" id="fec">14/01/2015</td>
                        </tr>
                        <tr>
                            <td><b>Monto</b></td>
                            <td style="text-align: right" id="mon">0,00</td>
                        </tr>
                    </table>
                    <center>
						<!--<button type="button" class="btn btn-danger">
							<span class="glyphicon glyphicon-print" aria-hidden="true"></span> Imprimir
						</button>-->
						
					</center>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        
    </script>
</asp:Content>
