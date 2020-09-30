<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/pages/BAVMaster.Master" AutoEventWireup="true" CodeBehind="NotificacionNueva.aspx.cs" Inherits="IBBAV.pages.NotificacionViajes.NotificacionNueva" %>
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
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/bootstrap-table.min.js") %>"></script>    
    <script type="text/javascript" src="<%= ResolveUrl("~/bootstrap-table/locale/bootstrap-table-es-VE.min.js") %>"></script>    
    <script type="text/javascript" src="<%= ResolveUrl("~/js/accounting.min.js") %>"></script>
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        
		<div class="well">
           Esta Opción le permitira registrar una notificación de viaje.
		   </br>
		   Recuerde:
		   </br>
		   * Cada Notificación podrá tener un máximo de tres (3) destinos distintos.
		   </br>
		   * Podrá realizar operaciones desde el exterior, por un periodo máximo de seis (6) meses. Luego de trasncurrido este periodo, deberá realizar una nueva notificación.
        </div> 
        
        <asp:Panel ID="panelValidacion" runat="server">
            <uc1:PreguntasDesafio runat="server" ID="PreguntasDesafio" TipoPreguntas="PreguntasAfiliado" TipoRepeater="Table" MaskedInputs="true" />
            <center>
                <asp:Button id="btnAceptarPreguntas" runat="server" Text="Aceptar" CssClass="btn btn-danger" OnClick="btnAceptarPreguntas_Click" />
                <asp:Button id="btnCancelarPreguntas" runat="server" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelarPreguntas_Click" />
            </center>
        </asp:Panel>
    
            <asp:UpdatePanel ID="panelDatos" runat="server" UpdateMode="Conditional" Visible ="false">
                <ContentTemplate>
                    <table id="tblClaves" runat="server" class="table table-responsive table-bav">
						<tr>
							<td class="fieldTxt">País de Destino:</td>
							<td class="fieldVal">
								<cc1:DropDownListPaises ID="ListaDestino" runat="server" CssClass="form-control cca"></cc1:DropDownListPaises>
							</td>
							<td class="fieldInf"></td>
						</tr>
						<tr>
							<td class="fieldTxt">Fecha de Salida:</td>
							<td class="fieldVal">
								<div class="input-group">
									<span class="input-group-addon">
										<span class="glyphicon glyphicon-calendar" aria-hidden=""></span>
									</span>
									<asp:TextBox ID="FD" runat="server" CssClass="form-control input-sm fd" aria-label="..." placeholder="Fecha desde"></asp:TextBox>
								</div>
							</td>
							<td class="fieldInf"></td>
						</tr>
						<tr>
							<td class="fieldTxt">Fecha de Retorno:</td>
							<td class="fieldVal">
								<div class="input-group">
									<span class="input-group-addon">
										<span class="glyphicon glyphicon-calendar" aria-hidden=""></span>
									</span>
									<asp:TextBox ID="FH" runat="server" CssClass="form-control input-sm fh" aria-label="..." placeholder="Fecha hasta"></asp:TextBox>
								</div>
							</td>
							<td class="fieldInf"></td>
						</tr>          
						<tr>							
						    <td colspan="3" class="buttons">
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-danger"  class="glyphicon glyphicon-refresh glyphicon-refresh-animate iconconsultar" OnClientClick="return validar();" OnClick="btnAceptar_Click" />                                
                                <asp:button id="btnRegresar" runat="server" Text="Regresar" Cssclass="btn btn-danger" OnClick="btnRegresar_Click"/>
                            </td>
                        </tr>						
					</table>
					
					<script>
                        //Calendario
                        function validar() {
                           var fd = $("#<%= FD.ClientID %>"),
                           fh = $("#<%= FH.ClientID %>"),
                           i = $('.form-control');
                           i.parent().removeClass('has-error');

                           $(".alert").hide();

                           if (fd.val() == "") {
                               i = $('.fd');
                               i.parent().addClass('has-error');
                               i.focus();
                               showAlert("Alerta", "Debe seleccionar la fecha salida");
                              
                               return false;
                           }
                           if (fh.val() == "") {
                               i = $('.fh');
                               i.parent().addClass('has-error');
                               i.focus();
                               showAlert("Alerta", "Debe seleccionar la fecha de retorno");
                               
                               return false;
                            }

                            i = $(".cca");
                            if (i && i.val() == '0') {
                               i.parent().addClass('has-error');
                               i.focus();
                               showAlert("Alerta", "Debe seleccionar un país de destino");
                            }
                           return true;
                        }
                        function doClick() {
                            console.log("text");
                                <%= ClientScript.GetPostBackEventReference(btnAceptar, "")%>;                       
                        }   

                        $(function () {

                            var d;
                            $("#<%= FD.ClientID %>").datepicker({
                                dateFormat: "dd/mm/yy",
                                changeMonth: true,
                                changeYear: true,
                                maxDate: "1y",
                                minDate: "-0y",
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
                                },
                                
                                onClose: function (selectedDate, inst) {

                                    $("#<%= FH.ClientID %>").datepicker("option", "disabled", false).val("");
                                    var selected_date = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
                                    new_date = new Date(selected_date.getTime() + 86400000);
                                    new_date = jQuery.datepicker.formatDate('dd/mm/yy', new_date);
                                    $("#<%= FH.ClientID %>").datepicker("option", "minDate", new_date);
                                }

                             }).attr('readonly', 'readonly').css("cursor", "pointer");

          
                            $("#<%= FH.ClientID %>").datepicker({
                              dateFormat: "dd/mm/yy",
                              changeMonth: true,
                              changeYear: true,
                              maxDate: "1y",
                              minDate: $("#<%= FD.ClientID %>").val()+1,
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

                            $("#<%= FH.ClientID %>").datepicker("option", "disabled", true).val("");            


                        });
                    </script>
				
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ListaDestino" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="btnAceptar"/>
					<asp:PostBackTrigger ControlID="btnRegresar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
</asp:Content>